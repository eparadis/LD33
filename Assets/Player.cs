using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	bool _isFalling = true;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if( _isFalling)
		{
			transform.Translate(0, -8 * Time.deltaTime, 0);
		}
	}
}
