using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageGameInput : MonoBehaviour {

	int i = 0;

	public void startGame() {
		StartCoroutine (MessageGame.Instance.DisplayMessage (i++));
	}

	public void onClickOne () {
		if (!MessageGame.Instance.inputLocked) {
			MessageGame.Instance.RecieveInput ("1");
		}
	}

	public void onClickZero () {
		if (!MessageGame.Instance.inputLocked) {
			MessageGame.Instance.RecieveInput ("0");
		}
	}

}
