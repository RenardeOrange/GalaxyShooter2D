using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _explosionSpeed = 2f;

    void Start()
    {
        Destroy(gameObject, 2.5f);
    }

    void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * _explosionSpeed);
    }
}
