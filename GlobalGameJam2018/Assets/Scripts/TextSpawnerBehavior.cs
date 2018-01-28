using System.Collections;
using System.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextSpawnerBehavior : MonoBehaviour {
    
    public GameObject LeftPrefab;
    public GameObject RightPrefab;

    public string[] CharacterNames;
    public string[] CharacterDisplayNames;
    public Sprite[] CharacterProfileImages;

    public float XOffset;
    public float YBetweenEntries;
    public float YFromBottom;
    public float ScrollUnitsPerSecond;
    
    private enum Side { LEFT, RIGHT }
    private class TextMessageInfo
    {
        public Side Side;
        public int CharacterIndex;
        public string RawText;
        public GameObject Object;
        public RectTransform RectTransform;
        public TMP_Text Text;
        public UnityEngine.UI.Image Image;
    }
    
    private List<TextMessageInfo> _messages;

    private RectTransform _myRect;

    private void ReadConversationScript()
    {
        Time.timeScale = 1;

        _messages = new List<TextMessageInfo>();

        var filename = string.Format("{0}", GameManager.Instance.ConversationScript);
        var asset = (TextAsset) Resources.Load(filename, typeof(TextAsset));

        foreach (var line in asset.text.Split('\n'))
        {
            var cleanedLine = line.TrimEnd(' ', '\r');
            if (string.IsNullOrEmpty(cleanedLine)) { continue; }
            if (cleanedLine.StartsWith("//")) { continue; }
                
            var firstColon = cleanedLine.IndexOf(':');
            var header = cleanedLine.Substring(0, firstColon).TrimEnd(' ');

            var headerSplits = header.Split(',');
            
            Side side;
            switch (headerSplits[0].Trim())
            {
                case "L":
                    side = Side.LEFT;
                    break;
                case "R":
                    side = Side.RIGHT;
                    break;
                default:
                    throw new System.Exception("Unrecognized side, expected L or R: " + headerSplits[0]);
            }

            var characterName = headerSplits[1].Trim();
            var characterNameIndex = -1;
            for (int i = 0; i < CharacterNames.Length; i++)
            {
                if (characterName.Equals(CharacterNames[i]))
                {
                    characterNameIndex = i;
                }
            }
            if (characterNameIndex == -1)
            {
                throw new System.Exception("Unrecognized character name: " + characterName);
            }

            var body = cleanedLine.Substring(firstColon + 1).TrimStart(' ');
            _messages.Add(new TextMessageInfo
            {
                Side = side,
                CharacterIndex = characterNameIndex,
                RawText = body + string.Format("\n<size=50%>{0}</size>", CharacterDisplayNames[characterNameIndex])
            });
        }
    }

	// Use this for initialization
	void Start () {
        ReadConversationScript();
        
        var parentCanvasRect = GetComponentInParent<RectTransform>();

        _myRect = GetComponent<RectTransform>();
        _myRect.sizeDelta = new Vector2(parentCanvasRect.sizeDelta.x, _messages.Count * YBetweenEntries + YFromBottom);

        var startingY = YFromBottom;
        for (int i = _messages.Count - 1; i >= 0; i--)
        {
            var message = _messages[i];

            startingY += YBetweenEntries;

            var gameObj = Instantiate(message.Side == Side.LEFT ? LeftPrefab : RightPrefab);
            gameObj.transform.SetParent(gameObject.transform);
            gameObj.transform.position = new Vector3(XOffset, startingY);
            
            message.Object = gameObj;

            message.RectTransform = gameObj.GetComponent<RectTransform>();

            message.Text = gameObj.GetComponentInChildren<TMP_Text>();
            message.Text.SetText(message.RawText);

            message.Image = gameObj.GetComponentInChildren<UnityEngine.UI.Image>();
            message.Image.sprite = CharacterProfileImages[message.CharacterIndex];
        }

        transform.localPosition = new Vector3(transform.localPosition.x, -1 * (_messages.Count * YBetweenEntries));
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.NavigateFromDevice();
            return;
        }

        var input = Input.GetAxis("Vertical");
        
        var yVal = transform.localPosition.y;

        var corners = new Vector3[4];
        _myRect.GetWorldCorners(corners);
        var yBottom = corners.Select(c => c.y).Min();
        var yTop = corners.Select(c => c.y).Max();
        //Debug.LogFormat("y pos: {0} bot: {1} y top: {2}; in: {3}", yVal, yBottom, yTop, input);
        
        var distanceToApply = input * ScrollUnitsPerSecond * Time.deltaTime;

        if (input > 0 && yBottom > 0)
        {
            return;
        }
        if (input < 0 && yTop < YBetweenEntries + YFromBottom)
        {
            return;
        }

        yVal += distanceToApply;
        
        transform.localPosition = new Vector3(transform.localPosition.x, yVal);
    }
}
