using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HotdogController : MonoBehaviour {

	public GameObject bunCannonAxel;
	public GameObject weenerSpawner;
	public GameObject weenerProjectile;

	public Transform target;
    public float targetDistance;
    public float actualTargetDistance;
    public float maxTargetDistance = 1f;
	public float minTargetDistance;


    public bool fireTimerGo = false;

	bool fired = false;
	public bool readyToFire = true;

    //public float launchPower = 1000f;
    public float reloadWait = 1.0f;
    public float fireWait = 2.0f;

    public float timeSinceFired = 0f;
    public float timeSinceReload = 0f;
	//public float verticalAim = 0.5f;

	// Use this for initialization
	void Start () {



	}
	
	// Update is called once per frame
	void Update () {

        timeSinceFired += Time.deltaTime;
        timeSinceReload += Time.deltaTime;

        float currentReloadWait = reloadWait * actualTargetDistance / 2;
        float currentFireWait = fireWait * actualTargetDistance / 2;

        currentFireWait = Mathf.Min(currentFireWait, fireWait * 2.5f);
        currentFireWait = Mathf.Max(currentFireWait, fireWait * 0.1f);
        currentReloadWait = Mathf.Min(currentFireWait, reloadWait * 3f);
        currentReloadWait = Mathf.Max(currentFireWait, reloadWait * 0.1f);
   
        Aim();

		if (timeSinceFired >= currentReloadWait && !readyToFire) {
			readyToFire = true;
			weenerSpawner.SetActive(true);
            timeSinceReload = 0;
		}

        if (timeSinceReload > currentFireWait && readyToFire)
        {
            fireTimerGo = true;
            Debug.Log("umm");
        }

        if (fireTimerGo && readyToFire) {
			LaunchProjectile ();
            fireTimerGo = false;
		}

	}

	public void LaunchProjectile() {

		// Spawning the weener in bun as a Transfrom called clone
		readyToFire = false;
		timeSinceFired = 0.0f;
		weenerSpawner.SetActive(false);

		GameObject.Instantiate (weenerProjectile, weenerSpawner.transform.position, weenerSpawner.transform.rotation);

	}

	public void Aim(){
		Vector3 targetPos = new Vector3 (target.position.x, this.transform.position.y, target.position.z);
        actualTargetDistance = Vector3.Distance(targetPos, this.transform.position);
        targetDistance = Vector3.Distance (targetPos, this.transform.position);
		if (targetDistance > 1f) {
			targetDistance = 1f;
		}
		float cannonCrank = -(40 * targetDistance);
		Quaternion cannonTargetRot = Quaternion.Euler (cannonCrank, 0, 0);
		transform.LookAt (targetPos);
		bunCannonAxel.transform.localRotation = cannonTargetRot;

	}
}
