using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Dialogs : MonoBehaviour {

    public GameObject gameOverDialogPrefab;
    private GameObject _gameOver;
    private GameObject _canvas;

    void Start()
    {
        _canvas = GameObject.Find("UI/Canvas");
    }
    
    void Update() {}

    public void ShowGameOver()
    {
        if( _gameOver == null)
        {
            _gameOver = GameObject.Instantiate(gameOverDialogPrefab);
            _gameOver.transform.SetParent(_canvas.transform, false);
        }
    }
}
