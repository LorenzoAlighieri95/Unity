using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutBound : MonoBehaviour
{

    void Start()
    {

    }

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
