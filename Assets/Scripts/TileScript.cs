using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {

	private float fallDelay = 1.5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Trigger utk spawn tile
	void OnTriggerExit(Collider other){


		if (other.tag == "Player") {
			TileManager.Instance.SpawnTiles ();
			StartCoroutine (FallDown());
		}
	}

	IEnumerator FallDown(){
		yield return new WaitForSeconds (fallDelay);
		GetComponent<Rigidbody> ().isKinematic = false;

		// ketika tiles jatuh pastikan recycle
		yield return new WaitForSeconds (2);

		switch(gameObject.name){
		case "LeftTile":
			TileManager.Instance.LeftTiles.Push (gameObject);
			gameObject.GetComponent<Rigidbody> ().isKinematic = true;
			gameObject.SetActive (false);
			break;

		case "TopTile":
			TileManager.Instance.TopTiles.Push (gameObject);
			gameObject.GetComponent<Rigidbody> ().isKinematic = true;
			gameObject.SetActive (false);
			break;
			
		}
	}

}
