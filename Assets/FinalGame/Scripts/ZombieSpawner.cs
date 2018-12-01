using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour {

    private List<Transform> spawnPoints = new List<Transform>();

    [SerializeField] private Transform player;
    [SerializeField] private List<GameObject> zombiePrefabs;
    [SerializeField] private float spawnInterval = 8;
    [SerializeField] private float waves = 4;
    [SerializeField] private float spawnIntervalDec = 3;
    [SerializeField] private float time = 360;

    private float wave = 1;
    private bool isSpawning = false;

	// Use this for initialization
	void Start () {
        //get the children of this transform (spawn points)
        foreach (Transform sp in transform)
        {
            spawnPoints.Add(sp);
        }

        //starts the spawning
        ToggleSpawnning();

        InvokeRepeating("UpdateSpawnInterval", time / waves, time / waves);
    }

    public void ToggleSpawnning()
    {
        if (!isSpawning)
        {
            //start the spawning
            isSpawning = true;
            InvokeRepeating("Spawn", 0, spawnInterval);
        }
        else
        {
            isSpawning = false;
            CancelInvoke("Spawn");
        }
    }

    private void Spawn()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Count);
        int zombiePrefabIndex = Random.Range(0, zombiePrefabs.Count);

        Transform spawnPoint = spawnPoints[spawnPointIndex];

        GameObject zombie = Instantiate(zombiePrefabs[zombiePrefabIndex]);
        Zombie zombieComp = zombie.GetComponent<Zombie>();

        zombieComp.SetTarget(player);
        zombie.transform.position = spawnPoint.position;
    }

    private void UpdateSpawnInterval()
    {
        //cancel current spawning
        ToggleSpawnning();

        if(wave <= 4)
        {
            spawnInterval -= spawnIntervalDec;
            wave++;
        }
        else
        {
            CancelInvoke("UpdateSpawnInterval");
        }

        //start spawnning again
        ToggleSpawnning();
    }
}
