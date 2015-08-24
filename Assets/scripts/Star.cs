using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour {

    GameObject _ui;
    GameObject _player;

	// Use this for initialization
	void Start () {
        _ui = GameObject.Find("UI");
        _player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        Bounds bounds = GetComponent<Renderer>().bounds;
        Bounds player = _player.GetComponent<Renderer>().bounds;

        if( bounds.Intersects(player))
            CollectStar();
	}

    void CollectStar()
    {
        if( _ui != null)
            _ui.SendMessage("IncrementScore");
        Destroy(gameObject);
    }
}
