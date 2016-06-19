using UnityEngine;
using System.Collections;
using System;

public class CursorChanger : MonoBehaviour {

    public GameObject gestureManager;
    public GameObject keybManager;
    public GameObject cursor;
    private GameObject currentCursor;

    public static CursorChanger Instance { get; private set; }

    // Use this for initialization
    void Start () {
        Instance = this;
    }

    public void changeCursor(GameObject piecePrefab, GameObject dumbPrefab)
    {
        if (currentCursor != null)
        {
            GameObject.Destroy(currentCursor);
        }
        currentCursor = GameObject.Instantiate(dumbPrefab);
        currentCursor.transform.parent = cursor.transform;
        currentCursor.transform.localPosition = Vector3.zero;
        gestureManager.GetComponent<GazeGestureManager>().activePiecePrefab = piecePrefab;
        keybManager.GetComponent<KeyboardInputTesting>().activePiecePrefab = piecePrefab;
    }


    // Update is called once per frame
    void Update () {
	
	}
}
