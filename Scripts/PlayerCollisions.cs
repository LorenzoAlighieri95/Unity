using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisions : MonoBehaviour
{
    public AudioClip impact;
    public AudioClip scream;
    public AudioClip reloadGun;
    public AudioClip fallingKids;

    public GameObject voice;
    public GameObject blood;
    public GameObject fire;
    
    public static bool powerUp = false;
    private Color objectColor;
    private float prevSpeed;
    //private GameObject flameThrowerGun;

    public static bool takeGun;
    private GameObject flameThrowerGun;
    private GameObject gun;

    public bool boolean=false;

    private Transform gunContainer;
    private GameObject equippedGun;
    private int munitions;
    private GameManager gameManager;
    private GunScript gunScript;

    private int prevHammo;

    void Start()
    {
        objectColor = blood.GetComponent<Renderer>().material.color;
        blood.GetComponent<Renderer>().material.color = new Color(objectColor.r, objectColor.g, objectColor.b, 0);
        gunContainer = Camera.main.transform.Find("GunContainer");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gunScript = gunContainer.GetChild(0).gameObject.GetComponent<GunScript>();
    }

    void Update()
    {

    }

    private int  GetHammo()
    {
        return gunContainer.GetChild(0).gameObject.GetComponent<GunScript>().munitions;
    }

    private void SetHammo(int i)
    {
        gunContainer.GetChild(0).gameObject.GetComponent<GunScript>().munitions = i;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Camera.main.GetComponent<CameraShake>().enabled = true;
        
        if ((collision.gameObject.tag == "Tree" || collision.gameObject.tag == "RandomObj") && !powerUp)
        {
            Camera.main.GetComponent<CameraShake>().shakeDuration = 0.5f;
            if (!voice.GetComponent<AudioSource>().isPlaying)
            {
                voice.GetComponent<AudioSource>().PlayOneShot(impact, 0.75f);
            }
            if (GetHammo() >= 50) {
                SetHammo(GetHammo() - 50);
                gameManager.moreHammoUI("-50");
            }
            //transform.Translate(Random.Range(Random.Range(-1.5f, -1f), Random.Range(1f, 1.5f)), 0, 0);
            StartCoroutine(FadeInObject(blood, 1.5f));
            StartCoroutine(FadeOutObject(blood, 0.5f));
        } 
        if (collision.gameObject.tag == "Zombie" && !powerUp)
        {
            Camera.main.GetComponent<CameraShake>().shakeDuration = 0.5f;
            if (!voice.GetComponent<AudioSource>().isPlaying)
            {
                voice.GetComponent<AudioSource>().PlayOneShot(scream, 0.5f);
            }
            StartCoroutine(FadeInObject(blood, 1.5f));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //munitions = gunContainer.GetChild(0).gameObject.GetComponent<GunScript>().munitions;
        if (other.CompareTag("Munitions"))
        {
            prevHammo = GetHammo();
            SetHammo(500);
            gameManager.moreHammoUI("+" + (500-prevHammo));
            voice.GetComponent<AudioSource>().PlayOneShot(reloadGun, 1f);
        }
        if (other.CompareTag("DroppedMunitions")) 
        {
            if (GetHammo() <= 450)
            {
                SetHammo(GetHammo()+50);
                gameManager.moreHammoUI("+50");
                voice.GetComponent<AudioSource>().PlayOneShot(reloadGun, 1f);
                //gameManager.moreHammoUI(true);
            }
            else if (GetHammo() > 450)
            {
                prevHammo = GetHammo();
                SetHammo(500);
                gameManager.moreHammoUI("+" + (500 - prevHammo));
                voice.GetComponent<AudioSource>().PlayOneShot(reloadGun, 1f);
            }
        }
        if (other.CompareTag("PowerUp") && !powerUp)
        {
            StartCoroutine(PowerUp());
        }
        if ((other.gameObject.tag == "Gun" || other.gameObject.tag == "FlameThrowerGun") && !this.boolean)
        {
            gun = other.gameObject;
            equippedGun = gunContainer.GetChild(0).gameObject;
            if (gun.tag == equippedGun.tag && equippedGun.GetComponent<GunScript>().munitions > 250) 
                gun.GetComponent<GunScript>().munitions = equippedGun.GetComponent<GunScript>().munitions;
            equippedGun.GetComponent<PickUpController>().Drop(equippedGun);
            gun.GetComponent<PickUpController>().PickUp(gun);
            StartCoroutine(this.ChangeBool());   
        }
    }

    IEnumerator ChangeBool()
    {
        boolean = true;
        yield return new WaitForSeconds(2);
        boolean = false;
    }

    IEnumerator PowerUp()
    {
        gunContainer.gameObject.SetActive(false);
        voice.GetComponent<AudioSource>().PlayOneShot(fallingKids, 0.5f);
        powerUp = true;
        fire.SetActive(true);
        //GetComponent<BoxCollider>().enabled = false;
        prevSpeed = GetComponent<ControllerPlayer>().speed;
        GetComponent<ControllerPlayer>().speed = prevSpeed*5;
        yield return new WaitForSeconds(18);
        gunContainer.gameObject.SetActive(true);
        GetComponent<ControllerPlayer>().speed = prevSpeed;
        //GetComponent<BoxCollider>().enabled = true;
        fire.SetActive(false);
        powerUp = false;
    }

    IEnumerator FadeOutObject(GameObject obj, float fadeSpeed)
    {
        while (obj.GetComponent<Renderer>().material.color.a > 0)
        {
            objectColor = obj.GetComponent<Renderer>().material.color;
            float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

            obj.GetComponent<Renderer>().material.color = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            yield return null;
        }
        obj.SetActive(false);
    }

    IEnumerator FadeInObject(GameObject obj, float fadeSpeed)
    {
        obj.SetActive(true);
        while (obj.GetComponent<Renderer>().material.color.a < 1)
        {
            objectColor = obj.GetComponent<Renderer>().material.color;
            float fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

            obj.GetComponent<Renderer>().material.color = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            yield return null;
        }
    }

    /*
    IEnumerator waiter()
    {
        zombieAnim.SetTrigger("AttackTrigger");
        GetComponent<MoveForward>().speed = -1;
        GameObject.Find("Player").GetComponent<ControllerPlayer>().speed = -1;
        yield return new WaitForSeconds(2.5f);
        Time.timeScale = 0;
        AudioListener.pause = true;
        scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        Time.timeScale = 1.0f;
        AudioListener.pause = false;
    }
    */
    /*
    private void OnTriggerEnter(Collider other)
    {
            transform.Translate(Random.Range(Random.Range(-1.5f, -1f), Random.Range(1f, 1.5f)), 0, 0);
            if (!voice.GetComponent<AudioSource>().isPlaying)
                voice.GetComponent<AudioSource>().PlayOneShot(impact, 0.75f);
        if (other.CompareTag("Zombie"))
        {
            voice.GetComponent<AudioSource>().PlayOneShot(scream, 0.75f);
        } else if (other.CompareTag("Munitions"))
        {
            ControllerPlayer.charger.Clear();
            //Debug.Log("Ricarica. Munizioni: " + ControllerPlayer.charger.Count);
        }
    }
    */
}
/*
        * QUESTO è QUELLO BUONO
       if ((collision.gameObject.tag == "Gun" || collision.gameObject.tag == "FlameThrowerGun") && !this.boolean)
       {
           gun = collision.gameObject;
           equippedGun = gunContainer.GetChild(0).gameObject;
           equippedGun.GetComponent<PickUpController>().Drop(equippedGun);
           gun.GetComponent<PickUpController>().PickUp(gun);
           StartCoroutine(this.ChangeBool());
       }
       */
/*
if (collision.gameObject.tag == "Gun" && !this.boolean)
{
    gun = collision.gameObject;
    equippedGun = gunContainer.GetChild(0).gameObject;
    if (equippedGun.tag == "Gun")
    {
        equippedGun.GetComponent<PickUpController>().Drop(equippedGun);
        gun.GetComponent<PickUpController>().PickUp(gun);
    }
    else
    {
        flameThrowerGun = transform.Find("GunContainer").Find("FlameThrowerGun").gameObject;
        flameThrowerGun.GetComponent<PickUpController>().Drop(flameThrowerGun);
        gun.GetComponent<PickUpController>().PickUp(gun);
        StartCoroutine(this.ChangeBool());
    }

}
if (collision.gameObject.tag == "FlameThrowerGun" && !this.boolean)
{
    flameThrowerGun = collision.gameObject;
    gun = transform.Find("GunContainer").Find("Gun").gameObject;

    gun.GetComponent<PickUpController>().Drop(gun);
    flameThrowerGun.GetComponent<PickUpController>().PickUp(flameThrowerGun);
    StartCoroutine(this.ChangeBool());
}     
*/
/*
if (collision.gameObject.tag == "Munitions")
{
    //ControllerPlayer.charger.Clear();
    ControllerPlayer.napalm = 500;
    voice.GetComponent<AudioSource>().PlayOneShot(reloadGun, 1f);           
}
if (collision.gameObject.tag == "DroppedMunitions")
{
    if (ControllerPlayer.napalm <= 450)
    {
        ControllerPlayer.napalm += 50;
        voice.GetComponent<AudioSource>().PlayOneShot(reloadGun, 1f);
    } else if (ControllerPlayer.napalm > 450)
    {
        ControllerPlayer.napalm = 500;
        voice.GetComponent<AudioSource>().PlayOneShot(reloadGun, 1f);
    }
}
if (collision.gameObject.tag == "PowerUp" && !powerUp)
{
    StartCoroutine(PowerUp());
}
*/