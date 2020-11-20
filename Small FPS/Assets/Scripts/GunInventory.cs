//Some Idiot Making a Inventory For Guns
using UnityEngine;

public class GunInventory : MonoBehaviour
{
    //Public
    public Transform player, GunContainer, fpscam;
    public float pickuprange, dropforwordforce, dropupwordforce;
    public bool equiped;
    public static bool slotfull;
    public GunShoot GunScript;
    public Rigidbody rb;
    public BoxCollider coll;

    //Setup for the start
    private void Start()
    {
        //Seeing if Any gun is equiped ---Start
        if (!equiped) {
            GunScript.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
        }
        if (equiped) {
            GunScript.enabled = true;
            rb.isKinematic = true;
            coll.isTrigger = true;
        }

        //Setting up Private Variables
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<BoxCollider>();
        //----Stop
    }

    //Update Method
    private void Update()
    {
        //Caculating the distance between player and gun
        Vector3 distance = player.position - transform.position;
        
        //Pickup is These Condition are met
        if (!equiped && distance.magnitude <= pickuprange && Input.GetKeyDown(KeyCode.E) && !slotfull){
            Pick();
        }
        if (equiped && Input.GetKeyDown(KeyCode.Q)) {
            Drop();
        }
    }

    //Pick Up Gun Function
    void Pick() {
        //Slot Full and Gun Is Equiped
        equiped = true;
        slotfull = true;

        //Making The RB's kinamatics off and makng the coll triggered
        rb.isKinematic = true;
        coll.isTrigger = true;
        //Moving The Gun's position 
        transform.SetParent(GunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        //Enabeling The Gun Script
        GunScript.enabled = true;

    }

    //Drop Gun function
    void Drop() {
        //Slot Not Full and Gun Is Not Equiped
        equiped = false;
        slotfull = false;

        //Making The RB's kinamatics no and makng the coll untriggered
        rb.isKinematic = false;
        coll.isTrigger = false;
        //DisAbling single position
        transform.SetParent(null);

        //Making the gun carry the movementum of the player
        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        //Adding the force when thrown
        rb.AddForce(fpscam.forward * dropforwordforce, ForceMode.Impulse);
        rb.AddForce(fpscam.up * dropupwordforce, ForceMode.Impulse);

        //Adding Random Rotation
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);

        //DisAbling The Gun Script
        GunScript.enabled = false;

     }
}
