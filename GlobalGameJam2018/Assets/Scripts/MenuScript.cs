using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour {

    public string[] Labels;
    public Vector2[] Locations;
    public string[] Scenes;

    public GameObject TextObject;
    private TMPro.TMP_Text _textComponent;

    public GameObject CursorObject;
    private UnityEngine.UI.Image _cursorComponent;
    
    private int _index = 0;
    
	// Use this for initialization
	void Start () {
        _textComponent = TextObject.GetComponent<TMPro.TMP_Text>();
        _cursorComponent = CursorObject.GetComponent<UnityEngine.UI.Image>();

        UpdateForIndex();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _index--;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _index++;
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            GameManager.Instance.NavigateFromMenu((GameManager.Device) _index);
            return;
        }
        else
        {
            return;
        }
        
        if (_index < 0) { _index += Labels.Length; }
        if (_index >= Labels.Length) { _index -= Labels.Length; }

        UpdateForIndex();
	}

    private void UpdateForIndex()
    {
        _textComponent.text = Labels[_index];
        _cursorComponent.transform.position = Locations[_index];
    }
}
