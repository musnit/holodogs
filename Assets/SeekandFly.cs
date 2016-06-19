using UnityEngine;
using System.Collections;

public class SeekandFly : MonoBehaviour {

    private bool flying = false;
    Vector3 destination;
    public float speed = 1f;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!flying)
        {
            GameObject[] terrains = GameObject.FindGameObjectsWithTag("Terrain");

            if (terrains.Length == 0)
            {

            }
            else
            {
                int terrainToFly = Random.Range(0, terrains.Length); // creates a number between 1 and 12
                destination = terrains[terrainToFly].transform.position + new Vector3(0, 0.3f, 0);
                Vector3 directionToFly = (destination - transform.position).normalized * speed;
                gameObject.GetComponent<Rigidbody>().velocity = directionToFly;
                flying = true;
            }
        }
        if (flying)
        {
            float sqrMag = (destination - transform.position).sqrMagnitude;
            if( sqrMag < 0.01){
                flying = false;
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }

    }
}
