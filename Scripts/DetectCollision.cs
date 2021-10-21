using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectCollision : MonoBehaviour
{
    private Animator zombieAnim;
    private Scene scene;
    public static bool dead = false;
    public GameObject gun;

    void Start()
    {
        zombieAnim = GetComponent<Animator>();
    }

    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Projectile")
        {
            Destroy(GetComponent<Collider>());
            GetComponent<MoveForward>().speed = -1;
            zombieAnim.SetTrigger("FallingBackTrigger");
            GetComponent<MoveForward>().speed = 0;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(die());
        }     
    }
    
    IEnumerator die()
    { 
        zombieAnim.SetTrigger("AttackTrigger");
        GameObject.Find("AutomaticRifle").transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * 5);
        GetComponent<MoveForward>().speed = -1.25f;
        GameObject.Find("Player").GetComponent<ControllerPlayer>().speed = -1.25f;
        dead = true;
        yield return new WaitForSeconds(2.5f);
        //Time.timeScale = 0;
        AudioListener.pause = true;
        scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        //Time.timeScale = 1.0f;
        dead = false;
        AudioListener.pause = false;
        ControllerPlayer.charger.Clear();
        
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
