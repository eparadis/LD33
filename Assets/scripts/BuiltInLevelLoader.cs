using UnityEngine;
using System.Collections;

public class BuiltInLevelLoader : MonoBehaviour {

    public GameObject PlayerPrefab;
    public GameObject PlatformPrefab;
    public GameObject SpikePrefab;
    public GameObject StarPrefab;
    
    // Use this for initialization
    void Start () {
        
        var input = PlayerPrefs.GetString("builtInLevelSpec", "");

        if( input == "")
        {
            Application.LoadLevel("title");
            return;
        }

        var parser = new LevelSpecParser( PlayerPrefab, PlatformPrefab, SpikePrefab, StarPrefab);
        parser.CreateGameObjectsFromLevelSpec(input);
    }
}
