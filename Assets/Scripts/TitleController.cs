using UnityEngine;
using System.Collections;

public class TitleController : MonoBehaviour {

	public UnityEngine.UI.Text title;
	public UnityEngine.UI.Text titleShadow;
	public UnityEngine.UI.Text titleLine1;
	public UnityEngine.UI.Text titleLine1Shadow;
	public UnityEngine.UI.Text titleLine2;
	public UnityEngine.UI.Text titleLine2Shadow;
	public UnityEngine.UI.Text titleLine3;
	public UnityEngine.UI.Text titleLine3Shadow;
	public GameObject guitarHit;
	public GameObject guitarHitBig;
	private bool fading;

	// Called on creation
	void Awake () {
		this.title.GetComponent<UnityEngine.UI.Text> ().enabled = false;
		this.titleShadow.GetComponent<UnityEngine.UI.Text> ().enabled = false;
		StartCoroutine (beginAnimation());
		this.fading = false;
	}

	// Called on every frame
	void Update () {
		if (Input.GetKey (KeyCode.UpArrow) && Input.GetKey (KeyCode.DownArrow) && !this.fading) {
			StartCoroutine (fadeOut());
		}
	}

	IEnumerator fadeOut() {
		this.fading = true;
		for(int i = 0; i < 30; i++) {
			Camera.main.backgroundColor = new Color(
				Camera.main.backgroundColor.r + 0.75f / 30.0f, 
				Camera.main.backgroundColor.g + 0.75f / 30.0f,
				Camera.main.backgroundColor.b + 0.75f / 30.0f);
			this.titleLine1Shadow.color = new Color(
				this.titleLine1Shadow.color.r + 1.0f / 30.0f, 
				this.titleLine1Shadow.color.g + 1.0f / 30.0f,
				this.titleLine1Shadow.color.b + 1.0f / 30.0f
				);
			this.titleLine2Shadow.color = new Color(
				this.titleLine2Shadow.color.r + 1.0f / 30.0f, 
				this.titleLine2Shadow.color.g + 1.0f / 30.0f,
				this.titleLine2Shadow.color.b + 1.0f / 30.0f
				);
			this.titleLine3Shadow.color = new Color(
				this.titleLine3Shadow.color.r + 1.0f / 30.0f, 
				this.titleLine3Shadow.color.g + 1.0f / 30.0f,
				this.titleLine3Shadow.color.b + 1.0f / 30.0f
				);
			this.titleShadow.color = new Color(
				this.titleShadow.color.r + 1.0f / 30.0f, 
				this.titleShadow.color.g + 1.0f / 30.0f,
				this.titleShadow.color.b + 1.0f / 30.0f
				);
			yield return new WaitForSeconds(1.0f / 60.0f);
		}
		Application.LoadLevel(1);
	}

	IEnumerator beginAnimation() {
		for (int i = 0; i < 15; i++) {
			this.titleLine1.transform.localPosition = new Vector3(
				-600 + (600.0f / 15.0f) * (i + 1),
				titleLine1.transform.localPosition.y,
				titleLine1.transform.localPosition.z
				);
			this.titleLine1Shadow.transform.localPosition = new Vector3(
				-600 + (600.0f / 15.0f) * (i + 1),
				titleLine1Shadow.transform.localPosition.y,
				titleLine1Shadow.transform.localPosition.z
				);
			yield return new WaitForSeconds(1.0f / 60.0f);
		}
		this.guitarHit.GetComponent<AudioSource> ().PlayOneShot (this.guitarHit.GetComponent<AudioSource> ().clip);
		yield return new WaitForSeconds(0.2f);
		for(int i = 0; i < 15; i++) {
			this.titleLine2.transform.localPosition = new Vector3(
				600 - (600.0f / 15.0f) * (i + 1),
				titleLine2.transform.localPosition.y,
				titleLine2.transform.localPosition.z
				);
			this.titleLine2Shadow.transform.localPosition = new Vector3(
				600 - (600.0f / 15.0f) * (i + 1),
				titleLine2Shadow.transform.localPosition.y,
				titleLine2Shadow.transform.localPosition.z
				);
			yield return new WaitForSeconds(1.0f / 60.0f);
		}
		this.guitarHit.GetComponent<AudioSource> ().PlayOneShot (this.guitarHit.GetComponent<AudioSource> ().clip);
		yield return new WaitForSeconds (0.2f);
		for (int i = 0; i < 15; i++) {
			this.titleLine3.transform.localPosition = new Vector3(
				-600 + (600.0f / 15.0f) * (i + 1),
				titleLine3.transform.localPosition.y,
				titleLine3.transform.localPosition.z
				);
			this.titleLine3Shadow.transform.localPosition = new Vector3(
				-600 + (600.0f / 15.0f) * (i + 1),
				titleLine3Shadow.transform.localPosition.y,
				titleLine3Shadow.transform.localPosition.z
				);
			yield return new WaitForSeconds(1.0f / 60.0f);
		}
		this.guitarHit.GetComponent<AudioSource> ().PlayOneShot (this.guitarHit.GetComponent<AudioSource> ().clip);
		yield return new WaitForSeconds(0.1f);
		for(int i = 0; i < 5; i++) {
			titleLine1.GetComponent<UnityEngine.UI.Text>().fontSize += 5;
			titleLine2.GetComponent<UnityEngine.UI.Text>().fontSize += 5;
			titleLine3.GetComponent<UnityEngine.UI.Text>().fontSize += 5;
			titleLine1Shadow.GetComponent<UnityEngine.UI.Text>().fontSize += 5;
			titleLine2Shadow.GetComponent<UnityEngine.UI.Text>().fontSize += 5;
			titleLine3Shadow.GetComponent<UnityEngine.UI.Text>().fontSize += 5;
			yield return new WaitForSeconds(1.0f / 60.0f);
		}
		for(int i = 0; i < 5; i++) {
			titleLine1.GetComponent<UnityEngine.UI.Text>().fontSize -= 5;
			titleLine2.GetComponent<UnityEngine.UI.Text>().fontSize -= 5;
			titleLine3.GetComponent<UnityEngine.UI.Text>().fontSize -= 5;
			titleLine1Shadow.GetComponent<UnityEngine.UI.Text>().fontSize -= 5;
			titleLine2Shadow.GetComponent<UnityEngine.UI.Text>().fontSize -= 5;
			titleLine3Shadow.GetComponent<UnityEngine.UI.Text>().fontSize -= 5;
			yield return new WaitForSeconds(1.0f / 60.0f);
		}
		titleLine1.GetComponent<UnityEngine.UI.Text> ().enabled = false;
		titleLine2.GetComponent<UnityEngine.UI.Text>().enabled = false;
		titleLine3.GetComponent<UnityEngine.UI.Text>().enabled = false;
		titleLine1Shadow.GetComponent<UnityEngine.UI.Text>().enabled = false;
		titleLine2Shadow.GetComponent<UnityEngine.UI.Text>().enabled = false;
		titleLine3Shadow.GetComponent<UnityEngine.UI.Text>().enabled = false;

		this.title.GetComponent<TextGrowShrink> ().startAnim ();
		this.titleShadow.GetComponent<TextGrowShrink> ().startAnim ();
		this.title.GetComponent<UnityEngine.UI.Text> ().enabled = true;
		this.titleShadow.GetComponent<UnityEngine.UI.Text> ().enabled = true;
		this.guitarHitBig.GetComponent<AudioSource> ().PlayOneShot (this.guitarHitBig.GetComponent<AudioSource> ().clip);
	}
}
