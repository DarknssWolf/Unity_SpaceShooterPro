using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 3.0f;
    private Player _player;
    private CircleCollider2D _collider;
    [SerializeField]
    private GameObject _explosionPrefab;

    private GameController _spawnManager;

    public float speed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("GameController").GetComponent<GameController>();
        if (_spawnManager == null)
        {
            Debug.Log("GameController component not found");
        }

        _collider = gameObject.GetComponent<CircleCollider2D>();
        if (_collider == null)
        {
            Debug.Log("Asteroid collider not found");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.down * speed * Time.deltaTime);

        //if (transform.position.y < -20f)
        //{
        //    float ranX = Random.Range(-8, 8);
        //    transform.position = new Vector3(ranX, 8, 0);
        //}

        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            if (_player == null)
            {
                Debug.Log("Player component not found");
            }

            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            if (_player != null)
            {
                //calls a method on another object
                _player.setLives(-1);
            }

            _spawnManager.StartSpawning();

            _collider.enabled = false;
        }

        if (other.tag == "Laser")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            Destroy(other.gameObject);

            if (_player != null)
            {
                _player.incrementScore(5);
            }

            _spawnManager.StartSpawning();

            _collider.enabled = false;
        }
        
        Destroy(this.gameObject, 0.1f);
    }
}
