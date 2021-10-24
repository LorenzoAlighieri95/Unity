using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectCollision : MonoBehaviour
{
    private Animator zombieAnim;
    private Scene scene;
    public static bool dead = false;
    //public GameObject gun;
    public GameObject dropMunitions;

    void Start()
    {
        zombieAnim = GetComponent<Animator>();
    }

    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        /*
        if (collision.gameObject.tag=="Projectile")
        {
            Destroy(GetComponent<Collider>());
            GetComponent<MoveForward>().speed = -1;
            zombieAnim.SetTrigger("FallingBackTrigger");
            GetComponent<MoveForward>().speed = 0;
            Destroy(collision.gameObject);
            //Debug.Log("COLLISION");
        }
        */
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(die());
        }     
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Projectile")
        {
            Destroy(GetComponent<Collider>());
            GetComponent<MoveForward>().speed = -1;
            zombieAnim.SetTrigger("FallingBackTrigger");
            GetComponent<MoveForward>().speed = 0;
            
            if (Random.Range(0, 10) > 7)
            {
                Instantiate(dropMunitions, new Vector3(transform.position.x, 0, transform.position.z),dropMunitions.transform.rotation);
            }
        }
    }

    IEnumerator die()
    {
        //Debug.Log(GameObject.Find("FlameThrowerGun"));
        zombieAnim.SetTrigger("AttackTrigger");
        GameObject.Find("FlameThrowerGun").transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * 100);
        GetComponent<MoveForward>().speed = -1.25f;
        GameObject.Find("Player").GetComponent<ControllerPlayer>().speed = -1.25f;
        dead = true;
        yield return new WaitForSeconds(2.5f);
        AudioListener.pause = true;
        scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        dead = false;
        AudioListener.pause = false;
        //ControllerPlayer.charger.Clear();        
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
