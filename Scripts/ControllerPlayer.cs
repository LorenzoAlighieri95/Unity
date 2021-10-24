using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public float horizontalInput;
    public float mouseInput;
    public float speed = 5;
    public float rotationSpeed = 30.0f;
    public float xRange;

    public GameObject projectilePrefab;
    public GameObject gun;
    //public GameObject muzzle;
    public GameObject chargerSound;
    public GameObject flameThrower;
    public GameObject smoke;
    //public float fireRate=0.25f;
    //private float timer;
    //private float newTime = 0;
    private float offset = 0.015f;
    private GameObject projectile;
    private Vector3 gunPosition;
    private AudioSource audioData;
    private AudioSource audioCharger;
    public AudioClip noHammo;
    public static int meters;
    public static int napalm;
 
    public static List<GameObject> charger = new List<GameObject>();

    public Text metersTravelled;
    public Text munitions;

    //private Rigidbody rb;

    void Start()
    {
        gunPosition = gun.transform.localPosition;
        audioData = gun.GetComponent<AudioSource>();
        audioCharger = chargerSound.GetComponent<AudioSource>();
        napalm = 250;
        //rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        meters = Mathf.RoundToInt(transform.position.z);

        if (!PauseMenu.GameIsPaused && !DetectCollision.dead)
        {
            //munitions.text = " Munitions: " + (1001 - charger.Count).ToString();
            munitions.text = " Napalm: " + napalm.ToString();
            metersTravelled.text = " Score: " + meters.ToString();
            //Destroy(projectile, 3);

            horizontalInput = Input.GetAxis("Horizontal");
            //mouseInput = Input.GetAxis("Mouse X");
            //transform.Rotate(Vector3.up * Time.deltaTime * speed * mouseInput);

            transform.Translate(Vector3.forward * Time.deltaTime * speed /** (Time.time/10)*/);
            //rb.AddForce(0,0,speed*Time.deltaTime);
            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * 1.5f);

            if (!PlayerCollisions.powerUp)
            {
                speed = 8 + meters / 200;
            }
            
            //Debug.Log(speed);
            /*
            if (meters < 500)
            {
                speed = 10;
            }
            else if (meters > 500 &&  meters < 1000)
            {
                speed = 15;
            }
            else if (meters > 1000 && meters < 2000)
            {
                speed = 20;
            }
            else if (meters > 2000 && meters <3000)
            {
                speed = 25;
            }
            else if (meters > 3000 && meters < 4000)
            {
                speed = 27;
            }
            else if (meters > 4000)
            {
                speed = 30;
            }
            */

            /*
            if (transform.position.x < -xRange)
            {
                transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
            }
            if (transform.position.x > xRange)
            {
                transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
            }
            */
            if (transform.rotation.y < -0.1f)
            {
                transform.rotation = new Quaternion(transform.rotation.x, -0.1f, transform.rotation.z, 1);
            }
            if (transform.rotation.y > 0.05f)
            {
                transform.rotation = new Quaternion(transform.rotation.x, 0.05f, transform.rotation.z, 1);
            }

            if (Input.GetKeyDown("space") && napalm > 0)
            {
                audioData.Play(0);
            }
            else if (Input.GetKeyDown("space") && napalm == 0)
            {
                audioCharger.PlayOneShot(noHammo, 1f);    
            }
            if (Input.GetKey("space") && napalm > 0)
            {
                //projectile = Instantiate(projectilePrefab, gun.transform.position, transform.rotation);
                flameThrower.SetActive(true);
                smoke.SetActive(false);
                gun.transform.position = new Vector3(gun.transform.position.x, gun.transform.position.y, Random.Range(gun.transform.position.z - offset, gun.transform.position.z + offset));
                //charger.Add(projectile);
                //munitionCount++;
                napalm=napalm-1;
            }

            if (Input.GetKeyUp("space") || napalm == 0)
            {
                //muzzle.SetActive(false);
                flameThrower.SetActive(false);
                smoke.SetActive(true);
                gun.transform.localPosition = gunPosition;
                audioData.Pause();
            }
            /*
            CODICE PER LA MITRAGLIATRICE
            if ( Input.GetKeyDown("space") && charger.Count <= 1000)
            {
                audioData.Play(0);
            } else if (Input.GetKeyDown("space") && charger.Count >= 1000)
            {
                audioCharger.PlayOneShot(noHammo, 1f);
            }
            if (Input.GetKey("space") && charger.Count <= 1000 )
            {
                projectile = Instantiate(projectilePrefab, gun.transform.position, transform.rotation);
                muzzle.SetActive(true);
                gun.transform.position = new Vector3(gun.transform.position.x, gun.transform.position.y, Random.Range(gun.transform.position.z - offset, gun.transform.position.z + offset));
                charger.Add(projectile);
                munitionCount++;
                Debug.Log(munitionCount);
            }
            
            if (Input.GetKeyUp("space") || charger.Count > 1000)
            {
                muzzle.SetActive(false);
                gun.transform.localPosition = gunPosition;
                audioData.Pause();
            }
            */
        } else
        {
            GetComponent<AudioSource>().Pause();
        }

        /*
        if (Input.GetMouseButton(0)) 
        {             
            if (Input.GetMouseButtonDown(0) && Time.time >= newTime+ 2 ){
                timer = Time.time;
            }
            Debug.Log("Tempo: " + Time.time + " timer: " + timer + 3 + " newTime: " + newTime + 2);
            if (Time.time >= timer && Time.time <= timer + 3 && Time.time >= newTime+ 2 ){
                projectile = Instantiate(projectilePrefab, gun.transform.position, transform.rotation);
                muzzle.SetActive(true); 
                gun.transform.position = new Vector3 (gun.transform.position.x,gun.transform.position.y,Random.Range(gun.transform.position.z - offset, gun.transform.position.z + offset));
            } else {
                muzzle.SetActive(false);
                gun.transform.localPosition = gunPosition;
                newTime = Time.time;
            }
        }
        Destroy (projectile, 6);
        if (Input.GetMouseButtonUp(0)){
            muzzle.SetActive(false);
            gun.transform.localPosition = gunPosition;
        }
        */
    }


}
