using UnityEngine;
using System.Collections;

public class Spikes : MonoBehaviour {

    public GameObject Ui;
    GameObject _player;

    Bounds _bounds;

	void Start () {
        _player = GameObject.Find("Player");
        _bounds = GetComponent<Renderer>().bounds;
	}
	
    void Update () {
        Bounds player = _player.GetComponent<Renderer>().bounds;
        
        if( _bounds.Intersects(player))
            PushPlayerBack();
    }
    
    void PushPlayerBack()
    {
        _player.SendMessage("KnockBack", _bounds.center);
    }
}
