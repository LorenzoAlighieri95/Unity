using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisions : MonoBehaviour
{
    public AudioClip impact;
    public AudioClip scream;
    public AudioClip reloadGun;
    public GameObject voice;
    public GameObject blood;

    private Color objectColor;

    //private Animator zombieAnim;
    //private Scene scene;

    void Start()
    {
        //StartCoroutine(FadeOutObject(blood, 2f));
        objectColor = blood.GetComponent<Renderer>().material.color;
        blood.GetComponent<Renderer>().material.color = new Color(objectColor.r, objectColor.g, objectColor.b, 0); 
    }

    void Update()
    {
        
    }
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.GetContact(0).point.x < transform.position.x)
        {
            Debug.Log("left " + " collision x: " + collision.GetContact(0).point.x + " player x: " + transform.position.x);
        } else
        {
            Debug.Log("right " + " collision x: " + collision.GetContact(0).point.x + " player x: " + transform.position.x);
        }
        if (collision.gameObject.tag == "Tree" || collision.gameObject.tag == "RandomObj")
        {
            if (!voice.GetComponent<AudioSource>().isPlaying)
            {
                voice.GetComponent<AudioSource>().PlayOneShot(impact, 0.75f);
            }
            transform.Translate(Random.Range(Random.Range(-1.5f, -1f), Random.Range(1f, 1.5f)), 0, 0);
            StartCoroutine(FadeInObject(blood, 1.5f));
            StartCoroutine(FadeOutObject(blood, 0.5f));
        } 
        if (collision.gameObject.tag == "Zombie")
        {
            voice.GetComponent<AudioSource>().PlayOneShot(scream, 0.75f);
            StartCoroutine(FadeInObject(blood, 1.5f));
            StartCoroutine(FadeOutObject(blood, 0.5f));
        }
        if (collision.gameObject.tag == "Munitions")
        {
            ControllerPlayer.charger.Clear();
            voice.GetComponent<AudioSource>().PlayOneShot(reloadGun, 0.75f);           
        }  
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
}
