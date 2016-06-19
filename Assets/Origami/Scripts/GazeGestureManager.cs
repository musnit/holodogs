using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class GazeGestureManager : MonoBehaviour
{
    public static GazeGestureManager Instance { get; private set; }

    [HideInInspector]
    public GameObject activePiecePrefab;

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
            // Send an OnSelect message to the focused object and its ancestors.
            if (FocusedObject != null && FocusedObject.GetComponent<ChangeCursor>())
            {
                FocusedObject.SendMessage("OnSelect", activePiecePrefab);
            }
            else if (activePiecePrefab)
            {
                GameObject newObject = GameObject.Instantiate(activePiecePrefab);
                newObject.transform.position = cursor.transform.position;
            }
            else if (FocusedObject != null)
            {
                FocusedObject.SendMessage("OnSelect");
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
            if (hitInfo.collider.gameObject.GetComponent<HoloToolkit.Unity.SurfacePlane>())
            {
                // If the raycast hit a plane, use that as the focused object.
                FocusedObject = hitInfo.collider.gameObject;
                FocusedObject.SendMessageUpwards("OnHover", null, SendMessageOptions.DontRequireReceiver);
            }
            else if (FocusedObject)
            {
                // If raycast did hit walls.
                FocusedObject.SendMessageUpwards("OffHover", null, SendMessageOptions.DontRequireReceiver);
                FocusedObject = null;
            }
        }
        else
        {
            // If the raycast did not hit anything, clear the focused object.
            FocusedObject.SendMessageUpwards("OffHover", null, SendMessageOptions.DontRequireReceiver);
            FocusedObject = null;
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
