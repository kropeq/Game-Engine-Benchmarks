using UnityEngine;
using UnityEngine.UI;
using System.Collections;
// required to StopWatch
using System.Diagnostics;
// required to TimeSpan
using System;
// required to fpsy.Sum()
using System.Linq;

public class PhysicsMark : MonoBehaviour {

	Stopwatch stopWatch;
	TimeSpan ts;

	bool countResult = false;
	float deltaTime = 0.0f;
	float cameraSize = 0.0f;
	float minimum = 100.0f;
	float average = 0.0f;
	float maximum = 0.0f;
	float height,width,ratio,widthForBunnies,range;
	int score = 0;
	int increase = 25;

	GameObject rabbit,bunny;
	Text text, averageText, minimumText, maximumText;
	Button testButton;

	// array that keeps fps of every frame when test button is clicked
	// used to count the fps average
	private float[] fpsy = new float[10000];
	// frame counter
	private int i = 0;

	// Use this for initialization
	void Start () {
		rabbit = Resources.Load<GameObject> ("Prefabs/wabbit_alpha");
		text = GameObject.Find ("Score").GetComponent<Text> ();
		minimumText = GameObject.Find ("Minimum").GetComponent<Text> ();
		averageText = GameObject.Find ("Average").GetComponent<Text> ();
		maximumText = GameObject.Find ("Maximum").GetComponent<Text> ();
		testButton = GameObject.Find ("Button").GetComponent<Button> ();

		height = Screen.height;
		width = Screen.width;
		ratio = width / height;

		// Check size of Camera
		cameraSize = Camera.main.orthographicSize;

		// set width for putting clones of Bunny Sprite
		widthForBunnies = ratio * cameraSize * 2.0f;

		createWalls ();
	}
	
	// Update is called once per frame
	void Update () {
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
	}

	// click event for every place of screen
	public void addRabbits(){
		// if this app runs on Windows, Mac OS or Linux platform
		#if UNITY_STANDALONE
		increase = 25;
		range = widthForBunnies / 26;
		for (int i = 0; i < increase; i++) {
			bunny = Instantiate (rabbit);
			bunny.transform.position = new Vector2(((-widthForBunnies/2)+(i+1)*range),4.45f);
		}
		// if this app runs on device with Android
		#elif UNITY_ANDROID
		increase = 10;
		range = widthForBunnies / 11;
		for (int i = 0; i < increase; i++) {
		bunny = Instantiate (rabbit);
		bunny.transform.position = new Vector2(((-widthForBunnies/2)+(i+1)*range),4.45f);
		//bunny.transform.position = new Vector2(Random.Range(-ratio,ratio),Random.Range(3.85f,4.85f));
		}
		#endif
		score += increase;
		text.text = "Rabbits: " + score;
	}

