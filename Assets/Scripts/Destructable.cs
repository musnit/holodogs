using UnityEngine;
using System.Collections;

public class Destructable : MonoBehaviour {
    public float health = 100;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnFiredAt(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log(name + ": X-(");
            Destroy(this.gameObject);
        }
    }
}
