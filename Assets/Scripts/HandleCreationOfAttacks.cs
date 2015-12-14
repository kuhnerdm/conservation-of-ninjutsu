using UnityEngine;
using System.Collections;

public class HandleCreationOfAttacks : MonoBehaviour {

	// TODO: Change this script's name and references

	// Info about attacks
	private int numAttacks;
	private string[] attacks;

	// Not used anymore; TODO: remove references to these
	public GameObject indText;
	private UnityEngine.UI.Text myText;
	private GameObject myCanvas;

	// Templates for instantiation
	public GameObject exampleIndicator;
	public GameObject exampleCanvas;
	public GameObject exampleProgBar;
	public GameObject exampleHoriArrow;
	public GameObject exampleVertArrow;
	public GameObject exampleCircle;
	public GameObject exampleSymbolTemplate;

	// Refs to graphics
	private GameObject myInd;
	private GameObject myProgBar;
	private GameObject[] mySymbols;
	private GameObject mySymbolTemplate;

	// Called on creation
	void Awake() {

		//Set up canvas and indicator
		myInd = Instantiate (exampleIndicator);
		myProgBar = Instantiate (exampleProgBar);
		myInd.GetComponent<PegToEnemy> ().enemy = gameObject;
		myProgBar.GetComponent<PegToEnemyProgressBar> ().enemy = gameObject;
		myProgBar.GetComponent<Grow> ().myInd = myInd;
		myCanvas = Instantiate (exampleCanvas);
		mySymbols = new GameObject[0];

		//Set up text component; Not used anymore; TODO: Remove references to these
		this.indText = new GameObject ();
		this.indText.transform.SetParent (myCanvas.transform);
		this.myText = this.indText.AddComponent<UnityEngine.UI.Text> ();
		this.myText.canvas.worldCamera = Camera.main;
		this.indText.transform.localScale = new Vector3 (1, 1, 1);
		this.myText.fontSize = 30;
		this.myText.horizontalOverflow = HorizontalWrapMode.Overflow;
		this.myText.font = Resources.GetBuiltinResource<Font> ("Arial.ttf");
		this.myText.alignment = TextAnchor.MiddleCenter;
		this.myText.color = Color.black;

		//Set up scripts
		this.indText.AddComponent<PegToIndicator> ();
		this.indText.AddComponent<IndicatorUpdate> ();
		this.indText.GetComponent<PegToIndicator> ().indicator = myInd;

	}

	// Called on creation; randomly generates attacks and fills array
	public void setAttacks(int num) {

		// Initialize attack array
		this.numAttacks = num;
		this.myInd.GetComponent<PegToEnemy> ().numAttacks = num;
		this.attacks = new string[this.numAttacks];

		// Fill array with attacks
		for(int i = 0; i < this.numAttacks; i++) {
			float randomVal = Random.value * 3.0f;
			if(randomVal < 1.0f) {
				attacks[i] = "slam";
			}
			else if(randomVal < 2.0f) {
				attacks[i] = "slash";
			}
			else {
				attacks[i] = "stab";
			}
		}

		// Update indicator to be safe
		this.destroySymbols ();
		this.updateIndicator ();
	}