	// create colliders for bunnies to not go outside the view of camera
	void createWalls(){
		// Load sprite texture
		Sprite left = Resources.Load<Sprite> ("wall");
		Sprite right = Resources.Load<Sprite> ("wall");
		Sprite down = Resources.Load<Sprite> ("wall");
		Sprite up = Resources.Load<Sprite> ("wall");

		// Create a new gameobject
		GameObject objLeft = new GameObject ("Left");
		GameObject objRight = new GameObject ("Right");
		GameObject objDown = new GameObject ("Down");
		GameObject objUp = new GameObject ("Up");

		// Set coordinates of the Sprite
		objRight.transform.position = new Vector3 (ratio*cameraSize, 0, 0);
		objLeft.transform.position = new Vector3 (ratio*(-cameraSize), 0, 0);
		objDown.transform.position = new Vector3 (0, -cameraSize, 0);
		objUp.transform.position = new Vector3 (0, cameraSize, 0);

		// Add SpriteRenderer component to object
		SpriteRenderer rendererRight = objRight.AddComponent (typeof(SpriteRenderer)) as SpriteRenderer;
		SpriteRenderer rendererLeft = objLeft.AddComponent (typeof(SpriteRenderer)) as SpriteRenderer;
		SpriteRenderer rendererDown = objDown.AddComponent (typeof(SpriteRenderer)) as SpriteRenderer;
		SpriteRenderer rendererUp = objUp.AddComponent (typeof(SpriteRenderer)) as SpriteRenderer;

		// Set created sprite to Renderer
		rendererRight.sprite = right;
		rendererLeft.sprite = left;
		rendererDown.sprite = down;
		rendererUp.sprite = up;

		// check size of the sprite
		float sizeOfSpriteY = rendererRight.bounds.size.y;
		float sizeOfSpriteX = rendererRight.bounds.size.x;
		// don't know if i'm right, 1 / sizeOfSprite, because sizeOfSprite was 0.1
		float sizeRatioY = 1.0f / sizeOfSpriteY;
		float sizeRatioX = 1.0f / sizeOfSpriteX;
		// check size of the Camera - x2 because it is from the center to the side
		float widthOfCamera = cameraSize * ratio * 2.0f;
		float heightOfCamera = cameraSize * 2.0f;
		// count the height of vertical walls
		float sizeWidth = widthOfCamera * sizeRatioX;
		float sizeHeight = heightOfCamera * sizeRatioY;

		// Set scale of Sprite to fit height
		objRight.transform.localScale = new Vector3(1,sizeHeight,1);
		objLeft.transform.localScale = new Vector3(1,sizeHeight,1);
		objDown.transform.localScale = new Vector3(sizeWidth,1,1);
		objUp.transform.localScale = new Vector3(sizeWidth,1,1);

		// Add RigidBody2D component to objects
		Rigidbody2D rbRight = objRight.AddComponent (typeof(Rigidbody2D)) as Rigidbody2D;
		Rigidbody2D rbLeft = objLeft.AddComponent (typeof(Rigidbody2D)) as Rigidbody2D;
		Rigidbody2D rbDown = objDown.AddComponent (typeof(Rigidbody2D)) as Rigidbody2D;
		Rigidbody2D rbUp = objUp.AddComponent (typeof(Rigidbody2D)) as Rigidbody2D;

		// Turn off the gravity
		rbRight.gravityScale = 0;
		rbLeft.gravityScale = 0;
		rbDown.gravityScale = 0;
		rbUp.gravityScale = 0;

		// Turn on isKinematic to not moving after colliding
		rbRight.isKinematic = true;
		rbLeft.isKinematic = true;
		rbDown.isKinematic = true;
		rbUp.isKinematic = true;

		// Add BoxCollider2D component to object
		BoxCollider2D collRight = objRight.AddComponent (typeof(BoxCollider2D)) as BoxCollider2D;
		BoxCollider2D collLeft = objLeft.AddComponent (typeof(BoxCollider2D)) as BoxCollider2D;
		BoxCollider2D collDown = objDown.AddComponent (typeof(BoxCollider2D)) as BoxCollider2D;
		BoxCollider2D collUp = objUp.AddComponent (typeof(BoxCollider2D)) as BoxCollider2D;
	}

	// click event for test button
	public void onClick(){
		// run the test
		countResult = true;

		// start the timer
		stopWatch = new Stopwatch ();
		stopWatch.Start ();

		// deactivate the button
		testButton.interactable = false;
	}

	void OnGUI()
	{
		int w = Screen.width, h = Screen.height;

		GUIStyle style = new GUIStyle();

		Rect rect = new Rect(0, 0, w, h * 2 / 100);
		style.alignment = TextAnchor.UpperLeft;
		style.fontSize = h * 2 / 40;
		style.normal.textColor = new Color (0.0f, 0.0f, 0.5f, 1.0f);
		float msec = deltaTime * 1000.0f;
		float fps = 1.0f / deltaTime;
		if (countResult) {
			startMeasurement (fps);
		}
		string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
		GUI.Label(rect, text		, style);
	}

	// this is active when user will push a test button
	void startMeasurement(float fps){
		// check elapsed seconds
		ts = stopWatch.Elapsed;
		// test lasts 5 seconds
		if (ts.Seconds > 5) {
			// break counting the result
			countResult = false;

			// count the final average result
			average = (fpsy.Sum ()) / i;

			// stop and reset the timer
			stopWatch.Stop ();
			stopWatch.Reset ();

			// print the results
			minimumText.text = string.Format ("Minimum: {0:0.000}", minimum);
			averageText.text = string.Format ("Average: {0:0.000}", average);
			maximumText.text = string.Format ("Maximum: {0:0.000}", maximum);

			// reset value of variables for the next click event
			for (int index = i; index >= 0; index--) {
				fpsy [index] = 0.0f;
			}
			i = 0;
			minimum = 100.0f;
			average = 0.0f;
			maximum = 0.0f;

			// activate button again
			testButton.interactable = true;
		} else {
			// save current fps of the frame
			fpsy [i] = fps;

			// check if current fps was lower than minimum
			if (fps < minimum) {
				minimum = fps;
			}

			// check if current fps was higher than maximum
			if (fps > maximum) {
				maximum = fps;
			}

			// increase frame counter
			i++;
		}
	}

}
