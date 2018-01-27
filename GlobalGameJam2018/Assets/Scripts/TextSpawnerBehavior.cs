using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSpawnerBehavior : MonoBehaviour {

    public GameObject TextMessagePrefab;
    
    private enum Side { LEFT, RIGHT }
    private class TextMessageInfo
    {
        public Side Side;
        public string Text;
        public GameObject Object;
    }

    private List<TextMessageInfo> _messages = new List<TextMessageInfo>
    {
        new TextMessageInfo
        {
            Side = Side.LEFT,
            Text = "Are you a real villain?",
        },
        new TextMessageInfo
        {
            Side = Side.RIGHT,
            Text = "Well, technically, uh, nah."
        },
        new TextMessageInfo
        {
            Side = Side.LEFT,
            Text = "Have you ever caught a good guy like a, like a real superhero?"
        },
        new TextMessageInfo
        {
            Side = Side.RIGHT,
            Text = "Nah."
        },
        new TextMessageInfo
        {
            Side = Side.RIGHT,
            Text = "*shakes head*"
        },
        new TextMessageInfo
        {
            Side = Side.LEFT,
            Text = "Have you ever tried a disguise?"
        },
        new TextMessageInfo
        {
            Side = Side.RIGHT,
            Text = "*shakes head again*"
        },
        new TextMessageInfo
        {
            Side = Side.RIGHT,
            Text = "Nah, nah..."
        },
        new TextMessageInfo
        {
            Side = Side.LEFT,
            Text = "Alright! I can see, that I will have to teach you, how to be villains!"
        },
    };
    private int _currentMessageIndex = -1;

	// Use this for initialization
	void Start () {
        foreach (var message in _messages)
        {
            var gameObj = Instantiate(TextMessagePrefab);
            gameObj.transform.SetParent(gameObject.transform);

            var textComponent = gameObj.GetComponent<TMPro.TMP_Text>();
            textComponent.SetText(message.Text);

            message.Object = gameObj;
        }
	}
	
	// Update is called once per frame
	void Update () {
        var oldMessageIndex = _currentMessageIndex;
		if (Input.GetKeyDown(KeyCode.S))
        {
            _currentMessageIndex++;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            _currentMessageIndex--;
        }
        _currentMessageIndex = System.Math.Max(0, _currentMessageIndex);
        _currentMessageIndex = System.Math.Min(_messages.Count - 1, _currentMessageIndex);

        if (oldMessageIndex == _currentMessageIndex) { return; }
        Debug.LogFormat("{0} -> {1}", oldMessageIndex, _currentMessageIndex);

        _messages[oldMessageIndex].Object.transform.position = new Vector3(0, 0);
        _messages[_currentMessageIndex].Object.transform.position = new Vector3(0, 300);
    }
}
