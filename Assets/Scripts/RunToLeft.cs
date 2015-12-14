using UnityEngine;
using System.Collections;

public class RunToLeft : MonoBehaviour {

	// TODO: Make this not frame-based

	private int runTimer; // How many frames to keep running

	// Called on creation; initializes
	void Awake () {
		Vector3 newPos = gameObject.transform.position;
		newPos.x = 125.0f;
		gameObject.transform.position = newPos;
		this.runTimer = 25; // Run in from offscreen
	}
	
	// Called on every frame; moves to the left
	void Update () {
		if (this.runTimer > 0) {
			Vector3 newPos = gameObject.transform.position;
			newPos.x--;
			gameObject.transform.position = newPos;
			this.runTimer--;
		}
	}

	// Starts running to center of screen
	public void runUp() {
		this.gameObject.GetComponent<Animator> ().SetTrigger ("IdleToRun");
		this.runTimer += 50;
	}
}
