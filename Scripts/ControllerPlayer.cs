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
    public Joystick joystickMove;
    public Joystick joystickFire;

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
    private Touch touch;
 
    public static List<GameObject> charger = new List<GameObject>();

    public Text metersTravelled;
    public Text munitions;
    public Text finalScore;

    private float rotationY;
    //private Rigidbody rb;

    void Start()
    {
        gunPosition = gun.transform.localPosition;
        audioData = gun.GetComponent<AudioSource>();
        audioCharger = chargerSound.GetComponent<AudioSource>();
        napalm = 250;
        //rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        meters = Mathf.RoundToInt(transform.position.z);

        if (!PauseMenu.GameIsPaused && !DetectCollision.dead)
        {      
            munitions.text = "   Napalm: " + napalm.ToString();
            metersTravelled.text = "   Score: " + meters.ToString();
            finalScore.text = "Final Score: " + meters.ToString();

            if (!PlayerCollisions.powerUp)
            {
                if (speed < 400)
                {
                    speed = 8 + meters / 200;
                }
            }

            transform.Translate(Vector3.forward * Time.deltaTime * speed);
   
            //CODICE PER LA ROTAZIONE MOUSE
            mouseInput = Input.GetAxis("Mouse X");
            rotationY += mouseInput * 1.5f;
            rotationY= Mathf.Clamp (rotationY, 60,110);
            float rotationY2 = Mathf.Clamp(rotationY, -5, 5);
            Debug.Log(rotationY);
            gun.transform.localEulerAngles = new Vector3(gun.transform.localEulerAngles.x, rotationY, gun.transform.localEulerAngles.z);
            //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, rotationY2,transform.localEulerAngles.z);
            //Camera.main.transform.localEulerAngles = new Vector3(Camera.main.transform.localEulerAngles.x, rotationY, Camera.main.transform.localEulerAngles.z);
            //FINE CODICE ROTAZIONE
            //CODICE PER LA ROTAZIONE JOYSTICK
            if (Input.GetJoystickNames().Length > 0)
            {
                mouseInput = Input.GetAxis("Joystick X");
                rotationY += mouseInput * 10f;
                rotationY = Mathf.Clamp(rotationY, 60, 110);
                gun.transform.localEulerAngles = new Vector3(gun.transform.localEulerAngles.x, rotationY, gun.transform.localEulerAngles.z);
            }
            //FINE CODICE ROTAZIONE
            horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * 1.5f);
            
            if ((Input.GetKeyDown("space") || Input.GetMouseButtonDown(0)|| Input.GetButtonDown("Fire2")) && napalm > 0)
            {
                audioData.Play(0);
            }
            if ((Input.GetKeyDown("space") || Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire2")) && napalm == 0)
            {
                audioCharger.PlayOneShot(noHammo, 1f);
            }
            if ((Input.GetKey("space") || Input.GetMouseButton(0) || Input.GetButton("Fire2")) && napalm > 0)
            {
                flameThrower.SetActive(true);
                smoke.SetActive(false);
                gun.transform.position = new Vector3(gun.transform.position.x, gun.transform.position.y, Random.Range(gun.transform.position.z - offset, gun.transform.position.z + offset));
                napalm = napalm - 1;
            }
            if ((Input.GetKeyUp("space")||Input.GetMouseButtonUp(0) || Input.GetButtonUp("Fire2")) || napalm == 0)
            {
                flameThrower.SetActive(false);
                smoke.SetActive(true);
                gun.transform.localPosition = gunPosition;
                audioData.Pause();
            }
            
#if UNITY_ANDROID
                horizontalInput = joystickMove.Horizontal;
                transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * 5f);
   
                mouseInput = joystickFire.Horizontal;
                gun.transform.Rotate(Vector3.up * Time.deltaTime * mouseInput);

                rotationY += mouseInput * 2.5f;
                rotationY= Mathf.Clamp (rotationY, 60,120);
                gun.transform.localEulerAngles = new Vector3(gun.transform.localEulerAngles.x, rotationY,gun.transform.localEulerAngles.z);
                /*
                if (gun.transform.localEulerAngles.y < 60f)
                {
                    gun.transform.rotation = new Quaternion.Euler(gun.transform.localEulerAngles.x, 60f, gun.transform.localEulerAngles.z, 1);
                    
                    Debug.Log("minore di 60: " +  gun.transform.position.y);
                }
                if (gun.transform.localEulerAngles.y > 120)
                {   
               
                    gun.transform.rotation = new Quaternion.Euler(gun.transform.localEulerAngles.x, 120f,gun.transform.localEulerAngles.z, 1);
                    Debug.Log("maggiore di 120: "+  gun.transform.position.y);
                }
                */
                if (mouseInput != 0 && napalm == 0)
                {
                    audioCharger.PlayOneShot(noHammo, 1f);
                }
                if (mouseInput != 0 && napalm > 0)
                {       
                    audioData.Play(0);
                    flameThrower.SetActive(true);
                    smoke.SetActive(false);
                    gun.transform.position = new Vector3(gun.transform.position.x, gun.transform.position.y, Random.Range(gun.transform.position.z - offset, gun.transform.position.z + offset));
                    napalm = napalm - 1;
                }
                if (mouseInput == 0 || napalm == 0)
                {
                    flameThrower.SetActive(false);
                    smoke.SetActive(true);
                    gun.transform.localPosition = gunPosition;
                    audioData.Pause();
                }
                
                
#endif
        }
        else
        {
            GetComponent<AudioSource>().Pause();
        }
    }
}
/*

*/
/*
CODICE PER LA MITRAGLIATRICE
munitions.text = " Munitions: " + (1001 - charger.Count).ToString();
Destroy(projectile, 3);
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
