using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private float _spawnIntervalMin = 3f;
    [SerializeField] private float _spawnIntervalMax = 6f;
    [SerializeField] private float _spawnIntervalInitial = 3f;

    private SpriteRenderer _spriteRenderer;
    private float _halfSpriteWidth;
    private float _halfSpriteHeight;
    private bool _isSpawning = true;
    public bool IsSpawning { get { return _isSpawning; } set { _isSpawning = value; } }

    private void Start()
    {
        StartCoroutine(SpawnEnemyCoroutine());
        _spriteRenderer = _enemyPrefab.GetComponent<SpriteRenderer>();
        _halfSpriteWidth = _spriteRenderer.bounds.extents.x;
        _halfSpriteHeight = _spriteRenderer.bounds.extents.y;
    }

    IEnumerator SpawnEnemyCoroutine()
    {
        yield return new WaitForSeconds(_spawnIntervalInitial);

        while (_isSpawning)
        {
            float randomX = Random.Range(-Camera.main.orthographicSize * Camera.main.aspect + _halfSpriteWidth,
                        Camera.main.orthographicSize * Camera.main.aspect - _halfSpriteWidth);
            Vector3 spawnPosition = transform.position = new Vector3(randomX, Camera.main.orthographicSize + _halfSpriteHeight, 0f);

            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(Random.Range(_spawnIntervalMin, _spawnIntervalMax));
        }
        
    }
}
