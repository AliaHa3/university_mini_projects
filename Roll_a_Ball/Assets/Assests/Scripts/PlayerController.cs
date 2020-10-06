using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	 public float Speed ;
	private int Score;
	public GUIText scoreText;
	public GUIText winText;

	// Use this for initialization
	void Start () {
		Score = 0;
		DisplayText ();
		winText.guiText.text = "";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void FixedUpdate () 
	{
		float MoveHorizantal = Input.GetAxis ("Horizontal");
		float MoveVertical = Input.GetAxis ("Vertical");

		Vector3 Movement = new Vector3 (MoveHorizantal,0.0f,MoveVertical);

		rigidbody.AddForce (Movement * Speed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other) {
		
		if (other.gameObject.tag == "PickUp") 
		{
			other.gameObject.SetActive(false);
			Score++;
			DisplayText();
		}
	}

	void DisplayText()
	{
		scoreText.guiText.text = "Count : " + Score.ToString ();
		if (Score == 12)
	 		winText.guiText.text = "You Win !! ";

	}

}
