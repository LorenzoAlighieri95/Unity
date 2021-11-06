using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGunInAir : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.GameIsPaused)
        {
            transform.Rotate(0.0f, 1.5f, 0.0f, Space.World);
            transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
        }
    }
}
