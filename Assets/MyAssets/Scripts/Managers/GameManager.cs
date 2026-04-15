using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public EventHandler<OnEnemyDestroyedEventArgs> OnEnemyDestroyed;
    public class OnEnemyDestroyedEventArgs : EventArgs
    {
        public string DestroyedGameObjectTag;
    }
    private int _playerScore;
    public int PlayerScore { get => _playerScore; set => _playerScore = value; }

    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            Debug.Log("2e GameManager détruit");
        }
    }

    private void Start()
    {
        PlayerPrefs.SetInt("PlayerScore", 0);
    }

    public void EnemyDestroyed(int points, string gameObjectTag) {
        
        if (gameObjectTag == "Laser")
        {
            _playerScore += points;
        }

        OnEnemyDestroyed?.Invoke(this, new OnEnemyDestroyedEventArgs {
            DestroyedGameObjectTag = gameObjectTag
        });
    }

    public void EndGame()
    {
        PlayerPrefs.SetInt("PlayerScore", _playerScore);

        if (PlayerPrefs.HasKey("PlayerHighScore"))
        {
            if (_playerScore > PlayerPrefs.GetInt("PlayerHighScore"))
            {
                PlayerPrefs.SetInt("PlayerHighScore", _playerScore);
            }
        }
        else
        {
            PlayerPrefs.SetInt("PlayerHighScore", _playerScore);
        }

        PlayerPrefs.Save();

        SceneManager.LoadScene("End");
    }
}
