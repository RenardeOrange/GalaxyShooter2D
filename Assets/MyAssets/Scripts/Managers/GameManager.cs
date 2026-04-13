using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public EventHandler<OnEnemyDestroyedEventArgs> OnEnemyDestroyed;
    public class OnEnemyDestroyedEventArgs : EventArgs
    {
        public string DestroyedGameObjectTag;
    }
    private int _playerScore;

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

    public void EnemyDestroyed(int points, string gameObjectTag) {
        
        if (gameObjectTag == "Laser")
        {
            _playerScore += points;
        }

        OnEnemyDestroyed?.Invoke(this, new OnEnemyDestroyedEventArgs {
            DestroyedGameObjectTag = gameObjectTag
        });
    }
}
