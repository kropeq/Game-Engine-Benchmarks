using UnityEngine;
using System.Collections;

public class Moves : MonoBehaviour {

	// constant size: half height of the board
	// it's constant cause it's set to Fit Height for multiresolutions
	const float size = 4.85f;
	// direction of moving: 1 = down, -1 = up
	int direction = 1;
	// screen size values
	float height,width,ratio;

	// Use this for initialization
	void Start(){
		// get Screen height and width
		height = Screen.height;
		width = Screen.width;
		// count ratio of width and height
		ratio = width / height;
		// multiply ratio by height of the board
		// width range: <-ratio,ratio>
		ratio = ratio * size;
		// set random position of the prefab width in range <-ratio,ratio>
		this.transform.position = new Vector2(Random.Range(-ratio,ratio),Random.Range(-size,size));
	}

	// Update is called once per frame
	void Update () {
		// update position of this rabbit
		this.transform.position = new Vector2 (this.transform.position.x, this.transform.position.y - 0.07f * direction);
		// if rabbit is going out of the screen, change direction of moving
		if (this.transform.position.y < -size) {
			direction = -1;
			this.transform.position = new Vector2(this.transform.position.x, -size);
		}
		// if rabbit is going out of the screen, change direction of moving
		if (this.transform.position.y > size) {
			direction = 1;
			this.transform.position = new Vector2(this.transform.position.x, size);
		}
	}
}
