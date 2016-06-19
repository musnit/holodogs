using UnityEngine;
using System.Collections;

public class WeenerProjectile : MonoBehaviour {



	// Use this for initialization
	void Start () {
		this.GetComponent<Rigidbody>().AddForce(transform.forward * 25f);
        gameObject.AddComponent<SelfDestructingProjectile>();
    }

    // Update is called once per frame
    void Update () {
	
	}
}
