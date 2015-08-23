using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Dialogs : MonoBehaviour {

    public GameObject gameOverDialogPrefab;
    public GameObject Player;
    private GameObject _gameOver;
    private GameObject _canvas;
    private Text _scoreLabel;
    private Text _healthLabel;
    private int _health;
    private int _score;

    void Start()
    {
        _canvas = GameObject.Find("UI/Canvas");
        _scoreLabel = GameObject.Find("UI/Canvas/Panel/ScoreLabel").GetComponent<Text>();
        _healthLabel = GameObject.Find("UI/Canvas/Panel/HealthLabel").GetComponent<Text>();

        _health = 5;
        _score = 0;
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
        _score += 1;
        _scoreLabel.text = _score.ToString();
    }

    public void DecrementHealth()
    {
        _health -= 1;
        _healthLabel.text = _health.ToString();

        if( _health == 0)
            Player.SendMessage("KillPlayer");
    }
}
