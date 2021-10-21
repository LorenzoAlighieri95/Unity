using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject zombie;
    public GameObject munitions;
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
    public float munitionsInterval = 20f;

    void Start()
    {
        //zombieInterval = zombieInterval * ControllerPlayer.meters;
        //Debug.Log(zombieInterval);
        InvokeRepeating("SpawnRandomZombie", startDelay, zombieInterval);
        InvokeRepeating("SpawnForest", startDelay, forestInterval);
        InvokeRepeating("SpawnGrass",startDelay,grassInterval);
        InvokeRepeating("SpawnRandomObj",startDelay,objsInterval);
        InvokeRepeating("SpawnBuildings",startDelay,buildsInterval);
        InvokeRepeating("SpawnMunitions", startDelay, munitionsInterval);
        //InvokeRepeating("SpawnObject(zombieZ,zombie), startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {

       // SpawnForest();
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
        zombieInterval = Random.Range(0, 3);
        //int index = Random.Range(0, animalPrefabs.Length);
        spawnZ = Camera.main.transform.position.z + 300f;
        //spawnRangeX += Camera.main.transform.position.x;
        spawnX = Random.Range(15, -15);
        Vector3 spawnPos = new Vector3(spawnX, 0, spawnZ);
        Instantiate(zombie, spawnPos, zombie.transform.rotation);
        zombie.GetComponent<AudioSource>().clip = zombieSounds[Random.Range(0,zombieSounds.Length)];
        if (zombie != null)
        {
            zombie.GetComponent<AudioSource>().Play(0);    
        }
    }

    void SpawnForest() 
    {     
        //spawnIntervalForest = 1;//Random.Range(0,2);
        spawnZ = Camera.main.transform.position.z+ 402f;
        indexForest = Random.Range(0, forest.Length);
        //spawnRangeX += Camera.main.transform.position.x;
        spawnX = Random.Range(spawnRangeX, -spawnRangeX);
        Vector3 spawnPos = new Vector3(spawnX, -1, spawnZ);
        GameObject tree = forest[indexForest];
        //Debug.Log("Tree: x: "+spawnX+ " z: "+spawnZ+ " " + tree.name + " interval: "+spawnIntervalForest);
        float y = Random.Range(0f, 360f);
        Instantiate(tree, spawnPos, Quaternion.Euler(tree.transform.rotation.x, y, tree.transform.rotation.z));      
    }

    void SpawnGrass()
    {
        spawnZ = Camera.main.transform.position.z+ 350f;
        indexGrass = Random.Range(0, grass.Length);
        //spawnRangeX += Camera.main.transform.position.x;
        spawnX = Random.Range(spawnRangeX, -spawnRangeX);
        Vector3 spawnPos = new Vector3(spawnX, 0, spawnZ);
        GameObject blade = grass[indexGrass];
        Instantiate(blade, spawnPos, blade.transform.rotation);
    }

    void SpawnRandomObj()
    {
        spawnZ = Camera.main.transform.position.z+ 402f;
        indexObjs = Random.Range(0, randomObjs.Length);
        //spawnRangeX += Camera.main.transform.position.x;
        spawnX = Random.Range(spawnRangeX, -spawnRangeX);
        Vector3 spawnPos = new Vector3(spawnX, -0.2f, spawnZ);
        GameObject obj = randomObjs[indexObjs];
        //Debug.Log("Blade: x: "+spawnX+ " z: "+spawnZ+ " " + blade.name + " interval: ");
        float y = Random.Range(0f, 360f);
        Instantiate(obj, spawnPos,Quaternion.Euler(obj.transform.rotation.x, y, obj.transform.rotation.z));    
    }

    void SpawnBuildings()
    {
        spawnZ = Camera.main.transform.position.z+ 402f;
        indexBuilds = Random.Range(0, buildings.Length);
        //spawnRangeX += Camera.main.transform.position.x;
        float [] array = new float[] {Random.Range(-55, -40), Random.Range(40, 55)};
        spawnX = array[Random.Range(0,array.Length)];
        Vector3 spawnPos = new Vector3(spawnX, -0.2f, spawnZ);
        GameObject obj = buildings[indexBuilds];
        //Debug.Log("Blade: x: "+spawnX+ " z: "+spawnZ+ " " + blade.name + " interval: ");
        float y = Random.Range(0f, 360f);
        Instantiate(obj, spawnPos,Quaternion.Euler(obj.transform.rotation.x, y, obj.transform.rotation.z));   
    }

    void SpawnMunitions()
    {
        spawnZ = Camera.main.transform.position.z + Random.Range(300f, 400f);
        spawnX = Random.Range(10,-10);
        Vector3 spawnPos = new Vector3(spawnX, 0, spawnZ);
        Instantiate(munitions, spawnPos, munitions.transform.rotation);
    }

}
