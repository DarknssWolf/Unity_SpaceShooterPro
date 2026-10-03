using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player _player;
    private Collider2D _collider;
    private Animator _anim;

    private AudioSource _ExplosionSource;

    public float speed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.Log("Player component not found");
        }

        _anim = gameObject.GetComponent<Animator>();
        if(_anim == null)
        {
            Debug.Log("Enemy anim not found");
        }

        _collider = gameObject.GetComponent<Collider2D>();
        if (_collider == null)
        {
            Debug.Log("Enemy collider not found");
        }

        _ExplosionSource = GetComponent<AudioSource>();

        if (_ExplosionSource == null)
        {
            Debug.LogError("Explosion source on enemy was not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < -20f)
        {
            float ranX = Random.Range(-8, 8);
            transform.position = new Vector3(ranX, 8, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (_player != null)
            {
                //calls a method on another object
                _player.setLives(-1);
            }

            _anim.SetTrigger("OnEnemyDeath");
            _collider.enabled = false;
        }

        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);

            if (_player != null)
                _player.incrementScore(10);

            _anim.SetTrigger("OnEnemyDeath");
            _collider.enabled = false;
        }

        _ExplosionSource.Play();
        Destroy(this.gameObject, 3f);
    }
}