	// Sets up indicator sprites
	public void updateIndicator() {

		// Make folder/parent
		this.mySymbolTemplate = Instantiate (this.exampleSymbolTemplate);
		this.mySymbolTemplate.GetComponent<PegToIndicator> ().indicator = myInd;

		// Make array of symbols
		this.mySymbols = new GameObject[this.attacks.Length];

		// Loop through attack list
		for (int i = 0; i < this.attacks.Length; i++) {

			// Decide which sprite to use
			if (this.attacks [i].Equals ("slam")) {
				this.mySymbols [i] = Instantiate (exampleHoriArrow);
			} else if (this.attacks [i].Equals ("slash")) {
				this.mySymbols [i] = Instantiate (exampleVertArrow);
			} else {
				this.mySymbols [i] = Instantiate (exampleCircle);
			}

			// Add to parent/folder
			this.mySymbols [i].transform.SetParent (mySymbolTemplate.transform);

			// Move back to base position from offscreen
			mySymbols[i].transform.localPosition = new Vector3(0, 0, 0);

			// Generate new position; TODO: needs some work; kinda hacky
			float newX = 0.0f;
			if (this.attacks.Length % 2 != 0) {
				newX = mySymbols[i].transform.localPosition.x - ((this.attacks.Length - 1) / 2);
				newX += 0.5f * i;
				newX += 0.5f * (this.attacks.Length / 2);
			} else {
				newX = mySymbols[i].transform.localPosition.x - 0.25f - ((this.attacks.Length - 2) / 4);
				newX += 0.5f * i;
				newX -= 0.5f * ((this.attacks.Length - 2) / 2);
				if(this.attacks.Length >= 6) {
					newX += 0.5f * ((this.attacks.Length - 2) / 2);
				}
			}

			// Assign new position
			mySymbols[i].transform.localPosition = new Vector3(
				newX,
				mySymbols[i].transform.localPosition.y,
				mySymbols[i].transform.localPosition.z
				);
		}

	}

	// Called before updateIndicator(); TODO: Incorporate this into updateIndicator()
	public void destroySymbols() {

		// Safety case
		if(this.mySymbols.Length == 0) {
			return;
		}

		// Loop through symbols
		for(int i = 0; i < this.mySymbols.Length; i++) {
			Destroy(this.mySymbols[i].transform.parent.gameObject); // Get rid of parent/folder too
			Destroy(this.mySymbols[i]);
		}
	}

	// Gets next attack
	public string nextAttack() {
		if (this.numAttacks >= 1) {
			return this.attacks[0];
		} else {
			return "noattack";
		}
	}

	// Removes first attack from array
	public void removeAttack() {
		string[] newAttacks = new string[this.numAttacks - 1];
		for (int i = 1; i < this.numAttacks; i++) {
			newAttacks [i - 1] = this.attacks [i];
		}
		this.attacks = newAttacks;
		this.numAttacks--;
	}

	// Safely destroys this and its references that we don't need
	public void destroyMe() {
		Destroy (this.indText);
		Destroy (this.myCanvas);
		Destroy (this.myInd);
		Destroy (this.myProgBar);
		for (int i = 0; i < this.numAttacks; i++) {
			Destroy (this.mySymbols[i]);
		}
		Destroy (this.mySymbolTemplate);
		Destroy (gameObject);
	}

	// Passes on wait time to prog bar object
	public void setWaitTime(float time) {
		this.myProgBar.GetComponent<Grow> ().waitTime = time;
	}

	// Generates new prog bar
	public void makeNewProgBar() {
		Destroy (myProgBar);
		myProgBar = Instantiate (exampleProgBar);
		myProgBar.GetComponent<PegToEnemyProgressBar> ().enemy = gameObject;
		myProgBar.GetComponent<Grow> ().myInd = myInd;
	}

	// Starts prog bar growing (this happens independently from waiting for player input, so it's a little janky)
	public void growProgBar() {
		myProgBar.GetComponent<Grow> ().startGrowing ();
	}

	IEnumerator startFade() {
		for (int i = 0; i < 30; i++) {
			gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, gameObject.GetComponent<SpriteRenderer>().color.a - 1.0f / 30.0f);
			this.myInd.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, this.myInd.GetComponent<SpriteRenderer>().color.a - 1.0f / 30.0f);
			for(int j = 0; j < this.mySymbols.Length; j++) {
				this.mySymbols[j].GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, this.mySymbols[j].GetComponent<SpriteRenderer>().color.a - 1.0f / 30.0f);
			}
			yield return new WaitForSeconds(1.0f / 60.0f);
		}
	}

	public void fadeOut() {
		StartCoroutine (startFade ());
	}

}
