using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5;
    //public GameObject zombie;
    //private Vector3 vector;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //vector = new Vector3 (transform.position.x, 0, 1);
        //Debug.Log(vector);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //transform.Translate(vector * speed * Time.deltaTime);
    }
}
