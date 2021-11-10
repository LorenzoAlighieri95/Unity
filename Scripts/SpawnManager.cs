using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float spawnDistance = 250f;
    private GameObject zombie;
    [SerializeField] private GameObject munitions;
    [SerializeField] private GameObject powerUp;
    //[SerializeField] private GameObject cross;
    private GameObject[] forest;
    private GameObject[] grass;
    private GameObject[] randomObjs;
    private GameObject[] buildings;

    public AudioClip[] zombieSounds;

    private float spawnZ;
    private float spawnX;
    private float spawnRangeX = 60f;

    public float zombieSlip = 0;
    public float zombieInterval = 0;
    public float forestInterval = 0.70f;
    public float grassInterval = 0.000005f;
    public float objsInterval = 5f;
    public float buildsInterval = 10f;
    public float munitionsInterval = 100;
    //public float crossesInterval = 1f;
    public float powerUpInterval = 100;
    public float gunInterval = 25f;

    public bool spawnZombie = true;
    public bool spawnForest = true;
    public bool spawnGrass = true;
    public bool spawnRandomObj = true;
    public bool spawnBuildings = true;
    public bool spawnMunitions = true;
    //public bool spawnCrosses = true;
    public bool spawnPowerUps = true;
    public bool spawnGun = true;

    private string objTag;

    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        StartCoroutine(SpawnRandomZombie(zombieInterval));
        StartCoroutine(SpawnForest(forestInterval));   
        StartCoroutine(SpawnGrass(grassInterval));
        StartCoroutine(SpawnRandomObj(objsInterval));
        StartCoroutine(SpawnBuildings(buildsInterval));
        StartCoroutine(SpawnMunitions(munitionsInterval));
        StartCoroutine(SpawnPowerUps(powerUpInterval));
        StartCoroutine(SpawnRandomGun(gunInterval));
    }


    IEnumerator SpawnRandomZombie(float waitTime)
    {
        while (spawnZombie)
        {
            yield return new WaitForSeconds(waitTime);
            //zombieInterval = 100f/ ((float)ControllerPlayer.meters+1);
            //zombieSlip = 1000;
            //zombieInterval = -zombieSlip * Mathf.Pow(10f,-4f) * (float)ControllerPlayer.meters + 4.8f;
            //zombieInterval = 1f;
            //zombieInterval = -zombieSlip * Mathf.Pow(10f, -4f) * player.GetComponent<ControllerPlayer>().speed + 4.8f;
            //Debug.Log( "speed: " + player.GetComponent<ControllerPlayer>().speed + " interval: "+ zombieInterval);
            //zombieInterval = 0.25f;

            zombieInterval = 100f/player.GetComponent<ControllerPlayer>().speed;
            //Debug.Log("speed: " + player.GetComponent<ControllerPlayer>().speed + " interval: " + zombieInterval);
            //spawnZ = Camera.main.transform.position.z + spawnDistance;
            //spawnX = Random.Range(Camera.main.transform.position.x - 30, Camera.main.transform.position.x + 30);
            //Vector3 spawnPos = new Vector3(spawnX, 0, spawnZ);
            //Instantiate(zombie, spawnPos, zombie.transform.rotation);
            //zombie = ObjectPool.SharedInstance.GetPooledObject("Zombie");
            /*
            if (zombie != null)
            {
                zombie.transform.position = spawnPos;
                zombie.SetActive(true);
                zombie.GetComponent<Collider>().enabled = true;
                zombie.GetComponent<AudioSource>().clip = zombieSounds[Random.Range(0, zombieSounds.Length)];
                zombie.GetComponent<AudioSource>().Play(0);
            }
            */
            //waitTime = zombieInterval;
            for (int i = 0; i < 50; i++)
            {
                spawnZ = Random.Range(Camera.main.transform.position.z + spawnDistance, Camera.main.transform.position.z + spawnDistance *2);
                spawnX = Random.Range(Camera.main.transform.position.x - 30, Camera.main.transform.position.x + 30);
                Vector3 spawnPos = new Vector3(spawnX, 0, spawnZ);
                //Instantiate(zombie, spawnPos, zombie.transform.rotation);
                zombie = ObjectPool.SharedInstance.GetPooledObject("Zombie");
                if (zombie != null)
                {
                    zombie.transform.position = spawnPos;
                    zombie.SetActive(true);
                    zombie.GetComponent<Collider>().enabled = true;
                    zombie.GetComponent<AudioSource>().clip = zombieSounds[Random.Range(0, zombieSounds.Length)];
                    zombie.GetComponent<AudioSource>().Play(0);
                }
            }
            waitTime = zombieInterval;
        }
    }

    IEnumerator SpawnRandomGun(float waitTime)
    {
        while (spawnGun)
        {
            yield return new WaitForSeconds(waitTime);
            spawnZ = Camera.main.transform.position.z + spawnDistance;
            spawnX = Random.Range(Camera.main.transform.position.x - 10, Camera.main.transform.position.x + 10);
            Vector3 spawnPos = new Vector3(spawnX, 2, spawnZ);
            if (Random.Range(0, 10) < 5) objTag = "Gun";
            else objTag = "FlameThrowerGun";
            GameObject gun = ObjectPool.SharedInstance.GetPooledObject(objTag);
            //float y = Random.Range(0f, 360f);
            if (gun != null)
            {
                gun.transform.position = spawnPos;
                //gun.transform.rotation = Quaternion.Euler(gun.transform.rotation.x, y, gun.transform.rotation.z);
                gun.SetActive(true);
            }
            //Instantiate(tree, spawnPos, Quaternion.Euler(tree.transform.rotation.x, y, tree.transform.rotation.z));
        }
    }

    IEnumerator SpawnForest(float waitTime)
    {
        while (spawnForest)
        {
            yield return new WaitForSeconds(waitTime);
            forestInterval = 160f / player.GetComponent<ControllerPlayer>().speed;
            //indexForest = Random.Range(0, forest.Length);
            //spawnZ = Camera.main.transform.position.z + spawnDistance;
            //spawnX = Random.Range(Camera.main.transform.position.x - spawnRangeX, Camera.main.transform.position.x + spawnRangeX);
            //Vector3 spawnPos = new Vector3(spawnX, -1, spawnZ);
            //GameObject tree = forest[indexForest];
            //GameObject tree = ObjectPool.SharedInstance.GetPooledObject("Tree");
            //float y = Random.Range(0f, 360f);
            /*
            if (tree != null)
            {
                tree.transform.position = spawnPos;
                tree.transform.rotation = Quaternion.Euler(tree.transform.rotation.x, y, tree.transform.rotation.z);
                tree.SetActive(true);
            }
            */
            //waitTime = forestInterval;
            for (int i = 0; i < 50; i++)
            {
                GameObject tree = ObjectPool.SharedInstance.GetPooledObject("Tree");
                if (tree != null)
                {
                    float y = Random.Range(0f, 360f);
                    spawnX = Random.Range(Camera.main.transform.position.x - spawnRangeX, Camera.main.transform.position.x + spawnRangeX);
                    spawnZ = Random.Range(Camera.main.transform.position.z + spawnDistance+50, Camera.main.transform.position.z + spawnDistance * 2);                  
                    tree.transform.rotation = Quaternion.Euler(tree.transform.rotation.x, y, tree.transform.rotation.z);
                    Vector3 spawnPos = new Vector3(spawnX, 0, spawnZ);
                    tree.transform.position = spawnPos;
                    tree.SetActive(true);
                }
            }
            //Instantiate(tree, spawnPos, Quaternion.Euler(tree.transform.rotation.x, y, tree.transform.rotation.z));
            waitTime = forestInterval;
        }
    }
    /*
    IEnumerator SpawnGrass(float waitTime)
    {
        while (spawnGrass)
        {
            yield return new WaitForSeconds(waitTime);
            //indexGrass = Random.Range(0, grass.Length);
            grassInterval = 1 / player.GetComponent<ControllerPlayer>().speed;
            spawnZ = Camera.main.transform.position.z + spawnDistance;
            spawnX = Random.Range(Camera.main.transform.position.x - spawnRangeX, Camera.main.transform.position.x + spawnRangeX);
            Vector3 spawnPos = new Vector3(spawnX, 0.2f, spawnZ);
            //GameObject blade = grass[indexGrass];
            //Instantiate(blade, spawnPos, blade.transform.rotation);
            GameObject blade = ObjectPool.SharedInstance.GetPooledObject("Grass");
            if (blade != null)
            {
                blade.transform.position = spawnPos;
                blade.SetActive(true);
            }
            waitTime = grassInterval;
        }
    }
    */

    IEnumerator SpawnGrass(float waitTime)
    {
        while (spawnGrass)
        {
            yield return new WaitForSeconds(waitTime);
            //indexGrass = Random.Range(0, grass.Length);
            grassInterval = 80 / player.GetComponent<ControllerPlayer>().speed;
            //spawnZ = Camera.main.transform.position.z + spawnDistance;
            
            //Vector3 spawnPos = new Vector3(spawnX, 0.2f, spawnZ);
            //GameObject blade = grass[indexGrass];
            //Instantiate(blade, spawnPos, blade.transform.rotation);
            
            for (int i = 0;  i < 50; i++)
            {
                GameObject blade = ObjectPool.SharedInstance.GetPooledObject("Grass");
                if (blade != null)
                {
                    spawnX = Random.Range(Camera.main.transform.position.x - spawnRangeX, Camera.main.transform.position.x + spawnRangeX);
                    spawnZ = Random.Range(Camera.main.transform.position.z + spawnDistance, Camera.main.transform.position.z + spawnDistance *2);
                    Vector3 spawnPos = new Vector3(spawnX, 0f, spawnZ);
                    blade.transform.position = spawnPos;
                    blade.SetActive(true);
                }
            }

            waitTime = grassInterval;
        }
    }

    IEnumerator SpawnRandomObj(float waitTime)
    {
        while (spawnRandomObj)
        {
            yield return new WaitForSeconds(waitTime);
            //indexObjs = Random.Range(0, randomObjs.Length);
            spawnZ = Camera.main.transform.position.z + spawnDistance;
            spawnX = Random.Range(Camera.main.transform.position.x - spawnRangeX, Camera.main.transform.position.x + spawnRangeX);
            Vector3 spawnPos = new Vector3(spawnX, -0.15f, spawnZ);
            //GameObject obj = randomObjs[indexObjs];
            float y = Random.Range(0f, 360f);
            //Instantiate(obj, spawnPos, Quaternion.Euler(obj.transform.rotation.x, y, obj.transform.rotation.z));
            GameObject obj = ObjectPool.SharedInstance.GetPooledObject("RandomObj");
            if (obj != null)
            {
                obj.transform.position = spawnPos;
                obj.transform.rotation = Quaternion.Euler(obj.transform.rotation.x, y, obj.transform.rotation.z);
                obj.SetActive(true);
            }
        }
    }

    IEnumerator SpawnBuildings(float waitTime)
    {
        while (spawnBuildings)
        {
            yield return new WaitForSeconds(waitTime);
            //indexBuilds = Random.Range(0, buildings.Length);
            spawnZ = Camera.main.transform.position.z + spawnDistance;
            float[] array = new float[] { Random.Range(Camera.main.transform.position.x - 60, -80), Random.Range(Camera.main.transform.position.x + 60, 80) };
            spawnX = array[Random.Range(0, array.Length)];
            Vector3 spawnPos = new Vector3(spawnX, -0.5f, spawnZ);
            //GameObject obj = buildings[indexBuilds];
            float y = Random.Range(0f, 360f);
            //Instantiate(obj, spawnPos, Quaternion.Euler(obj.transform.rotation.x, y, obj.transform.rotation.z));
            GameObject obj = ObjectPool.SharedInstance.GetPooledObject("Buildings");
            if (obj != null)
            {
                obj.transform.position = spawnPos;
                obj.transform.rotation = Quaternion.Euler(obj.transform.rotation.x, y, obj.transform.rotation.z);
                obj.SetActive(true);
            }
        }
    }

    IEnumerator SpawnMunitions(float waitTime)
    {
        while (spawnMunitions)
        {
            yield return new WaitForSeconds(waitTime);
            spawnZ = Camera.main.transform.position.z + spawnDistance;
            spawnX = Random.Range(Camera.main.transform.position.x - 10, Camera.main.transform.position.x + 10);
            Vector3 spawnPos = new Vector3(spawnX, 0.2f, spawnZ);
            Instantiate(munitions, spawnPos, munitions.transform.rotation);
        }
    }
    /*
    IEnumerator SpawnCrosses(float waitTime)
    {
        while (spawnCrosses)
        {
            yield return new WaitForSeconds(waitTime);
            spawnZ = Camera.main.transform.position.z + spawnDistance;
            spawnX = Random.Range(Camera.main.transform.position.x - spawnRangeX, Camera.main.transform.position.x + spawnRangeX);
            Vector3 spawnPos = new Vector3(spawnX, 1,spawnZ);
            float y = Random.Range(0f, 360f);
            Instantiate(cross, spawnPos, Quaternion.Euler(cross.transform.rotation.x, y, cross.transform.rotation.z));
        }
    }
    */
    IEnumerator SpawnPowerUps(float waitTime)
    {
        while (spawnZombie)
        {
            yield return new WaitForSeconds(waitTime);
            spawnZ = Camera.main.transform.position.z + spawnDistance;
            spawnX = Random.Range(Camera.main.transform.position.x - 10, Camera.main.transform.position.x + 10);
            Vector3 spawnPos = new Vector3(spawnX, 0.2f, spawnZ);
            Instantiate(powerUp, spawnPos, powerUp.transform.rotation);
        }
    }
}
