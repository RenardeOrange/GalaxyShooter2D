using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txtScore;
    [SerializeField] private Image _imgPlayerHP;
    [SerializeField] private Sprite[] _spritesPlayerHP;

    private Player _player;
    void Start()
    {
        _player = FindAnyObjectByType<Player>();

        UpdateHP(_spritesPlayerHP.Length - 1);
        UpdateScore();

        GameManager.Instance.OnEnemyDestroyed += GameManager_OnEnemyDestroyed;
    }

    private void GameManager_OnEnemyDestroyed(object sender, GameManager.OnEnemyDestroyedEventArgs e)
    {
        if (e.DestroyedGameObjectTag == "Laser")
        {
            UpdateScore();
        }
        else if (e.DestroyedGameObjectTag == "Player")
        {
            UpdateHP(_player.PlayerHP);
        }
    }

    private void UpdateHP(int hp)
    {
        if (hp < 0)
        {
            hp = 0;
        }
        _imgPlayerHP.sprite = _spritesPlayerHP[hp];
    }

    private void UpdateScore()
    {
        _txtScore.text = $"Pointage : {GameManager.Instance.PlayerScore}";
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnEnemyDestroyed -= GameManager_OnEnemyDestroyed;
    }
}
