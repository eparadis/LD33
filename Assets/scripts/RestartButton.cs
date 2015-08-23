using UnityEngine;
using System.Collections;

public class RestartButton : MonoBehaviour {

    public void DoRestart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
