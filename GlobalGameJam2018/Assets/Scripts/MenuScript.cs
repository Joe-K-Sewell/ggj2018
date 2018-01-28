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
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _index = (_index - 1) % Labels.Length;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _index = (_index + 1) % Labels.Length;
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(Scenes[_index]);
            return;
        }
        else
        {
            return;
        }

        _textComponent.text = Labels[_index];
        _cursorComponent.transform.position = Locations[_index];
	}
}
