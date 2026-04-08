using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _differenceFromPlayerSpeed = 5f;
    [SerializeField] private float _differenceFromCameraTop = 1f;

    private float _laserSpeed;

    private void Start()
    {
        Player player = FindAnyObjectByType<Player>();
        _laserSpeed = player.PlayerSpeed + _differenceFromPlayerSpeed;
    }

    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * _laserSpeed);

        if (transform.position.y > Camera.main.orthographicSize + _differenceFromCameraTop)
        {
            Destroy(gameObject);
        }
    }
}
