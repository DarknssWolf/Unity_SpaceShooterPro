using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _speedMultiplier = 1.5f;
    [SerializeField] //shows in inspector without allowing it to change by others
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _shieldVisualizer;
    [SerializeField]
    private GameObject _rightEngine;
    [SerializeField]
    private GameObject _leftEngine;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private int _cooldown = 3;
    private AudioSource _laserSource;
    [SerializeField]
    private AudioClip _laserClip;
    [SerializeField]
    private AudioClip _explosion;
    private GameController _spawnManager; //fetch object in start
    private float _canFire = -1f;
    private bool _tripleShot = false;
    private bool _speedBosstActive = false;
    private bool _shieldActive = false;

    [SerializeField]
    private int _playerScore = 0;
    private UI_Manager _uiManager;

    public float fireRate = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("GameController").GetComponent<GameController>();

        if (_spawnManager == null)
        {
            Debug.LogError("GameController was not found");
        }

        _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();

        if (_uiManager == null)
        {
            Debug.LogError("UI Manager was not found");
        }

        _laserSource = GetComponent<AudioSource>();

        if(_laserSource == null)
        {
            Debug.LogError("laser source on player was not found");
        }
        else
        {
            _laserSource.clip = _laserClip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        calculateMovement();
        playerShoot();
    }

    void playerShoot()
    {
        if(Input.GetKey(KeyCode.Space) && Time.time > _canFire)
        {
            _canFire = Time.time + fireRate;

            if (_tripleShot)
            {
                Instantiate(_tripleShotPrefab, transform.position + new Vector3(-0.7f, 0.14f, 0), Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
            }

            _laserSource.Play();
        }
    }

    void calculateMovement()
    {
        float y = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");

        if (_speedBosstActive == false)
        {
            transform.Translate(new Vector3(x, y, 0) * _speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(new Vector3(x, y, 0) * (_speed * _speedMultiplier) * Time.deltaTime);
        }

        //set bounds on y
        if (transform.position.y >= 4)
        {
            transform.position = new Vector3(transform.position.x, 4, 0);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }

        //Can also use this to set bounds by using a MathF function called clamp which allows you to set min and max values
        //transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4f, 4f), 0);

        //set bounds for x
        if (transform.position.x > 9)
        {
            transform.position = new Vector3(-9, transform.position.y, 0);
        }
        else if (transform.position.x < -9)
        {
            transform.position = new Vector3(9, transform.position.y, 0);
        }
    }

    public void setLives(int life)
    {
        if(_shieldActive && life < 0)
        {
            _shieldActive = false;
            _shieldVisualizer.SetActive(_shieldActive);
            return;
        }

        _lives += life;

        if (_lives == 2)
        {
            _leftEngine.SetActive(true);
        }
        else if (_lives == 1)
        {
            _rightEngine.SetActive(true);
        }
        
        _uiManager.updateLives(_lives);
        if (_lives < 1)
        {
            _spawnManager.PlayerDeath();
            //Always destroy an object after
            Destroy(this.gameObject);
        }
    }

    public void collectPowerUp(string type)
    {
        Debug.Log("Collected Powerup");
        if (type == "TripleShot")
        {
            _tripleShot = true;
            StartCoroutine(CooldownRoutineTripleShot());
        }
        else if (type == "SpeedBoost")
        {
            _speedBosstActive = true;
            StartCoroutine(CooldownRoutineSpeedBoost());
        }
        else if (type == "Shield")
        {
            _shieldActive = true;
            _shieldVisualizer.SetActive(_shieldActive);
            StartCoroutine(CooldownRoutineShield());
        }
    }

    public void incrementScore(int value)
    {
        _playerScore += value;
        _uiManager.updateScore(_playerScore);
    }

    IEnumerator CooldownRoutineTripleShot()
    {
        Debug.Log("Cooldown TripleShot enabled");
        yield return new WaitForSeconds(_cooldown);
        Debug.Log("Cooldown done");
        _tripleShot = false;
    }

    IEnumerator CooldownRoutineSpeedBoost()
    {
        Debug.Log("Cooldown SpeedBoost enabled");
        yield return new WaitForSeconds(_cooldown);
        Debug.Log("Cooldown done");
        _speedBosstActive = false;
    }

    IEnumerator CooldownRoutineShield()
    {
        Debug.Log("Cooldown Shield enabled");
        yield return new WaitForSeconds(_cooldown);
        Debug.Log("Cooldown done");
        _shieldActive = false;
        _shieldVisualizer.SetActive(_shieldActive);
    }
}
