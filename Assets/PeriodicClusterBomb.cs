using UnityEngine;
using System.Collections;
using System;

public class PeriodicClusterBomb : MonoBehaviour {

    float lastSpawnTime;
    public float spawnInterval = 3f;
    public GameObject clusterPrefab;

	// Use this for initialization
	void Start () {
        lastSpawnTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update () {
	    if (Time.realtimeSinceStartup - lastSpawnTime > spawnInterval)
        {
            lastSpawnTime = Time.realtimeSinceStartup;
            SpawnCluster();
        }
	}

    private void SpawnCluster()
    {
        for (int i=0; i< 6; i++){
            GameObject clusterBomb = GameObject.Instantiate(clusterPrefab);
            clusterBomb.transform.position = transform.position;
            clusterBomb.transform.Rotate(i * 20 * UnityEngine.Random.Range(0, 3), i * 20 * UnityEngine.Random.Range(0, 3), i * 60);
            Rigidbody rbody = clusterBomb.AddComponent<Rigidbody>();
            rbody.velocity = (clusterBomb.transform.forward * 0.5f);
            clusterBomb.AddComponent<SelfDestructingProjectile>();
        }
    }
}
