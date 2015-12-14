using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	//Template for instantiation
	public GameObject exampleEnemy;

	//Enemies in play
	private GameObject currentEnemy;
	private GameObject nextEnemy;

	//Player object
	public GameObject player;

	//Stat info
	private int score;
	private int numAttacks;
	private const int SCORE_BEFORE_NEW_ATTACK = 10;
	private const int SCORE_BEFORE_NEW_LIFE = 25;

	//State info
	private bool dead;
	private bool doingRound = false;
	private bool alreadySaidGameOver = false;
	private bool started;
	private int timer;
	private int deathTimer;
	private bool fading;

	//Text objects
	public GameObject gameOverText;
	public GameObject gameOverShadow;
	public GameObject scoreText;
	public GameObject restartText;
	public GameObject livesText;

	//Audio
	public GameObject swordSwipeSound;
	public GameObject bgm;
	public GameObject deathSound;

	//Other
	public UnityEngine.Sprite nothingSprite;
	public GameObject bg;

	// Called on creation; 
	void Awake () {
		this.started = false;
		this.timer = 240;
		this.deathTimer = 60;
		this.restartText.GetComponent<UnityEngine.UI.Text> ().text = "";
		StartCoroutine (fadeIn ());
		this.fading = false;
	}

	// Sets up first two enemies
	private void initiate() {
		this.currentEnemy = Instantiate (exampleEnemy);
		this.numAttacks = 1;
		this.currentEnemy.GetComponent<HandleCreationOfAttacks> ().setAttacks (numAttacks);
		this.currentEnemy.GetComponent<RunToLeft> ().runUp ();
		this.nextEnemy = Instantiate (exampleEnemy);
		this.nextEnemy.GetComponent<HandleCreationOfAttacks> ().setAttacks (numAttacks);
	}

	// Called on enemy death; switches next to current and makes new one
	private void switchEnemies() {
		this.currentEnemy.GetComponent<HandleCreationOfAttacks> ().destroyMe ();
		this.currentEnemy = this.nextEnemy;
		this.currentEnemy.GetComponent<RunToLeft> ().runUp ();
		this.nextEnemy = Instantiate (this.exampleEnemy);
		this.nextEnemy.GetComponent<HandleCreationOfAttacks> ().setAttacks (numAttacks);
	}

	// Handles one enemy attack
	IEnumerator doRound() {
		this.doingRound = true; // Allows for pseudo-looping of rounds

		// Get enemy attack
		string currentAttack = this.currentEnemy.GetComponent<HandleCreationOfAttacks> ().nextAttack();
		bool enemyDead = false;
		if (currentAttack == "noattack") { // if no more attacks
			this.currentEnemy.GetComponent<Animator>().SetTrigger("EnemyDeath");
			this.score++;
			this.scoreText.GetComponent<ShowScore>().updateScore(this.score);
			if((this.score + 1) % GameController.SCORE_BEFORE_NEW_ATTACK == 0) {
				this.numAttacks++;
			}
			if((this.score + 1) % GameController.SCORE_BEFORE_NEW_LIFE == 0) {
				this.player.GetComponent<PlayerHandler>().addLife();
			}
			this.switchEnemies ();
			enemyDead = true;
		} // else attack proceeded correctly to next one; we have currentAttack

		// Get player input
		bool stillLiving = true;
		while (!enemyDead) {

			// Set up time to wait
			float waitTime = 2.5f / (1.0f + 0.1f * this.score);
			if(waitTime < 0.25f) {
				waitTime = 0.25f; // Clamp for difficulty reasons
			}

			this.currentEnemy.GetComponent<HandleCreationOfAttacks>().setWaitTime(waitTime); // Tell the enemy how long we're waiting
			this.currentEnemy.GetComponent<HandleCreationOfAttacks>().growProgBar();
			yield return new WaitForSeconds(waitTime); // Sleep thread
			this.swordSwipeSound.GetComponent<AudioSource>().PlayOneShot(this.swordSwipeSound.GetComponent<AudioSource>().clip);
			this.currentEnemy.GetComponent<HandleCreationOfAttacks>().makeNewProgBar();
			string playerResponse;

			// Actually pick up physical inputs
			bool right = Input.GetKey(KeyCode.RightArrow);
			bool left = Input.GetKey(KeyCode.LeftArrow);
			bool up = Input.GetKey(KeyCode.UpArrow);
			bool down = Input.GetKey(KeyCode.DownArrow);
			bool none = !right && !left && !up && !down;

			// Interpret inputs
			if(right && left && !up & !down) {
				playerResponse = "hori";
			}
			else if(up && down && !right && !left) {
				playerResponse = "vert";
			}
			else if(none) {
				playerResponse = "dodge";
			}
			else {
				playerResponse = ""; // invalid; auto-lose
			}

			// Compare player input to enemy attack
			bool playerWon;
			if (currentAttack.Equals ("slam") && playerResponse.Equals ("hori")) {
				playerWon = true;
			} else if (currentAttack.Equals ("slash") && playerResponse.Equals ("vert")) {
				playerWon = true;
			} else if (currentAttack.Equals("stab") && playerResponse.Equals ("dodge")) {
				playerWon = true;
			}
			else { // wrong input
				playerWon = false;
			}

			// Take correct action
			if(!playerWon) {
				this.currentEnemy.GetComponent<Animator>().SetTrigger("IdleToAttack");
				stillLiving = this.player.GetComponent<PlayerHandler>().die(); // Remove one life
				if(stillLiving) { // i.e. if lives != 0
					continue;
				} else {
					break;
				}
			} else {
				this.player.GetComponent<Animator>().SetTrigger("attacktrigger"); // Play player attack animation
				// TODO: Enemy hit animation

				// Remove attack from enemy
				this.currentEnemy.GetComponent<HandleCreationOfAttacks>().removeAttack();
				this.currentEnemy.GetComponent<HandleCreationOfAttacks>().destroySymbols();
				this.currentEnemy.GetComponent<HandleCreationOfAttacks>().updateIndicator();
				break;
			}
		}

		// Executes on either gameover or the enemy losing an attack
		this.dead = !stillLiving;
		this.doingRound = false;
	}

	void Update() { // Called once per frame
		if (this.started) { // If we're past the starting pause
			if (this.alreadySaidGameOver == false) { // Stops from repeatedly calling gameover stuff
				if (doingRound == false && !this.dead) { // If not currently in round
					StartCoroutine (doRound ());
				} else if (this.dead) { // If gameover
					this.bgm.GetComponent<AudioSource> ().Stop (); // Stop bgm
					this.deathSound.GetComponent<AudioSource> ().PlayOneShot (this.deathSound.GetComponent<AudioSource> ().clip); // Play death sound
					this.player.GetComponent<Animator>().SetTrigger("PlayerDeath");
					this.player.GetComponent<SpriteRenderer>().sprite = this.nothingSprite;
					this.alreadySaidGameOver = true; 
					this.gameOverText.GetComponent<GameOverText> ().gameOver ();
					this.gameOverShadow.GetComponent<GameOverText> ().gameOver (); // Start game over text loop
					this.restartText.GetComponent<UnityEngine.UI.Text>().text = "Press the up and down arrows to restart!";
				}
			}
		} else {
			this.timer--;
			if(this.timer < 0) {
				this.started = true;
				this.initiate();
			}
		}
		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit();
		}
		if (this.dead) {
			if(this.deathTimer != 0) {
				this.deathTimer--;
			} else {
				if(Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow) && !fading) {
					StartCoroutine(fadeOut());
				}
			}
		}
	}

	IEnumerator fadeIn() {
		for (int i = 0; i < 30; i++) {
			Camera.main.backgroundColor = new Color(
				Camera.main.backgroundColor.r - 0.75f / 30.0f, 
				Camera.main.backgroundColor.g - 0.75f / 30.0f,
				Camera.main.backgroundColor.b - 0.75f / 30.0f);
			player.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, player.GetComponent<SpriteRenderer>().color.a + 1.0f / 30.0f);
			bg.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, bg.GetComponent<SpriteRenderer>().color.a + 1.0f / 30.0f);
			livesText.GetComponent<UnityEngine.UI.Text>().color = new Color(0.0f, 0.0f, 0.0f, livesText.GetComponent<UnityEngine.UI.Text>().color.a + 1.0f / 30.0f);
			scoreText.GetComponent<UnityEngine.UI.Text>().color = new Color(0.0f, 0.0f, 0.0f, livesText.GetComponent<UnityEngine.UI.Text>().color.a + 1.0f / 30.0f);
			restartText.GetComponent<UnityEngine.UI.Text>().color = new Color(0.0f, 0.0f, 0.0f, livesText.GetComponent<UnityEngine.UI.Text>().color.a + 1.0f / 30.0f);
			yield return new WaitForSeconds(1.0f / 60.0f);
		}
	}

	IEnumerator fadeOut() {
		this.fading = true;
		for (int i = 0; i < 30; i++) {
			Camera.main.backgroundColor = new Color(
				Camera.main.backgroundColor.r + 0.75f / 30.0f, 
				Camera.main.backgroundColor.g + 0.75f / 30.0f,
				Camera.main.backgroundColor.b + 0.75f / 30.0f);
			player.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, player.GetComponent<SpriteRenderer>().color.a - 1.0f / 30.0f);
			this.currentEnemy.GetComponent<HandleCreationOfAttacks>().fadeOut();
			this.nextEnemy.GetComponent<HandleCreationOfAttacks>().fadeOut();
			bg.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, bg.GetComponent<SpriteRenderer>().color.a - 1.0f / 30.0f);
			livesText.GetComponent<UnityEngine.UI.Text>().color = new Color(0.0f, 0.0f, 0.0f, livesText.GetComponent<UnityEngine.UI.Text>().color.a - 1.0f / 30.0f);
			scoreText.GetComponent<UnityEngine.UI.Text>().color = new Color(0.0f, 0.0f, 0.0f, livesText.GetComponent<UnityEngine.UI.Text>().color.a - 1.0f / 30.0f);
			restartText.GetComponent<UnityEngine.UI.Text>().color = new Color(0.0f, 0.0f, 0.0f, livesText.GetComponent<UnityEngine.UI.Text>().color.a - 1.0f / 30.0f);
			yield return new WaitForSeconds(1.0f / 60.0f);
		}
		Application.LoadLevel(Application.loadedLevel);
	}

}
