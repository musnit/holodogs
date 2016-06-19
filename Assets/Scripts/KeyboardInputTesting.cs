using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeyboardInputTesting : MonoBehaviour {

    public GameObject ref1;
    public GameObject cursor;

    [HideInInspector]
    public GameObject activePiecePrefab;

    // Use this for initialization
    void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && ref1)
        {
            Dictionary<string, GameObject> paramaters = new Dictionary<string, GameObject>();
            print(ref1.name);
            ref1.SendMessage("OnSelect");
        }
        if (Input.GetKeyDown(KeyCode.Return) && ref1)
        {
            print(ref1.name);
            ref1.SendMessageUpwards("OnHover");
        }
        if (Input.GetKeyDown(KeyCode.Backspace) && ref1)
        {
            print(ref1.name);
            ref1.SendMessageUpwards("OffHover");
        }

    }
}
