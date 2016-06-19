using UnityEngine;
using System.Collections;

public class SelfDestructingProjectile : MonoBehaviour {
    float spawnTime;
    public float lifeLength = 6;

    // Use this for initialization
    void Start () {
        spawnTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update () {
        if (Time.realtimeSinceStartup - spawnTime > lifeLength)
        {
            Destroy(gameObject);
        }
    }
}
