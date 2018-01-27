using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextSpawnerBehavior : MonoBehaviour {

    public GameObject TextMessagePrefab;
    public float FixedOffset;
    public float ScrollUnitsPerSecond;
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

    private enum ScrollState { NONE, TEXT_UP, TEXT_DOWN }
    private ScrollState _scrollState = ScrollState.TEXT_UP;
    /// <summary> Next index if currently scrolling, otherwise current index </summary>
    private int _currentMessageIndex = 0;
    
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

        var startingY = 0f;
        for (int i = 0; i < _messages.Count; i++)
        {
            var message = _messages[i];
            var leftSide = message.Side == Side.LEFT;

            var gameObj = Instantiate(TextMessagePrefab);
            gameObj.transform.SetParent(gameObject.transform);
            gameObj.transform.localPosition = new Vector3(gameObj.transform.localPosition.x, startingY);
            startingY -= FixedOffset;
            
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
        var distanceToApply = ScrollUnitsPerSecond * Time.deltaTime;
        var yVal = transform.localPosition.y;
        yVal += distanceToApply * Input.GetAxis("Vertical");
        transform.localPosition = new Vector3(transform.localPosition.x, yVal);
    }
}
