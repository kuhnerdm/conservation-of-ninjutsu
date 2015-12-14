using UnityEngine;
using System.Collections;

public class PegToIndicator : MonoBehaviour {

	public GameObject indicator;

	void Awake() {
		gameObject.transform.position = new Vector3 (10, 10, 0);
	}

	// Called every frame
	void Update () {
		gameObject.transform.position = this.indicator.transform.position;
	}
}
