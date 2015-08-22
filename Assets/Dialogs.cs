using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Dialogs : MonoBehaviour {

    public GameObject gameOverDialogPrefab;
    private GameObject _gameOver;
    private GameObject _canvas;
    private Text _scoreLabel;

    void Start()
    {
        _canvas = GameObject.Find("UI/Canvas");
        _scoreLabel = GameObject.Find("UI/Canvas/Panel/ScoreLabel").GetComponent<Text>();
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

    public void IncrementScore()
    {
        _scoreLabel.text = (int.Parse( _scoreLabel.text) + 1).ToString();
    }
}
