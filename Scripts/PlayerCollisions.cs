using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    public AudioClip impact;
    public AudioClip scream;
    public GameObject voice;    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tree"))
        {
            //transform.Translate(Random.Range(0, 1), transform.position.y, transform.position.z);
            transform.Translate(Random.Range(-1, 1), 0, 0);
            GetComponent<AudioSource>().PlayOneShot(impact, 0.75f);
        }
    }
    */
    
    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Tree"))
        //{
            transform.Translate(Random.Range(Random.Range(-1.5f, -1f), Random.Range(1f, 1.5f)), 0, 0);
            if (!voice.GetComponent<AudioSource>().isPlaying)
                voice.GetComponent<AudioSource>().PlayOneShot(impact, 0.75f);
        //}
        if (other.CompareTag("Zombie"))
        {
            voice.GetComponent<AudioSource>().PlayOneShot(scream, 0.75f);
        } else if (other.CompareTag("Munitions"))
        {
            ControllerPlayer.charger.Clear();
            //Debug.Log("Ricarica. Munizioni: " + ControllerPlayer.charger.Count);
        }
    }

    IEnumerator waiter()
    {
        transform.Translate(Random.Range(Random.Range(-1.5f, -1f), Random.Range(1f, 1.5f)), 0, 0);
        yield return new WaitForSeconds(0.2f);
        if (!GetComponent<AudioSource>().isPlaying)
            GetComponent<AudioSource>().PlayOneShot(impact, 0.75f);
    }

}
