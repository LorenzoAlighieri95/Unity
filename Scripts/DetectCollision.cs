using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectCollision : MonoBehaviour
{
    private Animator zombieAnim;
    private Scene scene;
    private Quaternion originalRotation;
    public static bool dead = false;
    //public GameObject gun;
    public GameObject dropMunitions;


    void Start()
    {
        zombieAnim = GetComponent<Animator>();
        originalRotation = transform.rotation;
    }

    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Projectile")
        { 
            ZombieDies();
        }
        
        if (collision.gameObject.tag == "Player")
        {
            if (!PlayerCollisions.powerUp)
            {
                StartCoroutine(die());             
                //transform.rotation = Quaternion.Slerp(transform.rotation, originalRotation, Time.time * 1.0f);
            } else
            {
                zombieAnim.SetTrigger("FallingBackTrigger");
            }
        } else
        {
           transform.Rotate(0, Random.Range(-5,5), 0, Space.Self);
        }
        if (collision.gameObject.tag == "Wall")
        {
            gameObject.SetActive(false);
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Projectile")
        {
            ZombieDies();
        }
    }

    IEnumerator die()
    {
        zombieAnim.SetTrigger("AttackTrigger");
        GameObject.Find("GunContainer").transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * 100);
        GetComponent<MoveForward>().speed = -0.5f;
        GameObject.Find("Player").GetComponent<ControllerPlayer>().speed = -0.5f;
        dead = true;
        yield return new WaitForSeconds(2.5f);
        AudioListener.pause = true;
        scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        dead = false;
        AudioListener.pause = false;            
    }

    public void ZombieDies()
    {
        //GetComponent<MoveForward>().speed = -5;
        ControllerPlayer.killPoints += 50;
        zombieAnim.SetTrigger("FallingBackTrigger");
        GetComponent<MoveForward>().speed = 0;
        GetComponent<Collider>().enabled = false;
        if (Random.Range(0, 10) > 8)
        {
            Instantiate(dropMunitions, new Vector3(transform.position.x, 0.2f, transform.position.z), dropMunitions.transform.rotation);
        }
    }
    /*
private void OnTriggerEnter(Collider other)
{   
    if (other.CompareTag("Projectile")){
        Destroy(GetComponent<Collider>());
        GetComponent<MoveForward>().speed = -1;
        zombieAnim.SetTrigger("FallingBackTrigger");
        GetComponent<MoveForward>().speed = 0;
        Destroy(other.gameObject);     
    } else if (other.CompareTag("Player")){
        StartCoroutine(waiter());
    }
}
*/

}
