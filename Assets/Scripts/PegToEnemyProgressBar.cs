using UnityEngine;
using System.Collections;

public class PegToEnemyProgressBar : MonoBehaviour {

	// Different from normal pegToEnemy because this one doesn't change scale

	public GameObject enemy;
	
	// Called on every frame
	void Update () {
		Vector3 newPos = gameObject.transform.position;
		newPos.x = enemy.transform.position.x;
		newPos.y = -27;
		gameObject.transform.position = newPos;
	}
}
