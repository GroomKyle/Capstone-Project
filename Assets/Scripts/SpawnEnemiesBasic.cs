//Last update: 9/12

//Using the Unity Engine
using UnityEngine;
using System.Collections;
using System.Collections.Generic; //Used for lists

/// <summary>
/// A Basic Enemy Spawner Used to demonstarate some basic Unity Point Usage
/// </summary>
public class SpawnEnemiesBasic : MonoBehaviour {


    /// <summary>
    /// An Array of locations the enemies can spawn from
    /// </summary>
    public Transform[] spawnPoints;

    /// <summary>
    /// The series of points that the enemies will march towards
    /// </summary>
    public Transform[] goalPoints;

    /// <summary>
    /// The amount of time in seconds between each spawn of the enemy, is given a default to give some buffer time in game
    /// but is actually read from the spawnObject for respawning
    /// </summary>
    public float spawnTime = 5f;

    /// <summary>
    /// Used to countdown and spawn enemies
    /// </summary>
    public float countdownTimer;

    /// <summary>
    /// The location to despawn at
    /// </summary>
    public GameObject despawn;

    /// <summary>
    /// The object to be randomly spawned, made from prefab
    /// </summary>
    public GameObject enemy;

    /// <summary>
    /// The amount in the list
    /// </summary>
    public int poolAmt = 50;

    /// <summary>
    /// Where all the enemies are stored until used
    /// </summary>
    List<GameObject> pool;

    // Use this for initialization
    void Start () {
        countdownTimer = spawnTime;
        pool = new List<GameObject>();

        for( int i = 0; i < poolAmt; i++)
        {
            GameObject Obj = (GameObject)Instantiate(enemy);
            Obj.SetActive(false);
            pool.Add(Obj);
        }
	}

    // Update is called once per frame
    void Update () {
        if (countdownTimer <= 0)
        {
            /*This is the instancing version
            int index = Random.Range(0, spawnPoints.Length);
            GameObject instance = (GameObject) Instantiate(enemy, spawnPoints[index].position, spawnPoints[index].rotation);
            instance.GetComponent<EnemyController>().dest = new Vector3(goalPoints[index].position.x, goalPoints[index].position.y, goalPoints[index].position.z);
            instance.GetComponent<EnemyController>().despawnPoint = despawn;
            //This doesnt work, not sure why though instance.GetComponent<NavMeshAgent>().destination.Set(goalPoints[index].position.x, goalPoints[index].position.y, goalPoints[index].position.z);
            */
            int index = Random.Range(0, spawnPoints.Length);

            for (int i = 0; i < pool.Count; i++)
            {
                if(!pool[i].activeInHierarchy)
                {
                    pool[i].transform.position = new Vector3(spawnPoints[i].position.x, spawnPoints[i].position.y, spawnPoints[i].position.z);
                    pool[i].transform.rotation = new Quaternion(spawnPoints[i].rotation.x, spawnPoints[i].rotation.y, spawnPoints[i].rotation.z, spawnPoints[i].rotation.z);
                    pool[i].GetComponent<EnemyMovement>().dest = new Vector3(goalPoints[index].position.x, goalPoints[index].position.y, goalPoints[index].position.z);
                    pool[i].GetComponent<EnemyMovement>().despawnPoint = despawn;
                    pool[i].SetActive(true);
                    break;
                }
            }

            countdownTimer = spawnTime;
        }
        else
            countdownTimer = countdownTimer - Time.deltaTime;
	}
}
