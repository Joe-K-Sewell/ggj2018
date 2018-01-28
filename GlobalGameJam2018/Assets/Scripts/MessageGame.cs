using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MessageGame : MonoBehaviour {

	public static MessageGame Instance;

	public GameObject messageContainer;
	public GameObject inputContainer;
	public GameObject text;
	public GameObject panel;
	public GameObject prompt;
	public GameObject canvas;

	public int msgLength = 10;

	[HideInInspector]
	public bool inputLocked;

	string message;

	int currentMsgPosition = 0;
	int currentInputPosition = 0;

	float numCorrect = 0;
	float numAttempt = 0;
	float msgInterval = 0.4f;

	// Use this for initialization
	void Start () {
		inputLocked = true;
		Instance = this;
		GenerateMessage ();
		FillContainers ();
	}

	void GameOver () {
		Debug.Log ("Game Over");
		Debug.Log (numCorrect);
		float score = (numCorrect / numAttempt) * 100;
		Debug.Log (score);
		GameManager.Instance.score += score;
		StartCoroutine (EndGame ());
	}

	IEnumerator EndGame () {
		canvas.SetActive (false);
		yield return new WaitForSecondsRealtime (3);
		SceneManager.LoadScene("Menu", LoadSceneMode.Single);
		yield return 0;
	}

	// fills the containers with empty text components
	void FillContainers () {
		for (int i = 0; i < msgLength; i++) {
			GameObject msgBox = (GameObject)Instantiate (text);
			msgBox.GetComponentInChildren<Text> ().text = "";
			msgBox.transform.SetParent (messageContainer.transform);

			GameObject inputBox = (GameObject)Instantiate (text);
			inputBox.GetComponentInChildren<Text> ().text = "";
			inputBox.transform.SetParent (inputContainer.transform);
		}
	}

	// displays the message up to the passed msgPosition
	public IEnumerator DisplayMessage (int msgPosition) {
		SetPrompt ("Remember the message!");
		inputLocked = true;

		StartCoroutine (EraseMessage());
		for (int i = 0; i <= msgPosition; i++) {
			yield return new WaitForSecondsRealtime (msgInterval);
			messageContainer.transform.GetChild (i).GetComponentInChildren<Text>().text = message[i].ToString();
		}
	}

	// erases all the text components in the message boxes
	IEnumerator EraseMessage() {
		yield return new WaitForSecondsRealtime (0.5f + (msgLength*.08f));
		for (int i = 0; i <= currentMsgPosition; i++) {
			yield return new WaitForSecondsRealtime (msgInterval);
			messageContainer.transform.GetChild (i).GetComponentInChildren<Text> ().text = "";
		}

		SetPrompt ("Enter the message!");
		inputLocked = false;
	}

	// places the passed string into the next open box
	public void RecieveInput (string input) {
		inputContainer.transform.GetChild (currentInputPosition).GetComponentInChildren<Text>(). text = input;
		if (currentInputPosition == currentMsgPosition) {
			CheckInput ();
			StartCoroutine(ClearInput ());
			currentInputPosition = 0;
			currentMsgPosition++;
		} else {
			currentInputPosition++;
		}
	}

	// clears all the input strings
	IEnumerator ClearInput () {
		yield return new WaitForSecondsRealtime (1);
		for (int i = 0; i < inputContainer.transform.childCount; i++) {
			inputContainer.transform.GetChild (i).GetComponentInChildren<Text>().text = "";
			inputContainer.transform.GetChild (i).GetComponent<Image>().color = new Color(0,0,0,0);
		}
		yield return new WaitForSecondsRealtime(2);
		if (currentMsgPosition == msgLength) {
			Debug.Log ("done");
			GameOver ();
		} else {
			Debug.Log ("not done");
			StartCoroutine(DisplayMessage (currentMsgPosition));
		}
		yield break;
	}

	// prompts the users to get ready for the message and to recall the message
	void SetPrompt (string message) {
		prompt.GetComponent<TextMeshProUGUI> ().text = message;
	}

	public void onStart () {
		panel.SetActive (false);
		StartCoroutine (StartGame());
	}

	IEnumerator StartGame () {
		SetPrompt ("Get Ready!!");
		yield return new WaitForSecondsRealtime (2);
		//yield return new WaitForSeconds (2);
		StartCoroutine(DisplayMessage (currentMsgPosition));
	}

	// checks the users input to make sure it matches
	void CheckInput () {
		for (int i = 0; i <= currentMsgPosition; i++) {
			if (inputContainer.transform.GetChild(i).GetComponentInChildren<Text>().text == message[i].ToString() ) {
				// correct, mark box green
				inputContainer.transform.GetChild (i).GetComponent<Image>().color = Color.green;
				numCorrect++;
			} else {
				// false, mark box red
				inputContainer.transform.GetChild (i).GetComponent<Image>().color = Color.red;
			}
			numAttempt++;
		}
	}

	// generates a random string of 1s and 0s
	void GenerateMessage() {
		message = "";
		for (int i = 0; i < msgLength; i++) {
			int character = Random.Range (0, 2);
			message += character.ToString();
		}
		Debug.Log ("message " + message);
	}
}
