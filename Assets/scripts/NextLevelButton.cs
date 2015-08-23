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
        HardcodedLevelStore levelstore = new HardcodedLevelStore();
        string nextLevel = levelstore.GetNextLevel();
        if( nextLevel == "")
        {
            Application.LoadLevel("title");
            return;
        }

        PlayerPrefs.SetString( "builtInLevelSpec", nextLevel);
        Application.LoadLevel("builtin_level");
    }

}
