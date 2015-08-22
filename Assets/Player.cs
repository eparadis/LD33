using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

    bool _isFalling = true;
    public GameObject Ui;
    List<Bounds> _platformBounds;
    Bounds _bounds;
    int _jumpingCounter = 0;
    float _allowableTravel;

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

        float horizDistance = 0;
        if( Input.GetAxis("Horizontal") > 0)
            horizDistance = 0.08f;
        if( Input.GetAxis("Horizontal") < 0)
            horizDistance = -0.08f;

        horizDistance = FindMaxHorizontalMovement( horizDistance);
        transform.Translate(horizDistance, 0, 0);

        float vertDistance = 0;
        if( Input.GetAxis("Vertical") > 0)
        {
            if( WouldPassThroughBottomOfPlatform(0.08f) )
                vertDistance = _allowableTravel;
            else
                vertDistance = 0.08f;
            transform.Translate(0, vertDistance, 0);
        }
        
        if( Input.GetAxis("Vertical") < 0)
        {
            if( WouldPassThroughTopOfPlatform(-0.08f) )
                vertDistance = _allowableTravel;
            else
                vertDistance = -0.08f;
            transform.Translate(0, vertDistance, 0);
        }

        /*if( IsTouchingPlatform() && Input.GetButtonDown("Jump"))
            _jumpingCounter = 10;

        if( IsTouchingPlatform())
            _isFalling = false;

        float distance = 0;
        if( _isFalling)
        {
            if( WouldPassThroughTopOfPlatform(-0.16f))
                distance = _allowableTravel; 
            else
                distance = -0.16f;
            transform.Translate(0, distance, 0);
        }
        else
        {
            if( _jumpingCounter > 0)
            {
                _jumpingCounter -= 1;
                if( WouldPassThroughBottomOfPlatform(0.32f))
                {
                    _jumpingCounter = 0;
                    distance = _allowableTravel;
                }
                else
                    distance = 0.32f;
                transform.Translate(0, distance, 0);
            }
            if( _jumpingCounter == 0)
                _isFalling = true;
        }*/

        if( HasReachedBottomOfScreen() )
            KillPlayer();
    }

    bool HasReachedBottomOfScreen()
    {
        return transform.position.y < -5.0f;
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

    bool WouldPassThroughTopOfPlatform( float distance)
    {
        var newCharBound = new Bounds(_bounds.center + new Vector3( 0, distance, 0), _bounds.size);
        foreach( var platformBound in _platformBounds)
        {
            if( platformBound.Intersects( newCharBound))
            {
                Debug.Log("intersect top");
                if( OverlapTop( newCharBound, platformBound))
                {
                    _allowableTravel = GetAllowableTravelDownwards( _bounds, platformBound);
                    return true;
                }
            }
        }
        
        return false;
    }

    bool OverlapTop(Bounds character, Bounds platform)
    {
        if( character.min.y < platform.max.y )
        {
            Debug.Log("would overlap top");
            return true;
        }
        return false;
    }

    float GetAllowableTravelDownwards( Bounds character, Bounds platform)
    {
        return platform.max.y - character.min.y;
    }

    float GetAllowableTravelRightwards( Bounds character, Bounds platform)
    {
        return platform.min.x - character.max.x;
    }

    float GetAllowableTravelLeftwards( Bounds character, Bounds platform)
    {
        return platform.max.x - character.min.x;
    }

    float GetAllowableTravelUpwards( Bounds character, Bounds platform)
    {
        return platform.min.y - character.max.y;
    }

    bool OverlapRight( Bounds character, Bounds platform)
    {
        if( character.max.x > platform.min.x )
        {
            Debug.Log("would overlap right");
            return true;
        }
        return false;
    }

    bool WouldTouchRightSideOfPlatform( float distance)
    {
        var newCharBound = new Bounds(_bounds.center + new Vector3( distance, 0, 0), _bounds.size);
        foreach( var platformBound in _platformBounds)
        {
            if( platformBound.Intersects( newCharBound))
            {
                Debug.Log("intersect right");
                if( OverlapTop( newCharBound, platformBound)
                    && OverlapRight( newCharBound, platformBound))
                {
                    _allowableTravel = GetAllowableTravelRightwards( _bounds, platformBound);
                    return true;
                }
            }
        }
        
        return false;
    }

    bool OverlapLeft( Bounds character, Bounds platform)
    {
        if( character.min.x < platform.max.x )
        {
            Debug.Log("would overlap left");
            return true;
        }
        return false;
    }
    
    bool WouldTouchLeftSideOfPlatform( float distance)
    {
        var newCharBound = new Bounds(_bounds.center + new Vector3( distance, 0, 0), _bounds.size);
        foreach( var platformBound in _platformBounds)
        {
            if( platformBound.Intersects( newCharBound))
            {
                Debug.Log("intersect left");
                if( OverlapTop( newCharBound, platformBound)
                    && OverlapLeft( newCharBound, platformBound))
                {
                    _allowableTravel = GetAllowableTravelLeftwards( _bounds, platformBound);
                    return true;
                }
            }
        }
        
        return false;
    }

    bool OverlapBottom( Bounds character, Bounds platform)
    {
        if( character.max.y > platform.min.y )
        {
            Debug.Log("would overlap bottom");
            return true;
        }
        return false;
    }
    
    bool WouldPassThroughBottomOfPlatform( float distance)
    {
        var newCharBound = new Bounds(_bounds.center + new Vector3( 0, distance, 0), _bounds.size);
        foreach( var platformBound in _platformBounds)
        {
            if( platformBound.Intersects( newCharBound))
            {
                Debug.Log("intersect bottom");
                if( OverlapBottom( newCharBound, platformBound))
                {
                    _allowableTravel = GetAllowableTravelUpwards( _bounds, platformBound);
                    return true;
                }
            }
        }
        
        return false;
    }

    float FindMaxHorizontalMovement( float best)
    {
        var newCharBound = new Bounds(_bounds.center + new Vector3( best, 0, 0), _bounds.size);
        List<Bounds> intersectingBounds = new List<Bounds>();
        foreach( var platformBound in _platformBounds)
        {
            if( platformBound.Intersects( newCharBound))
                intersectingBounds.Add(platformBound);
        }

        if( best > 0)
        {
            foreach( Bounds testingBound in intersectingBounds)
            {
                float maxMove = testingBound.min.x - _bounds.max.x;
                if( maxMove < 0 )
                    continue;
                if( maxMove < best)
                    best = maxMove;
            }
        } else if( best < 0)
        {
            foreach( Bounds testingBound in intersectingBounds)
            {
                float maxMove = testingBound.max.x - _bounds.min.x;
                if( maxMove > 0)
                    continue;
                if( maxMove > best)
                    best = maxMove;
            }
        }

        return best;
    }
}