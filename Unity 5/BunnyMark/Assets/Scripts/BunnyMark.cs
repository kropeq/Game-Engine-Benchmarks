using UnityEngine;
// required to UI elements
using UnityEngine.UI;
using System.Collections;

public class BunnyMark : MonoBehaviour {

	GameObject rabbit;
	Text text;
	// storing the result value
	int result = 0;
	// storing the time difference between frames
	float deltaTime = 0.0f;

	// Use this for initialization
	void Start () {
		// initialize prepared prefab of rabbit sprite
		rabbit = Resources.Load<GameObject> ("Prefabs/Rabbit");
		// initialize Text component to update the text later
		text = GameObject.Find ("Result").GetComponent<Text> ();
	}

	// it is executed when user click in the canvas area
	// add 25 moving rabbits to the scene
	public void addRabbits(){
		// update the result value
		result += 25;
		// create 25 rabbits from the "rabbit" prefab
		for (int i = 0; i < 25; i++) {
			Instantiate (rabbit);
		}
		// update text of result on the GUI
		text.text = "Rabbits: " + result;
	}
	
	// Update is called once per frame
	void Update () {
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
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
