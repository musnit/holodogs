using HoloToolkit.Unity;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class GazeGestureManager : MonoBehaviour
{
    public static GazeGestureManager Instance { get; private set; }

    [HideInInspector]
    public GameObject activePiecePrefab = null;

    public GameObject cursor;

    // Represents the hologram that is currently being gazed at.
    public GameObject FocusedObject { get; private set; }

    GestureRecognizer recognizer;

    // Use this for initialization
    void Start()
    {
        Instance = this;

        // Set up a GestureRecognizer to detect Select gestures.
        recognizer = new GestureRecognizer();

        recognizer.TappedEvent += (source, tapCount, ray) =>
        {

            if (FocusedObject != null)
            {
                FocusedObject.SendMessage("OnSelect");
            }
            if (activePiecePrefab != null && //piece is selected and
            (FocusedObject.GetComponent<SurfacePlane>() == null || //not a plane..or
            (FocusedObject.GetComponent<SurfacePlane>() != null && FocusedObject.GetComponent<SurfacePlane>().isSpawned() == true)) && //is a spawned place
            FocusedObject.GetComponent<ChangeCursor>() == null //not a piece selector
            )
            {
                GameObject newObject = GameObject.Instantiate(activePiecePrefab);
                newObject.transform.position = cursor.transform.position;
                if (newObject.GetComponent<HotdogController>() != null)
                {
                    newObject.GetComponent<HotdogController>().target = cursor.transform;
                }
            }
        };
        recognizer.StartCapturingGestures();
    }

    // Update is called once per frame
    void Update()
    {
        // Figure out which hologram is focused this frame.
        GameObject oldFocusObject = FocusedObject;

        // Do a raycast into the world based on the user's
        // head position and orientation.
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;
        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            if (FocusedObject != hitInfo.collider.gameObject)
            {
                if (FocusedObject != null)
                {
                    FocusedObject.SendMessageUpwards("OffHover", null, SendMessageOptions.DontRequireReceiver);
                }
                FocusedObject = hitInfo.collider.gameObject;
                FocusedObject.SendMessageUpwards("OnHover", null, SendMessageOptions.DontRequireReceiver);
            }
        }
        else
        {
            // If the raycast did not hit anything, clear the focused object.
            if (FocusedObject != null)
            {
                FocusedObject.SendMessageUpwards("OffHover", null, SendMessageOptions.DontRequireReceiver);
                FocusedObject = null;
            }
        }

        // If the focused object changed this frame,
        // start detecting fresh gestures again.
        if (FocusedObject != oldFocusObject)
        {
            recognizer.CancelGestures();
            recognizer.StartCapturingGestures();
        }
    }
}
