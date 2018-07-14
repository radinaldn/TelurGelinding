using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

	// property
	public float speed;

	public Vector3 dir;

	public GameObject ps;

	private bool isDead = false;

	public GameObject resetBtn;

	private int score = 0;

	public Text scoreText;

	public Animator gameOverAnim;

	public Text lastScoreText;

	public Text bestText;

	public Text newHighScore;

	public Image background;

	public LayerMask whatIsGround;

	private bool isPlaying = false;

	public Transform contactPoin;

	// Use this for initialization
	void Start () {
		// move player while click a button
		dir = Vector3.zero;

	}
	
	// Update is called once per frame
	void Update () {

		// jika player tidak di tanah, bunuh player
		if (!IsGrounded() && isPlaying) {
			isDead = true;

			GameOver ();

			resetBtn.SetActive (true);

			if (transform.childCount > 0) {
				transform.GetChild (0).transform.parent = null;
			}
		}

		if (Input.GetMouseButtonDown (0) && !isDead) {

			isPlaying = true;

			// scoring
			score++;
			scoreText.text = score.ToString();

			if (dir == Vector3.forward) {
				dir = Vector3.left;
			} else {
				dir = Vector3.forward;
			}
		}
		float amoutToMove = speed * Time.deltaTime;

		transform.Translate (dir * amoutToMove);
	}

	// ketika player nabrak pickup, pickup menghilang
	void OnTriggerEnter(Collider other){
		if (other.tag == "Pickup") {
			other.gameObject.SetActive (false);
			Instantiate (ps, transform.position, Quaternion.identity);
			// scoring when hit pickup
			score+=3;
			scoreText.text = score.ToString();
		}
	}


	private void GameOver(){
		gameOverAnim.SetTrigger ("GameOver");
		lastScoreText.text = score.ToString ();

		// best score
		int bestScore = PlayerPrefs.GetInt("BestScore", 0);

		if (score > bestScore) {
			PlayerPrefs.SetInt ("BestScore", score);
			newHighScore.gameObject.SetActive (true);

			bestText.color = new Color32 (193,10,181,255);
			newHighScore.color = new Color32 (193,10,181,255);

		}

		bestText.text = PlayerPrefs.GetInt ("BestScore", 0).ToString();
	}

	private bool IsGrounded(){
		Collider[] colliders = Physics.OverlapSphere (contactPoin.position, .5f, whatIsGround);

		for (int i = 0; i < colliders.Length; i++) {
			if (colliders [i].gameObject != gameObject) {
				return true;
			}
		}

		return false;
	}

	//void FixedUpdate(){

	//}
}
