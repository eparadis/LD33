using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    bool _isFalling = true;
    public GameObject Ui;

    void Start(){}

    void Update()
    {
        if( _isFalling)
            transform.Translate(0, -8 * Time.deltaTime, 0);

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
}
