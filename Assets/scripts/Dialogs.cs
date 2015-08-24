using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Dialogs : MonoBehaviour {

    public GameObject GameOverDialogPrefab;
    public GameObject YouWonDialogPrefab;
    public AudioClip[] CoinSounds;
    
    private AudioSource _audioSource;
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
        _audioSource = GetComponent<AudioSource>();
        
        _health = 5;
        _score = 0;
    }
    
    void Update() {}

    public void ShowGameOver()
    {
        if( _gameOver == null)
        {
            _gameOver = GameObject.Instantiate(GameOverDialogPrefab);
            _gameOver.transform.SetParent(_canvas.transform, false);
        }
    }

    public void ShowYouWon()
    {
        var youWon = GameObject.Instantiate(YouWonDialogPrefab);
        youWon.transform.SetParent(_canvas.transform, false);
    }

    public void IncrementScore()
    {
        _score += 1;
        _scoreLabel.text = _score.ToString();
        PlayRandomSound(CoinSounds);

        var coins = GameObject.FindGameObjectsWithTag("coin");
        if( coins.Length == 0)
        {
            ShowYouWon();
            //KillPlayer();
        }
    }

    public void DecrementHealth()
    {
        _health -= 1;
        _healthLabel.text = _health.ToString();

        if( _health == 0)
            KillPlayer();
    }

    public void Quit()
    {
        Application.LoadLevel("title");
    }

    void KillPlayer()
    {
        var player = GameObject.Find("Player");
        player.SendMessage("KillPlayer");
    }

    void PlayRandomSound( AudioClip[] sounds)
    {
        if( sounds.Length == 0)
            return;
        int randomIndex = Random.Range(0, sounds.Length);
        _audioSource.PlayOneShot( sounds[randomIndex]);
    }
}
