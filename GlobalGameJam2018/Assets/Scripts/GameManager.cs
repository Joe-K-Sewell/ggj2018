using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour {

	public static GameManager Instance = null;

    public enum Device
    {
        Phone = 0,
        Computer = 1,
        Tablet = 2
    }

    public Dictionary<Device, int> timesEntered = new Dictionary<Device, int>
    {
        { Device.Phone, 0 },
        { Device.Computer, 0 },
        { Device.Tablet, 0 },
    };

	public float score = 0;
	public float lastScore = 0;

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
    public Device? NextDevice = null;

    public void NavigateFromStart()
    {
        ConversationScript = "OpeningConversation1Phone";
        SceneManager.LoadScene("TextMessagePhone", LoadSceneMode.Single);
    }

    public void NavigateFromDevice()
    {
        if (ConversationScript.EndsWith("ConversationStart"))
        {
            if (ConversationScript.StartsWith("Phone"))
            {
                SceneManager.LoadScene("MatchingGame", LoadSceneMode.Single);
                return;
            }
            if (ConversationScript.StartsWith("Computer"))
            {
                SceneManager.LoadScene("Bullet HellMinigame", LoadSceneMode.Single);
                return;
            }
            if (ConversationScript.StartsWith("Tablet"))
            {
                SceneManager.LoadScene("ShooterMinigame", LoadSceneMode.Single);
                return;
            }
        }
        if (ConversationScript.EndsWith("End"))
        {
            if (timesEntered.Values.Min() == 2)
            {
                ConversationScript = "GoodEnding";
                SceneManager.LoadScene("TextMessagePhone");
                return;
            }
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
            NextDevice = null;
            return;
        }
        if (ConversationScript.EndsWith("Ending"))
        {
            SceneManager.LoadScene("Credits", LoadSceneMode.Single);
            NextDevice = null;
        }

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

        timesToThisDevice++;
        timesEntered[selectedDevice] = timesToThisDevice;

        ConversationScript = string.Format("{0}{1}ConversationStart", selectedDevice, timesToThisDevice);

        NextDevice = selectedDevice;
        ToDevice();
    }

    public void NavigateFromMinigame(bool won)
    {
        var kind = won ? "Good" : "Bad";
        ConversationScript = ConversationScript.Replace("ConversationStart", "Conversation" + kind + "End");
        ToDevice();
    }

    private void ToDevice()
    {
        string sceneName;
        switch (NextDevice)
        {
            case Device.Phone:
                sceneName = "TextMessagePhone";
                break;
            case Device.Tablet:
                sceneName = "TextMessageTablet";
                break;
            case Device.Computer:
                sceneName = "TextMessage";
                break;
            default:
                throw new System.Exception("Unrecognized device");
        }
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
