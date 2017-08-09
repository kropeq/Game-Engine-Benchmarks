using UnityEngine;
using System.Collections;

public class Moves : MonoBehaviour {

	// direction of moving: 1 = down, -1 = up
	int direction = 1;

	// Update is called once per frame
	void Update () {
		// update position of this rabbit
		this.transform.position = new Vector2 (this.transform.position.x, this.transform.position.y - 0.07f * direction);

		// if rabbit is going out of the screen, change direction of moving
		if (this.transform.position.y < -4.85f) {
			direction = -1;
			this.transform.position = new Vector2(this.transform.position.x, -4.85f);
		}

		// if rabbit is going out of the screen, change direction of moving
		if (this.transform.position.y > 4.85f) {
			direction = 1;
			this.transform.position = new Vector2(this.transform.position.x, 4.85f);
		}
	}
}
