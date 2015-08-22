using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

    bool _isFalling = true;
    public GameObject Ui;
    List<Bounds> _platformBounds;
    Bounds _bounds;
    int _jumpingCounter = 0;

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

        if( Input.GetAxis("Horizontal") > 0)
            transform.Translate(0.08f, 0, 0);

        if( Input.GetAxis("Horizontal") < 0)
            transform.Translate(-0.08f, 0, 0);

        if( IsTouchingPlatform() && Input.GetButtonDown("Jump"))
            _jumpingCounter = 10;

        if( IsTouchingPlatform())
            _isFalling = false;

        if( _isFalling)
        {
            transform.Translate(0, -0.16f , 0);
        }
        else 
        {
            if( _jumpingCounter > 0 ) 
            {
                _jumpingCounter -= 1;
                transform.Translate(0, 0.32f, 0);
            }

            if( _jumpingCounter == 0)
                _isFalling = true;
        }

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
