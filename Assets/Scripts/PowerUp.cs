using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int _powerUpID;
    [SerializeField]
    AudioClip _powerUpClip;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -8f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                switch(_powerUpID)
                {
                    case 0:
                        player.collectPowerUp("TripleShot");
                        break;
                    case 1:
                        player.collectPowerUp("SpeedBoost");
                        break;
                    case 2:
                        player.collectPowerUp("Shield");
                        break;
                    default:
                        Debug.Log("Unknown collected value!");
                        break;
                }
                
            }

            AudioSource.PlayClipAtPoint(_powerUpClip,this.gameObject.transform.position);
            Destroy(this.gameObject);
        }
    }
}
