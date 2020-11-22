//Some Idiot Making a enemy 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Script : MonoBehaviour
{
    //Varibles
    public NavMeshAgent agent;
    public Transform Player;
    public LayerMask WhatIsGround, WhoIsPlayer;

    //Petrolling Varibles
    public Vector3 walkPoint;
    bool walkpointset;
    public float walkpointrange;

    //Attacking
    public float timebetwAttacks;
    bool AlreadyAttacked;
    public GameObject EnemyBullets;
    public float forwordforce, upwardforce;

    //States
    public float sightRange, Attackingrange;
    public bool PlayerInSightRange, PlayerInAttackRange;

    //Health
    public float health;

    //Stup For Start
    private void Awake()
    {
        Player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    //Update During Play
    private void Update()
    {
        //Checking If our player in sight and attack range of the enemy
        PlayerInSightRange = Physics.CheckSphere(transform.position, sightRange, WhoIsPlayer);
        PlayerInAttackRange = Physics.CheckSphere(transform.position, Attackingrange, WhoIsPlayer);

        //StateMachine
        if (!PlayerInAttackRange && !PlayerInSightRange) Pertol();
        if (!PlayerInAttackRange && PlayerInSightRange) Chase();
        if (PlayerInAttackRange && PlayerInSightRange) Attack();
    }

    //All The Funciton -- Start

    //Petrol
    void Pertol() {
        //IF there is no walk point than search for one
        //if (!walkpointset) SearchWalkPoint();

        //Setting the destination while petrolling
       // if (walkpointset) agent.SetDestination(walkPoint);

        //Calculatng the distance to the walk point
       // Vector3 dtwp = transform.position - walkPoint;

        //Desitnation Reached
        ///if (dtwp.magnitude < 1f) walkpointset = false;
    }
    //Searching Walk Point
    void SearchWalkPoint() {
        /////Randomizing a Number
        //float randomZ = Random.Range(-walkpointrange, walkpointrange);
        //float randomX = Random.Range(-walkpointrange, walkpointrange);

        //Setting a Vector3
        //walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        //Checking if the walkpoint is in the ground
        //if (Physics.Raycast(walkPoint, -transform.up, 2f, WhatIsGround)) {
            //Setting the bolean to true
        //    walkpointset = true;
        //}
    }

    //Chase
    void Chase() {
        //Making the enemy chase player
        agent.SetDestination(Player.position);
    }

    //Attack
    void Attack() {
        //First Making the enemy stop
        agent.SetDestination(transform.position);
        transform.LookAt(Player);

        //Checking If enemy has already attacked
        if (!AlreadyAttacked) {
            //Shooting Code
            GameObject bullet = Instantiate(EnemyBullets, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * forwordforce, ForceMode.Impulse);
            bullet.GetComponent<Rigidbody>().AddForce(transform.up * upwardforce, ForceMode.Impulse);
            
            AlreadyAttacked = true;
            Invoke(nameof(ResetAttack), timebetwAttacks);
        }
    }
    void ResetAttack() {
        AlreadyAttacked = false;
    }
    //-- Stop

    //Dead System for the emeny
    public void Dead() {
        Destroy(gameObject);
    }
}
