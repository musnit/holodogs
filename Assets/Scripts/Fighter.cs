using UnityEngine;
using System.Collections;

public class Fighter : MonoBehaviour
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
    private float health = 100;

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentTarget = target;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTarget == null)
        {
            Debug.Log(name + ": 8-D");
            currentTarget = target;
        }
        // Determine the direction to the target
        Vector3 direction = currentTarget.transform.position - transform.position;
        // Ignore its height
        direction.y = 0;
        // Look at the target
        transform.rotation = Quaternion.LookRotation(direction);
        // Always apply gravity
        Vector3 movement = gravity;
        // Move towards the target if its out of range
        if (direction.magnitude > weaponRange)
        {
            direction.Normalize();
            movement += direction * movespeed;
        }
        else
        {
            float now = Time.time;
            if ((now - lastShotFired) > reloadTime)
            {
                lastShotFired = now;
                Debug.Log(name + ": Fire!");
                currentTarget.SendMessage("OnFiredAt", damage);
            }
        }
        controller.Move(movement * Time.deltaTime);
    }

    void OnTriggerEnter(Collider contact)
    {
        GameObject enemy = contact.gameObject;
        Fighter enemyFighter = enemy.GetComponent<Fighter>();
        if (enemyFighter == null || enemyFighter == this)
            return;
        currentTarget = enemy;
    }

    void onTriggerExit(Collider contact)
    {
        if (contact.gameObject == currentTarget)
            currentTarget = target;
    }

    void OnFiredAt(float damage)
    {
        health -= damage;
        Debug.Log(name + ": Took " + damage + " damage, health now at " + health);
        if (health <= 0)
        {
            Debug.Log(name + ": X-(");
            Destroy(this.gameObject);
        }
    }
}
