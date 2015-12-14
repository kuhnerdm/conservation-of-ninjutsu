using UnityEngine;
using System.Collections;

public class PegToEnemy : MonoBehaviour {

	// Used for indicator bar, NOT progress bar

	public GameObject enemy;
	public int numAttacks;

	void Awake() {
		gameObject.GetComponent<SpriteRenderer> ().enabled = false;
	}

	// Called on every frame
	void Update () {
		Vector3 newPos = gameObject.transform.position;
		newPos.x = enemy.transform.position.x;
		gameObject.transform.position = newPos;

		// Scale to num of attacks
		Vector3 newScale = gameObject.transform.localScale;
		newScale.x = 50 * numAttacks;
		gameObject.transform.localScale = newScale;

		gameObject.GetComponent<SpriteRenderer> ().enabled = true;
	}
}
