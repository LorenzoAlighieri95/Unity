using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public float horizontalInput;
    public float mouseInput;
    public float speed = 10.0f;
    public float rotationSpeed = 30.0f;
    public float xRange;

    public GameObject projectilePrefab;
    public GameObject gun;
    public GameObject muzzle;
    public GameObject chargerSound;
    //public float fireRate=0.25f;
    //private float timer;
    //private float newTime = 0;
    private float offset = 0.015f;
    private GameObject projectile;
    private Vector3 gunPosition;
    private AudioSource audioData;
    private AudioSource audioCharger;
    public AudioClip noHammo;
    //public static int meters;
    private int meters; 
 
    public static List<GameObject> charger = new List<GameObject>();

    public Text metersTravelled;
    public Text munitions;

    void Start()
    {
        //Cursor.visible = false;

        gunPosition = gun.transform.localPosition;
        audioData = gun.GetComponent<AudioSource>();
        audioCharger = chargerSound.GetComponent<AudioSource>();
        //audioData.PlayDelayed(1000);
    }

    // Update is called once per frame
    void Update()
    {
        meters = Mathf.RoundToInt(transform.position.z);
        //Debug.Log(meters);

        if (!PauseMenu.GameIsPaused)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            mouseInput = Input.GetAxis("Mouse X");
            transform.Rotate(Vector3.up * Time.deltaTime * speed * mouseInput);
            transform.Translate(Vector3.forward * Time.deltaTime * speed /** (Time.time/10)*/);
            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);

            if (transform.position.x < -xRange)
            {
                transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
            }
            if (transform.position.x > xRange)
            {
                transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
            }
            if (transform.rotation.y < -0.1f)
            {
                transform.rotation = new Quaternion(transform.rotation.x, -0.1f, transform.rotation.z, 1);
            }
            if (transform.rotation.y > 0.05f)
            {
                transform.rotation = new Quaternion(transform.rotation.x, 0.05f, transform.rotation.z, 1);
            }

            if (Input.GetMouseButtonDown(0) && charger.Count <= 1000)
            {
                audioData.Play(0);
            } else if (Input.GetMouseButtonDown(0) && charger.Count >= 1000)
            {
                audioCharger.PlayOneShot(noHammo, 1f);
            }
            if (Input.GetMouseButton(0) && charger.Count <= 1000 )
            {
                projectile = Instantiate(projectilePrefab, gun.transform.position, transform.rotation);
                muzzle.SetActive(true);
                gun.transform.position = new Vector3(gun.transform.position.x, gun.transform.position.y, Random.Range(gun.transform.position.z - offset, gun.transform.position.z + offset));
                charger.Add(projectile);        
            }
            
            if (Input.GetMouseButtonUp(0) || charger.Count > 1000)
            {
                muzzle.SetActive(false);
                gun.transform.localPosition = gunPosition;
                audioData.Pause();
            }

            munitions.text = " Munitions: " + (1001-charger.Count).ToString();
            metersTravelled.text = " Score: " + meters.ToString();
            //meters = Mathf.RoundToInt(transform.position.z);

            Destroy(projectile, 5);

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
