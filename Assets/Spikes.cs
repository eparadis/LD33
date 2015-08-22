using UnityEngine;
using System.Collections;

public class Spikes : MonoBehaviour {

    public GameObject Ui;
    public GameObject Player;

    Bounds _bounds;

	void Start () {
        _bounds = GetComponent<Renderer>().bounds;
	}
	
    void Update () {
        Bounds player = Player.GetComponent<Renderer>().bounds;
        
        if( _bounds.Intersects(player))
            PushPlayerBack();
    }
    
    void PushPlayerBack()
    {
        Player.SendMessage("KnockBack", _bounds.center);
    }
}
