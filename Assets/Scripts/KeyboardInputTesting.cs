using UnityEngine;
using System.Collections;

public class KeyboardInputTesting : MonoBehaviour {

    public GameObject ref1;

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
            print(ref1.name);
            ref1.SendMessage("OnSelect", activePiecePrefab);
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
