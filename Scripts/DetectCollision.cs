using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectCollision : MonoBehaviour
{
    private Animator zombieAnim;
    private Scene scene;

    void Start()
    {
        zombieAnim = GetComponent<Animator>();
    }

    void Update()
    {

    }

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
}
