using UnityEngine;
// required to UI elements
using UnityEngine.UI;
using System.Collections;

public class BunnyMark : MonoBehaviour {

	GameObject rabbit;
	Text text;

	// storing the result value
	int result = 0;

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
		
		// update the result
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
	
	}
}
