using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public float speed;
	private int point = 0;
	private float Timer = 0.0f;

	public Text pointText;
	public Text timerText;
	public Text statusText;
	public GameObject panel;

	public Rigidbody rb;

    public Button continueBtn;
	public Button startBtn;

	void Start ()
	{
		
		rb = GetComponent<Rigidbody>();


		panel = GameObject.FindGameObjectsWithTag("panel")[0];
		panel.gameObject.SetActive (false);

//		Button btn = continueBtn.GetComponent<Button>();
//		btn.onClick.AddListener(changeTimeScale);
//
//		Button sBtn = startBtn.GetComponent<Button>();
//		sBtn.onClick.AddListener(restartGame);


	}

	void Update (){
		
		Timer += Time.deltaTime;
		timerText.text = "TIMER:   " + (int)Timer + "s";
		pointText.text =  "SCORE:   " + point;
		if (Input.GetKeyDown (KeyCode.Space) && statusText.text == "") {
			changeTimeScale ();
		}
		if (Input.GetKey("escape"))
			Application.Quit();
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (movement * speed);
	}

	void OnCollisionEnter(Collision other) 
	{
		if (other.gameObject.CompareTag ("score"))
		{
			other.gameObject.SetActive (false);
			point += 2;
			pointText.text = "SCORE:   " + point.ToString ();
			if (point >= 20) {
				//win
				statusText.text =  "YOU WIN!";
				changeTimeScale();
				continueBtn.interactable = false;

			}
		}
		if (other.gameObject.CompareTag ("bomb"))
		{
			//lose
			statusText.text =  "YOU LOSE!";
			changeTimeScale();
			continueBtn.interactable = false;
		}
	}


	public void changeTimeScale () {
		if (Time.timeScale != 0.0f) {
			Time.timeScale = 0.0f;
			panel.gameObject.SetActive(true);
		} 
		else {
			Time.timeScale = 1.0f;
			panel.gameObject.SetActive(false);
		}
	}

	public void restartGame () {

		changeTimeScale();
		continueBtn.interactable = true;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);	
		
	}
}