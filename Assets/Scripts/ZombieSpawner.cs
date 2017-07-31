using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : SingletonComponent<ZombieSpawner> {

    public GameObject zombie;

    public float spawnInterval = 1f;
    public int maxZombies = 20;

    private int realMax = 0;

    private float time = 0f;

    private int zombieCount = 0;

    private bool started = false;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;

        if (started)
        {
            if (time > spawnInterval)
            {
                if (zombieCount < realMax)
                {
                    SpawnZombie(this.transform.position - new Vector3(3f, 0f, 0.25f));
                }

                realMax++;
                if (realMax > maxZombies)
                    realMax = maxZombies;
            }
        }
        else
        {
            if(time > 5f)
            {
                started = true;
            }
        }
	}

    public void SpawnZombie(Vector3 location)
    {
        Instantiate(zombie, location, new Quaternion());

        zombieCount++;
    }

    public void RemoveZombie()
    {
        zombieCount--;
    }
}
