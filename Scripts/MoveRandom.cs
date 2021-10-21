using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRandom : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5;
    //public GameObject player;
    private Vector3 vector;
    private float direction;

    void Start()
    {
        direction = Random.Range(-1f, 1f);
        //initialPos = transform.position.x;
        Debug.Log("Direction: " + direction + " Position: " + transform.position.x);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.position.x);
        vector = new Vector3(direction, 0, 1) ;
        //Debug.Log(vector);
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);
        transform.Translate(vector * speed * Time.deltaTime);
    }
}
