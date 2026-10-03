using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float _lasterSpeed = 10f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _lasterSpeed * Time.deltaTime);

        //remove gameobject once off screen
        if(transform.position.y > 10)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
