using UnityEngine;
using System.Collections;

public class NextLevelButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void NextLevelButtonClick()
    {
        string nextLevel = GetNextBuiltInLevelSpec();
        if( nextLevel == "")
            Application.LoadLevel("title");
        PlayerPrefs.SetString( "builtInLevelSpec", nextLevel);
        Application.LoadLevel("builtin_level");
    }

    string GetNextBuiltInLevelSpec()
    {
        return "";
    }
}
