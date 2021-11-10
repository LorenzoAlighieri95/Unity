using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public Vector3 offset = new Vector3(0, 0, 0);
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player!= null){
            transform.position = player.transform.position + offset;
            //transform.position = new Vector3(transform.position.x,transform.position.y,player.transform.position.z + offset.z);
        }
    }
}
