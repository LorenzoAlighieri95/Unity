using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutBound : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject player;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {      
        float maxZPos = Mathf.Abs(transform.position.z + transform.localScale.z / 2);

        if (maxZPos < Camera.main.transform.position.z)
        {   
            if (gameObject.tag == "PowerUp" || gameObject.tag == "Munitions" || gameObject.tag == "DroppedMunitions")
            {
                Destroy(gameObject);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
