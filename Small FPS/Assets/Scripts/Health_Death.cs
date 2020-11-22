//Some Idiot Making a Health and Death System Don't Mind Me
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Death : MonoBehaviour
{
    //Varibles
    public float health;
    public float MaxYDistance;
    public GameObject DeathScreen;
    public bool IsDead;

    //Setup For The Start
    private void Start(){
        if (this.transform.position.y <= MaxYDistance) IsDead = true;
        else IsDead = false;
    }

    //All The Health Mechincs
    private void Update(){
        //Checking if You have any health
        if (health <= 0) IsDead = true;

        //Checking If You Are Dead And if You are then calling a function
        if (IsDead) DeadFunction();
        else DeathScreen.SetActive(false);
    }

    //The Dead System
    void DeadFunction() {
        //Enabling The Death Screen
        DeathScreen.SetActive(true);
    }

    //Collison
    private void OnCollisionEnter(Collision collision){
        //Checking if The Player gets hit by a bullet
        if (collision.gameObject.CompareTag("Enemy")) DeadFunction();
    }
}
