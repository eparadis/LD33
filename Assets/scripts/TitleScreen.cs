using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour {

	void Start () {
	
	}

    public void OnStartButtonClick()
    {
        HardcodedLevelStore levelstore = new HardcodedLevelStore();
        PlayerPrefs.SetString("builtInLevelSpec", levelstore.GetFirstLevel());
        Application.LoadLevel("builtin_level");
    }

    public void OnLevelEditorButtonClick()
    {
        Application.LoadLevel("level_builder");
    }

    public void OnStartSavedLevelButtonClick()
    {
        Application.LoadLevel("saved_level");
    }
}
