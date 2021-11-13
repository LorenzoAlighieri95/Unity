using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGunInAir : MonoBehaviour
{
    void Update()
    {
        if (!PauseMenu.GameIsPaused)
        {
            transform.Rotate(0.0f, 1.5f, 0.0f, Space.World);
            transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
        }
    }
}
