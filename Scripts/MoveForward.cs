using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 0;
    private Transform playerTransform;
    [SerializeField] private float rotation_Speed=1f;
    [SerializeField] private float playerRange = 20;

    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
    }

    void Update()
    {
        Vector3 lTargetDir = playerTransform.position - transform.position;
        lTargetDir.y = 0.0f;
        
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lTargetDir), Time.deltaTime * rotation_Speed);
        
        //transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z));
        
        if (transform.position.z < playerTransform.position.z + playerRange*2 && transform.position.x > transform.position.x - playerRange && transform.position.x < transform.position.x + playerRange)
        {
            GetComponent<Animator>().SetTrigger("RunTrigger");
            //speed = 2;
        }
        
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //transform.position += transform.forward * speed * Time.deltaTime;   
    }
}
