using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisions : MonoBehaviour
{
    public AudioClip impact;
    public AudioClip scream;
    public AudioClip reloadGun;
    public AudioClip fallingKids;

    public GameObject voice;
    public GameObject blood;
    public GameObject fire;
    
    public static bool powerUp = false;
    private Color objectColor;
    private float prevSpeed;
    private GameObject flameThrowerGun;

    void Start()
    {
        objectColor = blood.GetComponent<Renderer>().material.color;
        blood.GetComponent<Renderer>().material.color = new Color(objectColor.r, objectColor.g, objectColor.b, 0);
        flameThrowerGun = GameObject.Find("FlameThrowerGun");
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {   
        /*
        if (collision.GetContact(0).point.x < transform.position.x)
        {
            Debug.Log("left " + " collision x: " + collision.GetContact(0).point.x + " player x: " + transform.position.x);
        } else
        {
            Debug.Log("right " + " collision x: " + collision.GetContact(0).point.x + " player x: " + transform.position.x);
        }
        */
        if ((collision.gameObject.tag == "Tree" || collision.gameObject.tag == "RandomObj") && !powerUp)
        {
            if (!voice.GetComponent<AudioSource>().isPlaying)
            {
                voice.GetComponent<AudioSource>().PlayOneShot(impact, 0.75f);
            }
            if (ControllerPlayer.napalm >= 50) {
                ControllerPlayer.napalm = ControllerPlayer.napalm - 50;
            }
            //transform.Translate(Random.Range(Random.Range(-1.5f, -1f), Random.Range(1f, 1.5f)), 0, 0);
            StartCoroutine(FadeInObject(blood, 1.5f));
            StartCoroutine(FadeOutObject(blood, 0.5f));
        } 
        if (collision.gameObject.tag == "Zombie" && !powerUp)
        {
            if (!voice.GetComponent<AudioSource>().isPlaying)
            {
                voice.GetComponent<AudioSource>().PlayOneShot(scream, 1f);
            }
            StartCoroutine(FadeInObject(blood, 1.5f));
        }
        /*
        if (collision.gameObject.tag == "Munitions")
        {
            //ControllerPlayer.charger.Clear();
            ControllerPlayer.napalm = 500;
            voice.GetComponent<AudioSource>().PlayOneShot(reloadGun, 1f);           
        }
        if (collision.gameObject.tag == "DroppedMunitions")
        {
            if (ControllerPlayer.napalm <= 450)
            {
                ControllerPlayer.napalm += 50;
                voice.GetComponent<AudioSource>().PlayOneShot(reloadGun, 1f);
            } else if (ControllerPlayer.napalm > 450)
            {
                ControllerPlayer.napalm = 500;
                voice.GetComponent<AudioSource>().PlayOneShot(reloadGun, 1f);
            }
        }
        if (collision.gameObject.tag == "PowerUp" && !powerUp)
        {
            StartCoroutine(PowerUp());
        }
        */
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Munitions"))
        {
            //ControllerPlayer.charger.Clear();
            ControllerPlayer.napalm = 500;
            voice.GetComponent<AudioSource>().PlayOneShot(reloadGun, 1f);
        }
        if (other.CompareTag("DroppedMunitions")) 
        {
            if (ControllerPlayer.napalm <= 450)
            {
                ControllerPlayer.napalm += 50;
                voice.GetComponent<AudioSource>().PlayOneShot(reloadGun, 1f);
            }
            else if (ControllerPlayer.napalm > 450)
            {
                ControllerPlayer.napalm = 500;
                voice.GetComponent<AudioSource>().PlayOneShot(reloadGun, 1f);
            }
        }
        if (other.CompareTag("PowerUp") && !powerUp)
        {
            StartCoroutine(PowerUp());
        }
    }

    IEnumerator PowerUp()
    {
        flameThrowerGun.SetActive(false);
        voice.GetComponent<AudioSource>().PlayOneShot(fallingKids, 0.5f);
        powerUp = true;
        fire.SetActive(true);
        //GetComponent<BoxCollider>().enabled = false;
        prevSpeed = GetComponent<ControllerPlayer>().speed;
        GetComponent<ControllerPlayer>().speed = prevSpeed*5;
        yield return new WaitForSeconds(18);
        flameThrowerGun.SetActive(true);
        GetComponent<ControllerPlayer>().speed = prevSpeed;
        //GetComponent<BoxCollider>().enabled = true;
        fire.SetActive(false);
        powerUp = false;
    }

    IEnumerator FadeOutObject(GameObject obj, float fadeSpeed)
    {
        while (obj.GetComponent<Renderer>().material.color.a > 0)
        {
            objectColor = obj.GetComponent<Renderer>().material.color;
            float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

            obj.GetComponent<Renderer>().material.color = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            yield return null;
        }
        obj.SetActive(false);
    }

    IEnumerator FadeInObject(GameObject obj, float fadeSpeed)
    {
        obj.SetActive(true);
        while (obj.GetComponent<Renderer>().material.color.a < 1)
        {
            objectColor = obj.GetComponent<Renderer>().material.color;
            float fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

            obj.GetComponent<Renderer>().material.color = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            yield return null;
        }
    }

    /*
    IEnumerator waiter()
    {
        zombieAnim.SetTrigger("AttackTrigger");
        GetComponent<MoveForward>().speed = -1;
        GameObject.Find("Player").GetComponent<ControllerPlayer>().speed = -1;
        yield return new WaitForSeconds(2.5f);
        Time.timeScale = 0;
        AudioListener.pause = true;
        scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        Time.timeScale = 1.0f;
        AudioListener.pause = false;
    }
    */
    /*
    private void OnTriggerEnter(Collider other)
    {
            transform.Translate(Random.Range(Random.Range(-1.5f, -1f), Random.Range(1f, 1.5f)), 0, 0);
            if (!voice.GetComponent<AudioSource>().isPlaying)
                voice.GetComponent<AudioSource>().PlayOneShot(impact, 0.75f);
        if (other.CompareTag("Zombie"))
        {
            voice.GetComponent<AudioSource>().PlayOneShot(scream, 0.75f);
        } else if (other.CompareTag("Munitions"))
        {
            ControllerPlayer.charger.Clear();
            //Debug.Log("Ricarica. Munizioni: " + ControllerPlayer.charger.Count);
        }
    }
    */
}
