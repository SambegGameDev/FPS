using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFunction : MonoBehaviour
{
    //Float
    public float lifetime;

    //Paritical

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        
        //For Enemy Damage
    }
}
