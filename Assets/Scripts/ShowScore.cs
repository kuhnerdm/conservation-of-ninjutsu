using UnityEngine;
using System.Collections;

public class ShowScore : MonoBehaviour {

	// Called on creation
	void Start () {
		gameObject.GetComponent<UnityEngine.UI.Text> ().text = "Score: 0";
	}

	// Called on every frame
	public void updateScore(int score) {
		gameObject.GetComponent<UnityEngine.UI.Text> ().text = "Score: " + score;
	}
}
