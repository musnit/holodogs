using UnityEngine;
using System.Collections;

public class SingleSpawner : MonoBehaviour {

    public GameObject objectPrefab;
    public Vector3 scaleVector;

    [HideInInspector]
    public GameObject spawnedObject;

    // Use this for initialization
    void Start () {
        spawnedObject = (GameObject)Instantiate(objectPrefab);
        spawnedObject.transform.position = transform.position;
        spawnedObject.transform.localScale = scaleVector;
        spawnedObject.transform.parent = transform.parent.parent;
    }

    // Update is called once per frame
    void Update () {
	
	}
}
