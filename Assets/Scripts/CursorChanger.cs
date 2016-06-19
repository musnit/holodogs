using UnityEngine;
using System.Collections;

public class CursorChanger : MonoBehaviour {

    public GameObject gestureManager;
    public GameObject keybManager;
    public GameObject cursor;
    public static CursorChanger Instance { get; private set; }

    // Use this for initialization
    void Start () {
        Instance = this;
    }

    public void changeCursor(GameObject piecePrefab)
    {
        GameObject cursorPiece = GameObject.Instantiate(piecePrefab);
        cursorPiece.transform.parent = cursor.transform;
        cursorPiece.transform.localPosition = Vector3.zero;
        gestureManager.GetComponent<GazeGestureManager>().activePiecePrefab = piecePrefab;
        keybManager.GetComponent<KeyboardInputTesting>().activePiecePrefab = piecePrefab;
    }

    // Update is called once per frame
    void Update () {
	
	}
}
