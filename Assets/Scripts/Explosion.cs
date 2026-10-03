using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    AudioSource _explosionSource;
    // Start is called before the first frame update
    void Start()
    {
        _explosionSource = gameObject.GetComponent<AudioSource>();
        if (_explosionSource == null)
        {
            Debug.Log("Explosion source on asteroid not found");
        }

        _explosionSource.Play();
        Destroy(this.gameObject, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
