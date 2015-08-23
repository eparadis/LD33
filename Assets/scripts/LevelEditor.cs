using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
    LevelSpecParser _parser;
    List<GameObject> _previouslyLoadedObjects;

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
        _previouslyLoadedObjects = new List<GameObject>();
	}
	
	// Update is called once per frame
    void Update () {}

    public void AddPlatform()
    {
        GameObject.Instantiate(PlatformPrefab);
    }

    public void LoadLevel()
    {
        DestroyObjects( _previouslyLoadedObjects );
        _parser = new LevelSpecParser( PlayerPrefab, PlatformPrefab, SpikePrefab, StarPrefab);
        string input = GameObject.Find("LevelEditor/Canvas/EditorPanel/InputField").GetComponent<InputField>().text;
        PlayerPrefs.SetString("levelspec", input);
        _parser.CreateGameObjectsFromLevelSpec(input);
        _previouslyLoadedObjects = _parser.GetInstantiatedObjects();
    }

    public void PlayLevel()
    {
        Application.LoadLevel("saved_level");
    }

    void DestroyObjects( List<GameObject> levelObjects)
    {
        foreach( var go in levelObjects)
        {
            Destroy(go);
        }
    }
}

public class LevelSpecParser
{
    GameObject _player;
    GameObject _platform;
    GameObject _spike;
    GameObject _star;
    List<GameObject> _levelObjects;

    public LevelSpecParser( GameObject player, GameObject platform, GameObject spike, GameObject star)
    {
        _player = player;
        _platform = platform;
        _spike = spike;
        _star = star;
        _levelObjects = new List<GameObject>();
    }

    public void CreateGameObjectsFromLevelSpec(string input)
    {
        foreach (var line in input.Split('\n'))
        {
            string cleanLine = line.Trim();
            if (cleanLine.Length == 0)
                continue;
            if (cleanLine[0] == '#')
            {
                Debug.Log("ignoreing comment " + line);
                continue;
            }
            if (cleanLine[0] == 'P')
            {
                var player = (GameObject)GameObject.Instantiate(_player);
                player.name = "Player";
                _levelObjects.Add(player);
            }
            if (cleanLine[0] == 'L')
            {
                var args = cleanLine.Split(' ');
                // , System.StringSplitOptions.RemoveEmptyEntries);
                var platform = (GameObject)GameObject.Instantiate(_platform, ParsePosition(args[1], args[2]), Quaternion.identity);
                _levelObjects.Add(platform);
            }
            if (cleanLine[0] == 'S')
            {
                var args = cleanLine.Split(' ');
                var star = (GameObject)GameObject.Instantiate(_star, ParsePosition(args[1], args[2]), Quaternion.identity);
                _levelObjects.Add(star);
            }
            if (cleanLine[0] == 'K')
            {
                var args = cleanLine.Split(' ');
                var spike = (GameObject)GameObject.Instantiate(_spike, ParsePosition(args[1], args[2]), Quaternion.identity);
                _levelObjects.Add(spike);
            }
        }
    }
    
    public List<GameObject> GetInstantiatedObjects()
    {
        return _levelObjects;
    }

    Vector3 ParsePosition( string x, string y )
    {
        return new Vector3( float.Parse(x), float.Parse(y), 0);
    }
}
