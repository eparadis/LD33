using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {

    public GameObject PlayerPrefab;
    public GameObject PlatformPrefab;
    public GameObject SpikePrefab;
    public GameObject StarPrefab;

	// Use this for initialization
	void Start () {

        var input = PlayerPrefs.GetString("levelspec", "");
        var parser = new LevelSpecParser( PlayerPrefab, PlatformPrefab, SpikePrefab, StarPrefab);
        parser.CreateGameObjectsFromLevelSpec(input);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
