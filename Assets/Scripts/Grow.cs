using UnityEngine;
using System.Collections;

public class Grow : MonoBehaviour {

	// For progress bar

	public float waitTime;
	public GameObject myInd;
	public bool growing = false;

	// Called every frame
	void Update () {
		if (gameObject != null && this.growing) { // If not destroyed, and if growing

			//Increase scale
			Vector3 newScale = gameObject.transform.localScale;
			newScale.x += (myInd.transform.localScale.x) / (waitTime * 60);
			gameObject.transform.localScale = newScale;
		}
	}

	// Starts the growth hnnnnnnnng
	public void startGrowing() {
		this.growing = true;
	}
}
