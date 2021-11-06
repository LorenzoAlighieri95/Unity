using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject muzzle;
    public GameObject chargerSound;
    public GameObject flameThrower;
    public GameObject smoke;
    //private float offset = 0.015f;
    private GameObject projectile;
    private Vector3 gunPosition;
    private AudioSource audioData;
    private AudioSource audioCharger;
    public AudioClip noHammo;
    public int munitions = 250;

    //private int projectileCount = 0;
    void Start()
    {
        gunPosition = transform.localPosition;
        audioData = GetComponent<AudioSource>();
        audioCharger = chargerSound.GetComponent<AudioSource>();
        if (transform.IsChildOf(GameObject.Find("GunContainer").transform))
        {
            ControllerPlayer.hammo = munitions;
        }
    }

    void Update()
    {
        if (!PauseMenu.GameIsPaused && !DetectCollision.dead)
        {
            if (GameObject.Find("GunContainer") != null)
            {
                if (transform.IsChildOf(GameObject.Find("GunContainer").transform))
                {
                    ControllerPlayer.hammo = munitions;
                    if (GetInputDown() && Hammo())
                    {
                        audioData.Play(0);
                    }
                    if (GetInputDown() && !Hammo())
                    {
                        audioCharger.PlayOneShot(noHammo, 1f);
                    }
                    if (GetInput() && Hammo())
                    {
                        munitions--;
                        GetComponent<CameraShake>().enabled = true;
                        GetComponent<CameraShake>().shakeDuration = Time.deltaTime;

                        if (tag == "FlameThrowerGun")
                        {
                            flameThrower.SetActive(true);
                            smoke.SetActive(false);
                        }
                        else if (tag == "Gun")
                        {
                            ShootProjectile();
                            muzzle.SetActive(true);
                        }
                        //transform.position = new Vector3(transform.position.x, transform.position.y, Random.Range(transform.position.z - offset, transform.position.z + offset));

                    }
                    if (GetInputUp() || !Hammo())
                    {
                        if (tag == "FlameThrowerGun")
                        {
                            GetComponent<CameraShake>().enabled = false;
                            flameThrower.SetActive(false);
                            smoke.SetActive(true);
                        }
                        if (tag == "Gun")
                        {
                            GetComponent<CameraShake>().enabled = false;
                            muzzle.SetActive(false);
                        }
                        //transform.localPosition = gunPosition;
                        audioData.Pause();
                    }
                }
            }
        }  else
        {
            audioData.Pause();
        }
    }
    
    bool GetInputDown()
    {
        if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire2"))
            return true;
        else
            return false;
    }

    bool GetInput()
    {
        if (Input.GetKey("space") || Input.GetMouseButton(0) || Input.GetButton("Fire2"))
            return true;
        else
            return false;
    }

    bool GetInputUp()
    {
        if (Input.GetKeyUp("space") || Input.GetMouseButtonUp(0) || Input.GetButtonUp("Fire2"))
            return true;
        else
            return false;
    }

    bool Hammo()
    {
        if (munitions > 0)
            return true;
        else
            return false;
    }

    void ShootProjectile()
    {
        projectile = ObjectPool.SharedInstance.GetPooledObject("Projectile");
        if (projectile != null)
        {
            projectile.transform.position = new Vector3(transform.position.x, transform.position.y-0.5f,transform.position.z+0.5f);
            projectile.transform.rotation = transform.rotation;
            projectile.SetActive(true);
        }

        StartCoroutine(DestroyProjectile(projectile));
    }

    IEnumerator DestroyProjectile(GameObject go)
    {
 
        yield return new WaitForSeconds(1f);
        go.SetActive(false);
    }
}
