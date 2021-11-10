using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerPlayer : MonoBehaviour
{
    public float speed;
    public float rangeRotationCam = 2.5f;
    public static int meters;

    public float horizontalInput;
    public float mouseInput;
    public Joystick joystickMove;
    public Joystick joystickFire;
    public float joystickInput; 
 
    public Text scoreText;
    public Text munitions;
    public Text finalScore;

    private float rotationY = 0;
    private float rotationY2 = 0;

    public static int hammo;
    public static int score;
    public static int killPoints;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        meters = Mathf.RoundToInt(transform.position.z);

        if (!PauseMenu.GameIsPaused && /*!DetectCollision.dead*/ !PlayerCollisions.dead)
        {
            score = meters + killPoints;
            munitions.text = "   Hammo: " + hammo.ToString();
            scoreText.text = "   Score: " + score.ToString();
            //scoreText.text = "   Speed: " + speed.ToString();
            finalScore.text = "Final Score: " + score.ToString();

            if (!PlayerCollisions.powerUp)
            {
                if (speed < 100)
                {
                    speed = 8 + meters / 250;
                }
            }

            transform.Translate(Vector3.forward * Time.deltaTime * speed);
   
            //CODICE PER LA ROTAZIONE MOUSE
            mouseInput = Input.GetAxis("Mouse X");
            //rotationY += mouseInput * 1.5f;
            //rotationY = Mathf.Clamp(rotationY, 70, 110);
            if (mouseInput != 0)
            {
                rotationY += Mathf.Clamp(mouseInput, -rangeRotationCam, rangeRotationCam);
                rotationY = Mathf.Clamp(rotationY, -rangeRotationCam, rangeRotationCam);
                Camera.main.transform.localEulerAngles = new Vector3(Camera.main.transform.localEulerAngles.x, rotationY, Camera.main.transform.localEulerAngles.z);
            }
            //FINE CODICE ROTAZIONE

            //CODICE PER LA ROTAZIONE JOYSTICK
            if (Input.GetJoystickNames().Length > 0)
            {
                joystickInput = Input.GetAxis("Joystick X");
                if (joystickInput != 0)
                {
                    rotationY2 += Mathf.Clamp(joystickInput, -rangeRotationCam, rangeRotationCam);
                    rotationY2 = Mathf.Clamp(rotationY2, -rangeRotationCam, rangeRotationCam);
                    Camera.main.transform.localEulerAngles = new Vector3(Camera.main.transform.localEulerAngles.x, rotationY2, Camera.main.transform.localEulerAngles.z);
                }
            }
            //FINE CODICE ROTAZIONE

            horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * 2f);

 /*          
#if UNITY_ANDROID
                horizontalInput = joystickMove.Horizontal;
                transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * 5f);
   
                mouseInput = joystickFire.Horizontal;
                gun.transform.Rotate(Vector3.up * Time.deltaTime * mouseInput);

                rotationY += mouseInput * 2.5f;
                rotationY= Mathf.Clamp (rotationY, 60,120);
                gun.transform.localEulerAngles = new Vector3(gun.transform.localEulerAngles.x, rotationY,gun.transform.localEulerAngles.z);
                
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
 */
        }
        else
        {
            GetComponent<AudioSource>().Pause();
        }
    }
}