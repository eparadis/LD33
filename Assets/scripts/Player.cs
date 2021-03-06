﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

    public AudioClip[] JumpSounds;
    public AudioClip[] HurtSounds;
    public AudioClip[] DeathSounds;

    GameObject _ui;

    bool _isFalling = true;
    List<Bounds> _platformBounds;
    Bounds _bounds;
    int _jumpingCounter = 0;
    bool _disableMovement = false;
    Vector3 _knockbackOrigin;
    int _knockbackCounter = 0;
    AudioSource _audioSource;

    public void KnockBack( Vector3 from)
    {
        if( _knockbackCounter == 0 )
        {
            if( _ui != null)
                _ui.SendMessage("DecrementHealth");
            _knockbackOrigin = from;
            _knockbackCounter = 4;
            PlayRandomSound(HurtSounds);
        }
    }

    public void KillPlayer()
    {
        if( _ui != null )
            _ui.SendMessage("ShowGameOver");
        _isFalling = false;
        _disableMovement = true;
        PlayRandomSound(DeathSounds);
    }

    void Start()
    {
        _ui = GameObject.Find("UI");
        var platforms = GameObject.FindGameObjectsWithTag("platform");
        _platformBounds = new List<Bounds>();
        for( var i=0; i<platforms.Length; i+=1)
        {
            _platformBounds.Add(platforms[i].GetComponent<Renderer>().bounds);
        }
        _knockbackOrigin = Vector3.zero;
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        _bounds = GetComponent<Renderer>().bounds;

        float horizDistance = 0;
        float vertDistance = 0;

        if( _knockbackCounter > 0)
        {
            _knockbackCounter -= 1;

            Vector3 knockback = KnockbackVector();
            horizDistance = knockback.x;
            vertDistance = knockback.y;
        }
        else
        {
            if( Input.GetAxis("Horizontal") > 0 && !_disableMovement)
                horizDistance += 0.08f;
            if( Input.GetAxis("Horizontal") < 0 && !_disableMovement)
                horizDistance += -0.08f;
            if( IsTouchingTopOfPlatform() && Input.GetButtonDown("Jump") && !_disableMovement)
            {
                PlayRandomSound(JumpSounds);
                _jumpingCounter = 10;
            }
        }

        if( IsTouchingTopOfPlatform())
            _isFalling = false;

        if( _isFalling)
        {
            vertDistance += -0.16f;
        }
        else
        {
            if( _jumpingCounter > 0)
            {
                _jumpingCounter -= 1;
                vertDistance += 0.32f;
            }

            if( _jumpingCounter == 0)
                _isFalling = true;
        }

        horizDistance = FindMaxHorizontalMovement( horizDistance);
        vertDistance = FindMaxVerticalMovement( vertDistance);
        transform.Translate(horizDistance, vertDistance, 0);
        
        if( HasReachedBottomOfScreen() && _disableMovement == false )
            KillPlayer();
    }

    void PlayRandomSound( AudioClip[] sounds)
    {
        if( sounds.Length == 0)
            return;
        int randomIndex = Random.Range(0, sounds.Length);
        _audioSource.PlayOneShot( sounds[randomIndex]);
    }

    Vector3 KnockbackVector()
    {
        if( _bounds.center.x > _knockbackOrigin.x)
            return new Vector3( 0.2f, 0, 0);
        else
            return new Vector3( -0.2f, 0, 0);

        //return (_bounds.center - _knockbackOrigin) * 0.4f;
    }

    bool HasReachedBottomOfScreen()
    {
        return transform.position.y < -5.0f;
    }

    bool IsTouchingTopOfPlatform()
    {
        foreach( var platformBound in _platformBounds)
        {
            if( platformBound.Intersects( _bounds)
                && Mathf.Abs(platformBound.max.y - _bounds.min.y) < 0.001f )
                return true;
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

    float FindMaxVerticalMovement( float best)
    {
        var newCharBound = new Bounds(_bounds.center + new Vector3( 0, best, 0), _bounds.size);
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
                float maxMove = testingBound.min.y - _bounds.max.y;
                if( maxMove < 0 )
                    continue;
                if( maxMove < best)
                    best = maxMove;
            }
        } else if( best < 0)
        {
            foreach( Bounds testingBound in intersectingBounds)
            {
                float maxMove = testingBound.max.y - _bounds.min.y;
                if( maxMove > 0)
                    continue;
                if( maxMove > best)
                    best = maxMove;
            }
        }
        
        return best;
    }
}