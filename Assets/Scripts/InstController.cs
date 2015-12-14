using UnityEngine;
using System.Collections;

public class InstController : MonoBehaviour {

	private bool fading;
	public UnityEngine.SpriteRenderer sprite1;
	public UnityEngine.SpriteRenderer sprite2;
	public UnityEngine.SpriteRenderer sprite3;

	// Use this for initialization
	void Start () {
		StartCoroutine (beginAnimation());
		this.fading = false;
	}
	
	// Called on every frame
	void Update () {
		if (Input.GetKey (KeyCode.UpArrow) && Input.GetKey (KeyCode.DownArrow) && !this.fading) {
			StartCoroutine (fadeOut());
		}
	}

	IEnumerator beginAnimation() {
		this.fading = true;
		for (int i = 0; i < 30; i++) {
			Camera.main.backgroundColor = new Color(
				Camera.main.backgroundColor.r - 0.75f / 30.0f, 
				Camera.main.backgroundColor.g - 0.75f / 30.0f,
				Camera.main.backgroundColor.b - 0.75f / 30.0f);
			sprite1.color = new Color(1.0f, 1.0f, 1.0f, sprite1.color.a + 1.0f / 30.0f);
			sprite2.color = new Color(1.0f, 1.0f, 1.0f, sprite2.color.a + 1.0f / 30.0f);
			sprite3.color = new Color(1.0f, 1.0f, 1.0f, sprite3.color.a + 1.0f / 30.0f);
			yield return new WaitForSeconds(1.0f / 60.0f);
		}
		this.fading = false;
	}

	IEnumerator fadeOut() {
		this.fading = true;
		for (int i = 0; i < 30; i++) {
			Camera.main.backgroundColor = new Color(
				Camera.main.backgroundColor.r + 0.75f / 30.0f, 
				Camera.main.backgroundColor.g + 0.75f / 30.0f,
				Camera.main.backgroundColor.b + 0.75f / 30.0f);
			sprite1.color = new Color(1.0f, 1.0f, 1.0f, sprite1.color.a - 1.0f / 30.0f);
			sprite2.color = new Color(1.0f, 1.0f, 1.0f, sprite2.color.a - 1.0f / 30.0f);
			sprite3.color = new Color(1.0f, 1.0f, 1.0f, sprite3.color.a - 1.0f / 30.0f);
			yield return new WaitForSeconds(1.0f / 60.0f);
		}
		Application.LoadLevel (2);
	}
}
