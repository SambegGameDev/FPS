using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFunction : MonoBehaviour
{
    //Float
    public float lifetime;

    //Paritical
    public GameObject HitParitical;
    public float particalifetime;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Destorying The Bullet If it collides with some thing
        Destroy(gameObject);
        //Making A Bullet Hit Paritical
        GameObject paritical = Instantiate(HitParitical, transform.position, Quaternion.identity);
        Destroy(paritical, particalifetime);

        //For Enemy Damage
    }
}
