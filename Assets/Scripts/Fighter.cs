using UnityEngine;
using System.Collections;

public class Fighter : Destructable
{
    private static float movespeed = 3f;
    private static Vector3 gravity = Physics.gravity;

    public GameObject target;

    private CharacterController controller;
    private GameObject currentTarget;
    private float lastShotFired = 0f;
    private float weaponRange = 3f;
    private float reloadTime = 0.5f;
    private float damage = 10;

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentTarget = target;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTarget == null && currentTarget != target)
        {
            Debug.Log(name + ": 8-D");
            currentTarget = target;
        }

        Vector3 direction = Vector3.zero;
        if (currentTarget != null)
        {
            // Determine the direction to the target
            direction = currentTarget.transform.position - transform.position;
            // Ignore its height
            direction.y = 0;
            // Look at the target
            transform.rotation = Quaternion.LookRotation(direction);
        }
        
        // Always apply gravity
        Vector3 movement = gravity;
        // Move towards the target if its out of range
        if (direction.magnitude > weaponRange)
        {
            direction.Normalize();
            movement += direction * movespeed;
        }
        else if (currentTarget != null)
        {
            float now = Time.time;
            if ((now - lastShotFired) > reloadTime)
            {
                lastShotFired = now;
                Debug.Log(name + ": Firing at " + currentTarget.name);
                currentTarget.SendMessage("OnFiredAt", damage);
            }
        }
        controller.Move(movement * Time.deltaTime);
    }

    void OnTriggerEnter(Collider contact)
    {
        GameObject contactObject = contact.gameObject;
        Destructable enemy = contactObject.GetComponent<Destructable>();
        if (enemy == null || enemy.gameObject == this.gameObject)
            return;
        currentTarget = enemy.gameObject;
    }

    void onTriggerExit(Collider contact)
    {
        if (contact.gameObject == currentTarget)
            currentTarget = target;
    }
}
