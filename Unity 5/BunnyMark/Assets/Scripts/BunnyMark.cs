using UnityEngine;
// required to UI elements
using UnityEngine.UI;
using System.Collections;
// required to List
using System.Collections.Generic;

struct RabbitObj {
	// direction of moving: 1 = down, -1 = up
	private int direction;
	private GameObject rabbit;
	// change direction of moving
	public void SetDirection(int _value){
		direction = _value;
	}
	// setting sprite
	public void SetRabbit(GameObject _value){
		rabbit = _value;
	}
	public int GetDirection(){
		return direction;
	}
	public GameObject GetRabbit(){
		return rabbit;
	}
}

public class BunnyMark : MonoBehaviour {

	// constant size: half height of the board
	// it's constant cause it's set to Fit Height for multiresolutions
	const float size = 4.85f;
	// declarations
	GameObject rabbit;
	Text text;
	// storing the result value
	int result = 0;
	// storing the time difference between frames
	float deltaTime = 0.0f;
	// value of increasing
	int increase = 50;
	// this stores clone of rabbit GameObject
	GameObject newRabbit;
	// screen size values
	float height,width,ratio;
	// List that stores all rabbits
	IList<RabbitObj> rabbits = new List<RabbitObj>();

	// Use this for initialization
	void Start () {
		// initialize prepared prefab of rabbit sprite
		rabbit = Resources.Load<GameObject> ("Prefabs/Rabbit");
		// initialize Text component to update the text later
		text = GameObject.Find ("Result").GetComponent<Text> ();
		// get Screen height and width
		height = Screen.height;
		width = Screen.width;
		// count ratio of width and height
		ratio = width / height;
		// multiply ratio by height of the board
		// width range: <-ratio,ratio>
		ratio = ratio * size;
	}

	// it is executed when user click in the canvas area
	// add the next moving rabbits to the scene
	public void addRabbits(){
		// if computer: add 100, if smartphone: add 10
		#if UNITY_STANDALONE
			increase = 50;
		#elif UNITY_ANDROID
			increase = 10;
		#endif
		// update the result value
		result += increase;
		// create the next rabbits from the "rabbit" prefab
		for (int i = 0; i < increase; i++) {
			// clone the rabbit prefab
			newRabbit = Instantiate (rabbit);
			// set random position of the prefab width in range <-ratio,ratio>
			newRabbit.transform.position = new Vector2(Random.Range(-ratio,ratio),Random.Range(-size,size));
			// create new struct object
			RabbitObj ro = new RabbitObj ();
			ro.SetRabbit (newRabbit);
			ro.SetDirection (1);
			// add this struct to list
			rabbits.Add (ro);
		}
		// update text of result on the GUI
		text.text = "Rabbits: " + result;
	}
	
	// Update is called once per frame
	void Update () {
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
		// loop through all of the rabbits in the list
		for (int i = 0; i < rabbits.Count; i++) {
			// get properties of this sprite
			GameObject temp = rabbits [i].GetRabbit ();
			int direction = rabbits [i].GetDirection ();
			// update the position of this sprite
			temp.transform.position = new Vector2(temp.transform.position.x, temp.transform.position.y - 0.07f * (rabbits[i].GetDirection()));
			// change direction of moving if it goes too far
			if (temp.transform.position.y < -size) {
				direction = -1;
				temp.transform.position = new Vector2 (temp.transform.position.x, -size);
			}
			if (temp.transform.position.y > size) {
				direction = 1;
				temp.transform.position = new Vector2 (temp.transform.position.x, size);
			}
			// create new struct object
			RabbitObj ro = new RabbitObj ();
			ro.SetRabbit (temp);
			ro.SetDirection (direction);
			// you must to replace all structure, not just variable
			// because it changes only a copy of structure
			// not the original one
			rabbits [i] = ro;
		}
	}

	// drawing on GUI every frame and executed as the last method
	void OnGUI(){
		// get width and height of the screen
		int w = Screen.width, h = Screen.height;
		// initializing new GUI style
		GUIStyle style = new GUIStyle ();
		// setting position(0,0) and size(w, h * 2 / 40 ) of the visible rectangle
		Rect rect = new Rect (0, 0, w, h * 2 / 40);
		// setting style of the text
		style.alignment = TextAnchor.LowerLeft;
		style.fontSize = h * 2 / 40;
		style.normal.textColor = new Color (255.0f, 255.0f, 255.0f, 1.0f);
		// counting miliseconds
		float msec = deltaTime * 1000.0f;
		// counting frames per second
		float fps = 1.0f / deltaTime;
		// setting the printing format: 16.6 ms (60 fps)
		string text = string.Format ("{0:0.0} ms ({1:0.} fps)", msec, fps);
		GUI.Label (rect, text, style);
	}
}
