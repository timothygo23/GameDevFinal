using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour {

    private List<Transform> spawnPoints = new List<Transform>();

    [SerializeField] private Transform player;
    [SerializeField] private List<GameObject> zombiePrefabs;
    [SerializeField] private float spawnRate = 3;

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
    }

    public void ToggleSpawnning()
    {
        if (!isSpawning)
        {
            //start the spawning
            isSpawning = true;
            InvokeRepeating("Spawn", 0, spawnRate);
        }
        else
        {
            isSpawning = false;
            CancelInvoke();
        }
    }

    private void Spawn()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Count);
        int zombiePrefabIndex = Random.Range(0, zombiePrefabs.Count);

        Debug.Log("SpawnPoint: " + spawnPointIndex);

        Transform spawnPoint = spawnPoints[spawnPointIndex];

        GameObject zombie = Instantiate(zombiePrefabs[zombiePrefabIndex]);
        ZombieHealth zombieHealth = zombie.GetComponent<ZombieHealth>();
        Zombie zombieComp = zombie.GetComponent<Zombie>();

        zombieComp.SetTarget(player);
        //zombieHealth.SetHealth(5); //change this nalang
        zombie.transform.position = spawnPoint.position;
    }

    public void SetSpawnRate(float spawnRate)
    {
        this.spawnRate = spawnRate;
    }
}
