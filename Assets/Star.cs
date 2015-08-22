using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour {

    public GameObject Ui;
    public GameObject Player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Bounds bounds = GetComponent<Renderer>().bounds;
        Bounds player = Player.GetComponent<Renderer>().bounds;

        if( bounds.Intersects(player))
            CollectStar();
	}

    void CollectStar()
    {
        Ui.SendMessage("IncrementScore");
        Destroy(gameObject);
    }
}
