using UnityEngine;
using System.Collections;

public class GameOverText : MonoBehaviour {

	// Makes the text move in this pattern:
	// * Turns left while growing
	// * Turns right while shrinking
	// * Turns right while growing
	// * Turns left while shrinking
	// * Repeat

	// State info
	private bool started;
	private float size;
	private bool movingLeft;
	private bool growing;
	private int timerGrow;
	private int timerTurn;

	// Called on creation; initializes
	void Start () {
		this.started = false;
		this.growing = true;
		this.size = 0.0f;
		this.movingLeft = true;
		this.timerGrow = 50;
		this.timerTurn = 50;
	}
	
	// Called every frame
	void Update () {

		if (started) { // Size zero unless started

			// Handle growing/shrinking
			if(growing) {
				if(timerGrow > 0) {
					this.size += 0.04f;
					timerGrow--;
				} else {
					timerGrow = 50;
					growing = false;
				}
			} else {
				if(timerGrow > 0) {
					this.size -= 0.04f;
					timerGrow--;
				} else {
					timerGrow = 50;
					growing = true;
				}
			}

			// Handle turning

			if(movingLeft) {
				if(timerTurn > 0) {
					this.transform.Rotate(new Vector3(0.0f, 0.0f, 0.2f));
					timerTurn--;
				} else {
					timerTurn = 100;
					movingLeft = false;
				}
			} else {
				if(timerTurn > 0) {
					this.transform.Rotate(new Vector3(0.0f, 0.0f, -0.2f));
					timerTurn--;
				} else {
					timerTurn = 100;
					movingLeft = true;
				}
			}
		}

		// Apply size
		this.GetComponent<UnityEngine.UI.Text> ().fontSize = (int) (this.size * 36);

		if (this.size == 0) { // Handles weird cases
			this.GetComponent<UnityEngine.UI.Text> ().text = "";
		} else {
			this.GetComponent<UnityEngine.UI.Text>().text = "GAME OVER!";
		}
	}

	// Starts the pattern
	public void gameOver() {
		this.started = true;
	}
}
