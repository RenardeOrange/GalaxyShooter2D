using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _enemySpeed = 4.0f;
    [SerializeField] private int _enemyPoints = 100;

    private SpriteRenderer _spriteRenderer;
    private float _halfSpriteWidth;
    private float _halfSpriteHeight;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _halfSpriteWidth = _spriteRenderer.bounds.extents.x;
        _halfSpriteHeight = _spriteRenderer.bounds.extents.y;
    }

    void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * _enemySpeed);
        if (transform.position.y < -Camera.main.orthographicSize - _halfSpriteHeight)
        {
            RandomPosition();
        }
    }

    private void RandomPosition()
    {
        float randomX = Random.Range(-Camera.main.orthographicSize * Camera.main.aspect + _halfSpriteWidth,
                        Camera.main.orthographicSize * Camera.main.aspect - _halfSpriteWidth);
        transform.position = new Vector3(randomX, Camera.main.orthographicSize + _halfSpriteHeight, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) {
            RandomPosition();
            return;
        }

        GameManager.Instance.EnemyDestroyed(_enemyPoints, collision.gameObject.tag);
        if(collision.gameObject.CompareTag("Laser"))
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }
}
