using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageGame : MonoBehaviour {

	public static MessageGame Instance;

	public GameObject messageContainer;
	public GameObject inputContainer;
	public GameObject text;

	public bool inputLocked;

	string message;
	int msgLength = 10;
	int currentMsgPosition = 0;
	int currentInputPosition = 0;

	float msgInterval = 0.4f;
	public Text display;

	// Use this for initialization
	void Start () {
		inputLocked = false;
		Instance = this;
		GenerateMessage ();
		FillContainers ();
	}

	void FillContainers () {
		for (int i = 0; i < msgLength; i++) {
			GameObject msgBox = (GameObject)Instantiate (text);
			msgBox.GetComponent<Text> ().text = "";
			msgBox.transform.SetParent (messageContainer.transform);

			GameObject inputBox = (GameObject)Instantiate (text);
			inputBox.GetComponent<Text> ().text = "";
			inputBox.transform.SetParent (inputContainer.transform);
		}
	}

	// displays the message up to the passed msgPosition
	public IEnumerator DisplayMessage (int msgPosition) {
		StartCoroutine (EraseMessage());
		for (int i = 0; i < msgPosition; i++) {
			yield return new WaitForSeconds (msgInterval);
			messageContainer.transform.GetChild (i).GetComponent<Text>().text = message[i].ToString();
		}
	}

	IEnumerator EraseMessage() {
		yield return new WaitForSeconds (0.5f + (msgLength*.08f));
		for (int i = 0; i < messageContainer.transform.childCount; i++) {
			yield return new WaitForSeconds (msgInterval);
			messageContainer.transform.GetChild (i).GetComponent<Text> ().text = "";
		}
	}

	public void RecieveInput (string input) {
		Debug.Log (currentInputPosition);
		Debug.Log ("Input is " + input);
		inputContainer.transform.GetChild (currentInputPosition).GetComponent<Text>(). text = input;
		if (currentInputPosition == currentMsgPosition) {
			CheckInput ();
			ClearInput ();
			currentInputPosition = 0;
			currentMsgPosition++;
		} else {
			currentInputPosition++;
		}
	}

	void ClearInput () {
		for (int i = 0; i < inputContainer.transform.childCount; i++) {
			inputContainer.transform.GetChild (i).GetComponent<Text>().text = "";
		}
	}

	// checks the users input to make sure it matches
	void CheckInput () {
		for (int i = 0; i < currentMsgPosition; i++) {
			if (inputContainer.transform.GetChild(i).GetComponent<Text>().text == message[i].ToString() ) {
				// correct
				Debug.Log ("correct");
				// mark box green
			} else {
				//false
				Debug.Log ("incorrect");
				// mark box red
			}
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
