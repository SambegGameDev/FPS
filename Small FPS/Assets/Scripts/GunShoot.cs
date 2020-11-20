using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    //Bullet GameObject
    public GameObject Bullet;

    //Float
    public float shootforce;
    public float upwardforce;

    //Gun States
    public float timebetshooting, spread, reloadtime, timebetshot;
    public int mag, bpt;
    public bool allowButtonHold;
    public int bulletleft, bulletshot;

    //Booleans
    public bool shooting, readytoshot, reloading;

    //Others
    public Camera fpscam;
    public Transform shootingpoint;
    public Animator ap;

    //Bug Fixing :D
    public bool allowInvoke = true;

    //Recoil
    public Rigidbody PlayerRB;
    public float recoil;

    private void Awake()
    {
        //Making sure your mag is full at the start
        bulletleft = mag;
        readytoshot = true;
    }

    private void Update()
    {
        //Calling the Input Function every time
        MyInput();
    }

    //Input Method
    void MyInput() {
        //Checking if we are allow to hold down the button 
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        //Reloading
        if (Input.GetKeyDown(KeyCode.R) && bulletleft < mag && !reloading) Reload();
        //Automatically Reload When you run out of ammo
        if (readytoshot && shooting && !reloading && bulletleft <= 0) Reload();

        //Shooting
        if (readytoshot && shooting && !reloading && bulletleft > 0) {
            //Setting bulletshots to 0
            bulletshot = 0;

            //Calling The Shoot function
            Shoot();
        }
    }
    //Shoot Function
    void Shoot() {
        readytoshot = false;

        //Finding The Position with ray
        Ray ray = fpscam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        //Checking if the ray hits something
        Vector3 targetpoint;
        if (Physics.Raycast(ray, out hit)) targetpoint = hit.point;
        else targetpoint = ray.GetPoint(75);

        //Calcutating The Direction
        Vector3 dwos = targetpoint - shootingpoint.position;

        //With Spread
        float x = Random.Range(-spread, spread), y = Random.Range(-spread, spread);
        Vector3 dws = dwos + new Vector3(x, y, 0);

        //Spawning Bullet
        GameObject bullet = Instantiate(Bullet, shootingpoint.position, Quaternion.identity);
        //Rotating the bullet
        bullet.transform.forward = dws.normalized;
        //Adding Force
        bullet.GetComponent<Rigidbody>().AddForce(dws.normalized * shootforce, ForceMode.Impulse);
        bullet.GetComponent<Rigidbody>().AddForce(fpscam.transform.up * upwardforce, ForceMode.Impulse);//Only for Bouncing Grenads
        //Adding Recoil
        PlayerRB.AddForce(-dws.normalized * recoil, ForceMode.Impulse);
        
        //Animation
        ap.SetTrigger("Shot");
        bulletleft--;
        bulletshot++;

        if (allowInvoke) {
            Invoke("ResetShot", timebetshooting);
            allowInvoke = false;
        }

        //Shotting MultipulBullets For ShotGun
        if (bulletshot > bpt && bulletleft > 0) {
            Invoke("Shoot", timebetshot);

        }
    }

    //Reseting Shot
    void ResetShot() {
        readytoshot = true;
        allowInvoke = true;
    }

    //Reload Function
    void Reload() {
        reloading = true;
        Invoke("RF", reloadtime);
        //Animation
        ap.SetTrigger("Reload");
    }

    //Finishing Reloading
    void RF() {
        bulletleft = mag;
        reloading = false;
    }
}


