using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Joueur")]
    [SerializeField] private float _playerSpeed = 10f;
    public float PlayerSpeed { get => _playerSpeed; set { _playerSpeed = value; } }
    [SerializeField] private int _playerHP = 3;
    public int PlayerHP { get => _playerHP; set { _playerHP = value; } }
    [SerializeField] private float _moveMaxHeight = 0f;

    [Header("Laser")]
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private float _fireRate = 0.5f;

    private InputSystem_Actions _inputSystemActions;
    private float _canFire = 0f;
    private bool _isFiring = false;

    private float _minX, _maxX, _minY, _maxY;
    private SpriteRenderer _spriteRenderer;

    private Animator _anim;

    private void Awake()
    {
        GameManager.Instance.OnEnemyDestroyed += Instance_OnEnemyDestroy;
    }

    void Start()
    {
        _inputSystemActions = new InputSystem_Actions();
        _inputSystemActions.Player.Enable();
        //_inputSystemActions.Player.Attack.performed += Attack_performed;
        _inputSystemActions.Player.Attack.started += _ => _isFiring = true;
        _inputSystemActions.Player.Attack.canceled += _ => _isFiring = false;

        // Largeur et hauteur du joueur
        _spriteRenderer = GetComponent<SpriteRenderer>();
        float halfPlayerWidth = _spriteRenderer.bounds.extents.x;
        float halfPlayerHeight = _spriteRenderer.bounds.extents.y;

        // Position de la bordure de la camera
        Camera camera = Camera.main;
        _minX = camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + halfPlayerWidth;
        _maxX = camera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - halfPlayerWidth; // Droite écran
        _minY = camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + halfPlayerHeight;
        _maxY = _moveMaxHeight - halfPlayerHeight;

        _anim = GetComponent<Animator>();
    }

    private void Instance_OnEnemyDestroy(object sender, GameManager.OnEnemyDestroyedEventArgs e)
    {
        if(e.DestroyedGameObjectTag == "Player")
        {
            _playerHP--;
            if (_playerHP <= 0) {
                Destroy(gameObject);
                Debug.Log("Fin de partie!!!");
                SpawnManager spawnManager = FindAnyObjectByType<SpawnManager>();
                spawnManager.IsSpawning = false;
                GameManager.Instance.EndGame();
            }
        }
    }


    /*
    private void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (_canFire < Time.time) {
            Instantiate(_laserPrefab, transform.position + new Vector3(0f, 0.9f, 0f), Quaternion.identity);
            _canFire = Time.time + _fireRate;
        }
    }
    */


    private void Update()
    {
        Movement();
        Attack();
    }


    private void Movement()
    {
        Vector2 direction2D = _inputSystemActions.Player.Move.ReadValue<Vector2>();
        direction2D.Normalize();

        transform.Translate(direction2D * Time.deltaTime * _playerSpeed);

        if(direction2D.x < 0f)
        {
            _anim.SetBool("TurnLeft", true);
            _anim.SetBool("TurnRight", false);
        }
        else if (direction2D.x > 0f)
        {
            _anim.SetBool("TurnLeft", false);
            _anim.SetBool("TurnRight", true);
        }
        else
        {
            _anim.SetBool("TurnLeft", false);
            _anim.SetBool("TurnRight", false);
        }

        float clampedX = Mathf.Clamp(transform.position.x, _minX, _maxX);
        float clampedY = Mathf.Clamp(transform.position.y, _minY, _maxY);

        transform.position = new Vector2(clampedX, clampedY);
    }

    private void Attack()
    {
        if (_isFiring && _canFire < Time.time)
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0f, 0.9f, 0f), Quaternion.identity);
            _canFire = Time.time + _fireRate;
        }
    }


    private void OnDestroy()
    {
        GameManager.Instance.OnEnemyDestroyed -= Instance_OnEnemyDestroy;

        _inputSystemActions.Player.Disable();
        //_inputSystemActions.Player.Attack.performed -= Attack_performed;
        _inputSystemActions.Player.Attack.started -= _ => _isFiring = true;
        _inputSystemActions.Player.Attack.canceled -= _ => _isFiring = false;
    }
}
