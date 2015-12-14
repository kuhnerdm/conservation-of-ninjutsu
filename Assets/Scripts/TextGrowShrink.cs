using UnityEngine;
using System.Collections;

public class TextGrowShrink : MonoBehaviour {

	// State info
	private bool started;
	private float size;
	private bool growing;
	private int timerGrow;

	public void startAnim() {
		this.started = true;
	}

	// Called on creation
	void Start () {
		this.started = true;
		this.size = 0.90f;
		this.growing = false;
		this.timerGrow = 60;
	}
	
	// Called on every frame
	void Update () {
		if (this.started) {
			this.timerGrow--;
			if(this.growing) {
				this.size += 0.1f / 60.0f;
			} else {
				this.size -= 0.1f / 60.0f;
			}
			if(this.timerGrow == 0) {
				this.growing = !this.growing;
				this.timerGrow = 60;
			}
			gameObject.transform.localScale = new Vector3(this.size, this.size, 0);
		}
	}
}
