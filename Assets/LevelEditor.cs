using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelEditor : MonoBehaviour {

    public GameObject EditorPanelPrefab;
    public GameObject PlayerPrefab;
    public GameObject PlatformPrefab;
    public GameObject SpikePrefab;
    public GameObject StarPrefab;

    GameObject _canvas;
    GameObject _ui;
    GameObject _editor;

	// Use this for initialization
	void Start () {
        //
        //_ui = GameObject.Find("UI");
        //_canvas = GameObject.Find("UI/Canvas");
        //_editor = GameObject.Instantiate(EditorPanelPrefab);
        //_editor.transform.SetParent(_canvas.transform, false);
        var levelspec = PlayerPrefs.GetString("levelspec", "");
        if( levelspec != "") 
            GameObject.Find("LevelEditor/Canvas/EditorPanel/InputField").GetComponent<InputField>().text = levelspec;
	}
	
	// Update is called once per frame
    void Update () {}

    public void AddPlatform()
    {
        GameObject.Instantiate(PlatformPrefab);
    }

    public void LoadLevel()
    {
        // TODO reset the level

        string input = GameObject.Find("LevelEditor/Canvas/EditorPanel/InputField").GetComponent<InputField>().text;
        PlayerPrefs.SetString("levelspec", input);
        foreach( var line in input.Split('\n'))
        {
            string cleanLine = line.Trim();
            if( cleanLine.Length == 0)
                continue;

            if( cleanLine[0] == '#')
            {
                Debug.Log("ignoreing comment " + line);
                continue;
            }

            if( cleanLine[0] == 'P')
            {
                var player = GameObject.Instantiate(PlayerPrefab);
            }

            if( cleanLine[0] == 'L')
            {
                var args = cleanLine.Split(' '); // , System.StringSplitOptions.RemoveEmptyEntries);
                GameObject.Instantiate(PlatformPrefab, ParsePosition( args[1], args[2]), Quaternion.identity );
            }

            if( cleanLine[0] == 'S')
            {
                var args = cleanLine.Split(' ');
                GameObject.Instantiate(StarPrefab, ParsePosition( args[1], args[2]), Quaternion.identity );
            }

            if( cleanLine[0] == 'K')
            {
                var args = cleanLine.Split(' ');
                GameObject.Instantiate(SpikePrefab, ParsePosition( args[1], args[2]), Quaternion.identity );
            }

        }
    }

    Vector3 ParsePosition( string x, string y )
    {
        return new Vector3( float.Parse(x), float.Parse(y), 0);
    }
    
}
