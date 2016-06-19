using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    public GameObject piecePrefab;
    public GameObject dumbPrefab;
    public GameObject hoverSignalPrefab;

    private GameObject hoverSignal;

    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelect()
    {
        CursorChanger.Instance.changeCursor(piecePrefab, dumbPrefab);
    }

    void OnHover()
    {
        if (hoverSignal != null)
        {
            GameObject.Destroy(hoverSignal);
        }
        hoverSignal = GameObject.Instantiate(hoverSignalPrefab);
        hoverSignal.transform.parent = transform;
        hoverSignal.transform.localPosition = new Vector3(0, 0.01f, 0);
    }

    void OffHover()
    {
        GameObject.Destroy(hoverSignal);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
