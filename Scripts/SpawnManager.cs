using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject zombie;
    public GameObject munitions;
    public GameObject powerUp;
    public GameObject cross;
    public GameObject [] forest;
    public GameObject [] grass;
    public GameObject [] randomObjs;
    public GameObject [] buildings;

    public AudioClip [] zombieSounds;

    private float spawnZ;
    private float spawnX;
    private float spawnRangeX = 60f;
    //private float zombieZ = 100f;
    //private float evinronmentZ = 200f;
    private float startDelay = 0;
    private int indexForest;
    private int indexGrass;
    private int indexObjs;
    private int indexBuilds;

    public float zombieInterval = 1.5f;
    public float forestInterval = 0.70f;
    public float grassInterval = 0.000005f;
    public float objsInterval = 5f;
    public float buildsInterval = 10f;
    public float munitionsInterval = 100;
    public float crossesInterval = 1f;
    public float powerUpInterval = 100; 

    void Start()
    {
        InvokeRepeating("SpawnRandomZombie", startDelay, zombieInterval);
        InvokeRepeating("SpawnForest", startDelay, forestInterval);
        InvokeRepeating("SpawnGrass",startDelay,grassInterval);
        InvokeRepeating("SpawnRandomObj",startDelay,objsInterval);
        InvokeRepeating("SpawnBuildings",startDelay,buildsInterval);
        InvokeRepeating("SpawnMunitions", startDelay, munitionsInterval);
        InvokeRepeating("SpawnCrosses", startDelay, crossesInterval);
        InvokeRepeating("SpawnPowerUps", 10, powerUpInterval);
    }

    void Update()
    {

    }

    /*
    void SpawnObject(zDistance, object) {
        //int index = Random.Range(0, animalPrefabs.Length);
        spawnZ = Camera.main.transform.position.z + zDistance;
        spawnX = Random.Range(spawnRangeX, -spawnRangeX);
        Vector3 spawnPos = new Vector3(spawnX, 0, spawnZ);
        Instantiate(object, spawnPos, object.transform.rotation);
        //Debug.Log("Zombie: x: "+spawnX+ " z: "+spawnZ+ " interval: "+spawnIntervalForest);
    }*/

    void SpawnRandomZombie()
    {
        zombieInterval = 0.15f/ControllerPlayer.meters; //NON SO SE FUNZIONA
        //int index = Random.Range(0, animalPrefabs.Length);
        spawnZ = Camera.main.transform.position.z + 301;
        //spawnRangeX += Camera.main.transform.position.x;
        //spawnX = Random.Range(15, -15);
        spawnX = Random.Range(Camera.main.transform.position.x - 30, Camera.main.transform.position.x + 30);
        Vector3 spawnPos = new Vector3(spawnX, 0, spawnZ);
        Instantiate(zombie, spawnPos, zombie.transform.rotation);
        if (zombie != null)
        {
            zombie.GetComponent<AudioSource>().clip = zombieSounds[Random.Range(0, zombieSounds.Length)];
            zombie.GetComponent<AudioSource>().Play(0);    
        }
    }

    void SpawnForest() 
    {     
        //spawnIntervalForest = 1;//Random.Range(0,2);
        spawnZ = Camera.main.transform.position.z+ 301;
        indexForest = Random.Range(0, forest.Length);
        spawnX = Random.Range(Camera.main.transform.position.x - spawnRangeX, Camera.main.transform.position.x + spawnRangeX);
        Vector3 spawnPos = new Vector3(spawnX, -1, spawnZ);
        GameObject tree = forest[indexForest];
        //Debug.Log("Tree: x: "+spawnX+ " z: "+spawnZ+ " " + tree.name + " interval: "+spawnIntervalForest);
        float y = Random.Range(0f, 360f);
        Instantiate(tree, spawnPos, Quaternion.Euler(tree.transform.rotation.x, y, tree.transform.rotation.z));      
    }

    void SpawnGrass()
    {
        spawnZ = Camera.main.transform.position.z+ 301;
        indexGrass = Random.Range(0, grass.Length);
        //spawnRangeX += Camera.main.transform.position.x;
        //spawnX = Random.Range(spawnRangeX, -spawnRangeX);
        spawnX = Random.Range(Camera.main.transform.position.x - spawnRangeX, Camera.main.transform.position.x + spawnRangeX);
        Vector3 spawnPos = new Vector3(spawnX, 0, spawnZ);
        GameObject blade = grass[indexGrass];
        Instantiate(blade, spawnPos, blade.transform.rotation);
    }

    void SpawnRandomObj()
    {
        spawnZ = Camera.main.transform.position.z+ 301;
        indexObjs = Random.Range(0, randomObjs.Length);
        //spawnRangeX += Camera.main.transform.position.x;
        //spawnX = Random.Range(spawnRangeX, -spawnRangeX);
        spawnX = Random.Range(Camera.main.transform.position.x - spawnRangeX, Camera.main.transform.position.x + spawnRangeX);
        Vector3 spawnPos = new Vector3(spawnX, -0.2f, spawnZ);
        GameObject obj = randomObjs[indexObjs];
        //Debug.Log("Blade: x: "+spawnX+ " z: "+spawnZ+ " " + blade.name + " interval: ");
        float y = Random.Range(0f, 360f);
        Instantiate(obj, spawnPos,Quaternion.Euler(obj.transform.rotation.x, y, obj.transform.rotation.z));    
    }

    void SpawnBuildings()
    {
        spawnZ = Camera.main.transform.position.z+ 301;
        indexBuilds = Random.Range(0, buildings.Length);
        //spawnRangeX += Camera.main.transform.position.x;
        float [] array = new float[] { Random.Range(Camera.main.transform.position.x -60, -80), Random.Range(Camera.main.transform.position.x + 60, 80) };
        spawnX = array[Random.Range(0,array.Length)];
        //spawnX = Random.Range(Random.Range(Camera.main.transform.position.x-80, -100), Random.Range(Camera.main.transform.position.x + 80, 100));
        //Debug.Log("buildx: " + spawnX + " playerx: " + Camera.main.transform.position.x);
        Vector3 spawnPos = new Vector3(spawnX, -0.2f, spawnZ);
        GameObject obj = buildings[indexBuilds];
        //Debug.Log("Blade: x: "+spawnX+ " z: "+spawnZ+ " " + blade.name + " interval: ");
        float y = Random.Range(0f, 360f);
        Instantiate(obj, spawnPos,Quaternion.Euler(obj.transform.rotation.x, y, obj.transform.rotation.z));   
    }

    void SpawnMunitions()
    {
        spawnZ = Camera.main.transform.position.z + 100;
        //spawnX = Random.Range(10,-10);
        spawnX = Random.Range(Camera.main.transform.position.x - 10, Camera.main.transform.position.x + 10);
        Vector3 spawnPos = new Vector3(spawnX, 0.1f, spawnZ);
        Instantiate(munitions, spawnPos, munitions.transform.rotation);
    }

    void SpawnCrosses()
    {
        spawnZ = Camera.main.transform.position.z + 301;
        spawnX = Random.Range(Camera.main.transform.position.x - spawnRangeX, Camera.main.transform.position.x + spawnRangeX);
        Vector3 spawnPos = new Vector3(spawnX, 1.4f, spawnZ);
        float y = Random.Range(0f, 360f);
        Instantiate(cross, spawnPos, Quaternion.Euler(cross.transform.rotation.x, y, cross.transform.rotation.z));
    }

    void SpawnPowerUps()
    {
        spawnZ = Camera.main.transform.position.z + 100;
        spawnX = Random.Range(Camera.main.transform.position.x - 10, Camera.main.transform.position.x + 10);
        Vector3 spawnPos = new Vector3(spawnX, 0.1f, spawnZ);
        Instantiate(powerUp, spawnPos, powerUp.transform.rotation);
    }

}
