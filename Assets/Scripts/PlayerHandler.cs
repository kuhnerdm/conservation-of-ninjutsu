using UnityEngine;
using System.Collections;

public class PlayerHandler : MonoBehaviour {

	private int lives;
	public GameObject lifeText; // ref to life text on screen

	// Called on creation
	void Start() {
		this.lives = 1;
	}

	// Removes a life; returns false if game over
	public bool die() {
		this.lives--;
		this.lifeText.GetComponent<ShowLives> ().updateLives (this.lives);
		if (this.lives == 0) {
			return false;
		} 
		return true;
	}

	// Adds an extra life
	public void addLife() {
		this.lives++;
		this.lifeText.GetComponent<ShowLives> ().updateLives (this.lives);
	}
}
