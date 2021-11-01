using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    //public GameObject gunScript;
    public  Rigidbody rb;
    public  BoxCollider coll;
    private  Transform player, gunContainer, fpsCam;

    public  float pickUpRange;
    public  float dropForwardForce, dropUpwardForce;

    public  bool equipped;
    public  bool slotFull;

    private bool takeGun;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        gunContainer = GameObject.Find("GunContainer").transform;
        fpsCam = Camera.main.transform;
        /*
        if (!equipped)
        {
            // gunScript.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
        }
        if (equipped)
        {
            // gunScript.enabled = true;
            rb.isKinematic = true;
            coll.isTrigger = true;
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 distancePlayer = player.position - transform.position;
        //Debug.Log("equipped: " + equipped + " take gun: " + PlayerCollisions.takeGun);
        //if (equipped && takeGun /*Input.GetKeyDown(KeyCode.Q)*/) Drop();
        //if (!equipped && distancePlayer.magnitude <= pickUpRange /*&& PlayerCollisions.takeGun Input.GetKeyDown(KeyCode.E) */&& !slotFull) PickUp();

        //takeGun = false;


    }

    public void PickUp(GameObject gameObject)
    {
        equipped = true;
        slotFull = true;

        rb = gameObject.GetComponent<Rigidbody>();
        coll = gameObject.GetComponent<BoxCollider>();

        rb.isKinematic = true;
        coll.isTrigger = true;

        //gunScript.enabled = true;

        gameObject.transform.SetParent(gunContainer);

        if (gameObject.tag == "FlameThrowerGun") 
        {
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
            gameObject.transform.localScale = Vector3.one;

        } else if (gameObject.tag == "Gun")
        {
            gameObject.transform.localPosition = new Vector3(0,-2.9f,0);
            gameObject.transform.localRotation = Quaternion.Euler(0, -75, 0);
            gameObject.transform.localScale = new Vector3(50, 50, 50);
        }

    }

    public void Drop(GameObject gameObject)
    {
        Debug.Log("droppo");
        equipped = false;
        slotFull = false;

        gameObject.transform.SetParent(null);
        rb = gameObject.GetComponent<Rigidbody>();
        coll = gameObject.GetComponent<BoxCollider>();

        rb.isKinematic = false;
        coll.isTrigger = false;

        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);

        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);

        //gunScript.enabled = false;
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            takeGun = true;
            if (equipped)) Drop();
            Debug.Log("player collision: " + takeGun);
        }
    }
    */
}