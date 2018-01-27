using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextSpawnerBehavior : MonoBehaviour {

    public GameObject TextMessagePrefab;
    public float FixedOffset;
    public TextAsset ConversationScript;
    
    private enum Side { LEFT, RIGHT }
    private class TextMessageInfo
    {
        public Side Side;
        public string RawText;
        public GameObject Object;
        public TMP_Text Text;
    }
    
    private List<TextMessageInfo> _messages;
    // = new List<TextMessageInfo>
    //{
    //    new TextMessageInfo
    //    {
    //        Side = Side.LEFT,
    //        RawText = "Are you a real villain?",
    //    },
    //    new TextMessageInfo
    //    {
    //        Side = Side.RIGHT,
    //        RawText = "Well, technically, uh, nah."
    //    },
    //    new TextMessageInfo
    //    {
    //        Side = Side.LEFT,
    //        RawText = "Have you ever caught a good guy like a, like a real superhero?"
    //    },
    //    new TextMessageInfo
    //    {
    //        Side = Side.RIGHT,
    //        RawText = "Nah."
    //    },
    //    new TextMessageInfo
    //    {
    //        Side = Side.RIGHT,
    //        RawText = "*shakes head*"
    //    },
    //    new TextMessageInfo
    //    {
    //        Side = Side.LEFT,
    //        RawText = "Have you ever tried a disguise?"
    //    },
    //    new TextMessageInfo
    //    {
    //        Side = Side.RIGHT,
    //        RawText = "*shakes head again*"
    //    },
    //    new TextMessageInfo
    //    {
    //        Side = Side.RIGHT,
    //        RawText = "Nah, nah..."
    //    },
    //    new TextMessageInfo
    //    {
    //        Side = Side.LEFT,
    //        RawText = "Alright! I can see, that I will have to teach you, how to be villains!"
    //    },
    //};
    private int _currentMessageIndex = -1;
    
    private void ReadConversationScript()
    {
        _messages = new List<TextMessageInfo>();
        
        foreach (var line in ConversationScript.text.Split('\n'))
        {
            var cleanedLine = line.TrimEnd(' ', '\r');
            if (string.IsNullOrEmpty(cleanedLine)) { continue; }

            var firstColon = cleanedLine.IndexOf(':');
            var header = cleanedLine.Substring(0, firstColon).TrimEnd(' ');
            var body = cleanedLine.Substring(firstColon + 1).TrimStart(' ');

            Side side;
            switch (header[0])
            {
                case 'L':
                    side = Side.LEFT;
                    break;
                case 'R':
                    side = Side.RIGHT;
                    break;
                default:
                    throw new System.Exception("Unrecognized: " + header[0]);
            }

            _messages.Add(new TextMessageInfo
            {
                Side = side,
                RawText = body
            });
        }
    }

	// Use this for initialization
	void Start () {
        ReadConversationScript();

        var parentCanvasRect = GetComponentInParent<RectTransform>();

        var spawnerRect = GetComponent<RectTransform>();
        spawnerRect.sizeDelta = new Vector2(parentCanvasRect.sizeDelta.x, 1);

        foreach (var message in _messages)
        {
            var leftSide = message.Side == Side.LEFT;

            var gameObj = Instantiate(TextMessagePrefab);
            gameObj.transform.SetParent(gameObject.transform);

            var rectTransform = gameObj.GetComponent<RectTransform>();
            var anchorNum = leftSide ? 0 : 1;
            rectTransform.anchorMin = new Vector2(anchorNum, 1);
            rectTransform.anchorMax = new Vector2(anchorNum, 1);
            rectTransform.pivot = new Vector2(anchorNum, 1);
            
            message.Text = gameObj.GetComponent<TMP_Text>();
            message.Text.SetText(message.RawText);
            message.Text.alignment = leftSide
                ? TextAlignmentOptions.Left
                : TextAlignmentOptions.Right;

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

        float nextDistance = FixedOffset;
        for (int i = _messages.Count - 1; i >= 0; i--)
        {
            var transform = _messages[i].Object.transform;
            var xPos = transform.localPosition.x;
            if (i > _currentMessageIndex)
            {
                transform.localPosition = new Vector3(xPos, 0);
            }
            else
            {
                transform.localPosition = new Vector3(xPos, nextDistance);
                nextDistance += FixedOffset;
            }
        }
    }
}
