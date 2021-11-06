using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public GunScript gunScript;
    public  Rigidbody rb;
    public  BoxCollider coll;
    private  Transform player, gunContainer, fpsCam;

    public  float pickUpRange;
    public  float dropForwardForce, dropUpwardForce;

    public  bool equipped;
    //public  bool slotFull;

    private bool takeGun;

    public GameObject chargerSound;
    public AudioClip switchGun;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        gunContainer = GameObject.Find("GunContainer").transform;
        fpsCam = Camera.main.transform;
        //gunScript = GetComponent<GunScript>();
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
        chargerSound.GetComponent<AudioSource>().PlayOneShot(switchGun, 0.5f);
        equipped = true;
        //slotFull = true;

        rb = gameObject.GetComponent<Rigidbody>();
        coll = gameObject.GetComponent<BoxCollider>();

        rb.isKinematic = true;
        coll.enabled = false;

        GetComponent<GunScript>().enabled = true;
        GetComponent<RotateGunInAir>().enabled = false;

        gameObject.transform.SetParent(gunContainer);

        if (gameObject.tag == "FlameThrowerGun") 
        {
            gameObject.transform.localPosition = new Vector3(-35,-3.5f,14f);
            gameObject.transform.localRotation = Quaternion.Euler(0,-0.95f,0);
            gameObject.transform.localScale = Vector3.one;

        } else if (gameObject.tag == "Gun")
        {
            //gameObject.transform.localPosition = new Vector3(-30,-6,11);
            //gameObject.transform.localRotation = Quaternion.Euler(0, -97, 0);
            //gameObject.transform.localScale = new Vector3(55, 50, 50);
            // NUOVA ARMA
            gameObject.transform.localPosition = new Vector3(22,1.7f,0);
            gameObject.transform.localRotation = Quaternion.Euler(0, -90, 0);
            gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
           
        }

    }

    public void Drop(GameObject gameObject)
    {
        
        equipped = false;
        //slotFull = false;

        gameObject.transform.SetParent(null);
        rb = gameObject.GetComponent<Rigidbody>();
        coll = gameObject.GetComponent<BoxCollider>();

        rb.isKinematic = false;
        coll.enabled = true;

        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);

        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);

        GetComponent<GunScript>().enabled = false;


        //Debug.Log(tag + " " +GetComponent<GunScript>().enabled);
    }
}