using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float spawnDistance = 250f;
    public GameObject zombie;
    public GameObject munitions;
    public GameObject powerUp;
    public GameObject cross;
    public GameObject[] forest;
    public GameObject[] grass;
    public GameObject[] randomObjs;
    public GameObject[] buildings;

    public AudioClip[] zombieSounds;

    private float spawnZ;
    private float spawnX;
    private float spawnRangeX = 60f;
    private int indexForest;
    private int indexGrass;
    private int indexObjs;
    private int indexBuilds;

    public float zombieInterval = 0;
    public float forestInterval = 0.70f;
    public float grassInterval = 0.000005f;
    public float objsInterval = 5f;
    public float buildsInterval = 10f;
    public float munitionsInterval = 100;
    public float crossesInterval = 1f;
    public float powerUpInterval = 100;

    public bool spawnZombie = true;
    public bool spawnForest = true;
    public bool spawnGrass = true;
    public bool spawnRandomObj = true;
    public bool spawnBuildings = true;
    public bool spawnMunitions = true;
    public bool spawnCrosses = true;
    public bool spawnPowerUps = true;

    void Start()
    {
        StartCoroutine(SpawnRandomZombie(zombieInterval));
        
        StartCoroutine(SpawnForest(forestInterval));
        
        StartCoroutine(SpawnGrass(grassInterval));
        StartCoroutine(SpawnRandomObj(objsInterval));
        StartCoroutine(SpawnBuildings(buildsInterval));
        StartCoroutine(SpawnMunitions(munitionsInterval));
        //StartCoroutine(SpawnCrosses(crossesInterval));
        StartCoroutine(SpawnPowerUps(powerUpInterval));
        
    }

    void Update()
    {

    }

    IEnumerator SpawnRandomZombie(float waitTime)
    {
        while (spawnZombie)
        {
            yield return new WaitForSeconds(waitTime);
            zombieInterval = 50f/ ((float)ControllerPlayer.meters+1);
            //zombieInterval = 5f / (ControllerPlayer.meters + 1);
            //Debug.Log( "Meters: " +ControllerPlayer.meters + 1+ " interval: "+ zombieInterval);
            spawnZ = Camera.main.transform.position.z + spawnDistance;
            spawnX = Random.Range(Camera.main.transform.position.x - 30, Camera.main.transform.position.x + 30);
            Vector3 spawnPos = new Vector3(spawnX, 0, spawnZ);
            //Instantiate(zombie, spawnPos, zombie.transform.rotation);
            zombie = ObjectPool.SharedInstance.GetPooledObject("Zombie");
            if (zombie != null)
            {
                zombie.transform.position = spawnPos;
                zombie.SetActive(true);
                zombie.GetComponent<AudioSource>().clip = zombieSounds[Random.Range(0, zombieSounds.Length)];
                zombie.GetComponent<AudioSource>().Play(0);
            }
            waitTime = zombieInterval;
        }
    }
    
    IEnumerator SpawnForest(float waitTime)
    {
        while (spawnForest)
        {
            yield return new WaitForSeconds(waitTime);
            indexForest = Random.Range(0, forest.Length);
            spawnZ = Camera.main.transform.position.z + spawnDistance;
            spawnX = Random.Range(Camera.main.transform.position.x - spawnRangeX, Camera.main.transform.position.x + spawnRangeX);
            Vector3 spawnPos = new Vector3(spawnX, -1, spawnZ);
            //GameObject tree = forest[indexForest];
            GameObject tree = ObjectPool.SharedInstance.GetPooledObject("Tree");
            float y = Random.Range(0f, 360f);
            if (tree != null)
            {
                tree.transform.position = spawnPos;
                tree.transform.rotation = Quaternion.Euler(tree.transform.rotation.x, y, tree.transform.rotation.z);
                tree.SetActive(true);
            }
            //Instantiate(tree, spawnPos, Quaternion.Euler(tree.transform.rotation.x, y, tree.transform.rotation.z));
        }
    }

    IEnumerator SpawnGrass(float waitTime)
    {
        while (spawnGrass)
        {
            yield return new WaitForSeconds(waitTime);
            indexGrass = Random.Range(0, grass.Length);
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
        }
    }

    IEnumerator SpawnRandomObj(float waitTime)
    {
        while (spawnRandomObj)
        {
            yield return new WaitForSeconds(waitTime);
            indexObjs = Random.Range(0, randomObjs.Length);
            spawnZ = Camera.main.transform.position.z + spawnDistance;
            spawnX = Random.Range(Camera.main.transform.position.x - spawnRangeX, Camera.main.transform.position.x + spawnRangeX);
            Vector3 spawnPos = new Vector3(spawnX, -0.2f, spawnZ);
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
            indexBuilds = Random.Range(0, buildings.Length);
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
