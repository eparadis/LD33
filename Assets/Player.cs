using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

    bool _isFalling = true;
    public GameObject Ui;
    List<Bounds> _platformBounds;
    Bounds _bounds;

    void Start()
    {
        var platforms = GameObject.FindGameObjectsWithTag("platform");
        _platformBounds = new List<Bounds>();
        for( var i=0; i<platforms.Length; i+=1)
        {
            _platformBounds.Add(platforms[i].GetComponent<Renderer>().bounds);
        }
    }

    void Update()
    {
        _bounds = GetComponent<Renderer>().bounds;

        if( IsTouchingPlatform())
            _isFalling = false;

        if( _isFalling)
            transform.Translate(0, -0.16f , 0);

        if( HasReachedBottomOfScreen() )
            KillPlayer();
    }

    bool HasReachedBottomOfScreen()
    {
        return transform.position.y < -5.0;
    }

    void KillPlayer()
    {
        Ui.SendMessage("ShowGameOver");
        _isFalling = false;
    }

    bool IsTouchingPlatform()
    {
        foreach( var platformBound in _platformBounds)
        {
            if( platformBound.Intersects( _bounds))
                return true;
        }

        return false;
    }
}
