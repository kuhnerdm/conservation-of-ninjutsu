using UnityEngine;
using System.Collections;

public class ShowLives : MonoBehaviour {

	// Called on creation
	void Start () {
		gameObject.GetComponent<UnityEngine.UI.Text> ().text = "Lives: 1";
	}

	// Called on every frame
	public void updateLives(int lives) {
		gameObject.GetComponent<UnityEngine.UI.Text> ().text = "Lives: " + lives;
	}
}
