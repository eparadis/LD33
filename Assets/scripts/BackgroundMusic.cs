using UnityEngine;
using System.Collections;

public class BackgroundMusic : MonoBehaviour {

    public static BackgroundMusic OnlyOneInstance;

    void Awake() {
        if( OnlyOneInstance)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(transform.gameObject);
            OnlyOneInstance = this;
        }
    }

	void Start () {
	}
}
