using UnityEngine;
using System.Collections;

public class BackgroundMusic : MonoBehaviour {

    public AudioClip TitleBGM;

    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }

	void Start () {
        if( TitleBGM != null)
            GetComponent<AudioSource>().PlayOneShot(TitleBGM);
	}
}
