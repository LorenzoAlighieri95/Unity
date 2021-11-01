using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5;
    private GameObject player;
    private Transform playerTransform;
    //private float maxDist = 50;
    //private float minDist = 0;
    //public GameObject zombie;
    //private Vector3 vector;
    void Start()
    {
        player = GameObject.Find("Player");
        playerTransform = player.transform;
    }

    // Update is called once per frame
    void Update()
    {

        /*
        if (Random.Range(0, 10) < 2)
        {
            if (playerTransform.position.z +1 < transform.position.z)
            {
                //Debug.Log("prev: " + transform.rotation);
                transform.LookAt(playerTransform);
                //Debug.Log("adter:" + transform.rotation);
            }
        }
        */
        //transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z));
        
        if (transform.position.z < playerTransform.position.z + 10 && transform.position.x > transform.position.x - 5 && transform.position.x < transform.position.x + 5)
        {
            //Debug.Log("pre: " +transform.rotation);
            transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y,playerTransform.position.z));
            //Debug.Log("after: "+transform.rotation);
        }
        
        //transform.position += transform.forward * speed * Time.deltaTime;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        /*
        if (Vector3.Distance(transform.position, playerTransform.position) >= minDist)
         {

             
             Debug.Log("player position: " + playerTransform.position + " zombie position: " + transform.position);

             
             if (Vector3.Distance(transform.position, playerTransform.position) <= MaxDist)
             {
                 transform.Translate(Vector3.forward * speed * Time.deltaTime);
             }
             
         }
        */
    }
}
