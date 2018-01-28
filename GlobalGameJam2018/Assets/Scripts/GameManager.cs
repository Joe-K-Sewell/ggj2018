using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEditor;

public class GameManager : MonoBehaviour {

	public static GameManager Instance = null;

    public enum Device
    {
        Phone = 0,
        Laptop = 1,
        Tablet = 2
    }

    public Dictionary<Device, int> timesEntered = new Dictionary<Device, int>
    {
        { Device.Phone, 0 },
        { Device.Laptop, 0 },
        { Device.Tablet, 0 },
    };

	public float score = 0;

	// Use this for initialization
	void Awake () {
		// ensure only one GameManager exists at a time
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy (gameObject);

		// make sure the manager isn't destoryed between scenes
		DontDestroyOnLoad (gameObject);
	}

    public string ConversationScript;
    public bool TabletToParker;

    public void NavigateFromStart()
    {
        ConversationScript = "OpeningConversation1Phone";
        SceneManager.LoadScene("TextMessagePhone", LoadSceneMode.Single);
    }

    public void NavigateFromDevice()
    {
        switch (ConversationScript)
        {
            case "OpeningConversation1Phone":
                ConversationScript = "OpeningConversation2Tablet";
                TabletToParker = true;
                SceneManager.LoadScene("TextMessageTablet", LoadSceneMode.Single);
                break;
            case "OpeningConversation2Tablet":
                ConversationScript = null;
                TabletToParker = false;
                SceneManager.LoadScene("Menu", LoadSceneMode.Single);
                break;
            default:
                throw new System.Exception("Unhandled scenario");
        }
    }

    public void NavigateFromMenu(Device selectedDevice)
    {
        var timesToThisDevice = timesEntered[selectedDevice];
        var minTimesToAnyDevice = timesEntered.Values.Min();

        if (timesToThisDevice != minTimesToAnyDevice)
        {
            Debug.LogError("Can't visit this yet, some other device needs to be visited first");
            return;
        }

        switch (timesToThisDevice)
        {
            case 0:
                switch (selectedDevice)
                {

                }
                break;
            case 1:
                break;
            default:
                throw new System.Exception("Unhandled # of times");
        }
    }

    public void NavigateFromMinigame()
    {

    }
}
