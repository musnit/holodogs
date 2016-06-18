using UnityEngine;
using HoloToolkit.Unity;

/// <summary>
/// CursorManager class takes Cursor GameObjects.
/// One that is on Holograms and another off Holograms.
/// 1. Shows the appropriate Cursor when a Hologram is hit.
/// 2. Places the appropriate Cursor at the hit position.
/// 3. Matches the Cursor normal to the hit surface.
/// </summary>
public class CursorManager : Singleton<CursorManager>
{
    [Tooltip("Drag the Cursor object to show when it hits a hologram.")]
    public GameObject CursorOnHolograms;

    [Tooltip("Drag the Cursor object to show when it does not hit a hologram.")]
    public GameObject CursorOffHolograms;

    [Tooltip("Distance, in meters, to offset the cursor from the collision point.")]
    public float DistanceFromCollision = 0.01f;

    void Awake()
    {
        if (CursorOnHolograms == null || CursorOffHolograms == null)
        {
            return;
        }

        // Hide the Cursors to begin with.
        CursorOnHolograms.SetActive(false);
        CursorOffHolograms.SetActive(false);
    }

    void LateUpdate()
    {
        if (GazeManager.Instance == null || CursorOnHolograms == null || CursorOffHolograms == null)
        {
            return;
        }

        GameObject activeCursor, inactiveCursor;
        if (GazeManager.Instance.Hit) {
            activeCursor = CursorOnHolograms;
            inactiveCursor = CursorOffHolograms;
        } else {
            activeCursor = CursorOffHolograms;
            inactiveCursor = CursorOnHolograms;
        }

        activeCursor.SetActive(true);
        inactiveCursor.SetActive(false);

        // Place the cursor at the calculated position.
        Vector3 position = GazeManager.Instance.Position + GazeManager.Instance.Normal * DistanceFromCollision;
        activeCursor.transform.position = position;

        // Orient the cursor to match the surface being gazed at.
        activeCursor.transform.up = Quaternion.AngleAxis(90, Vector3.right) * GazeManager.Instance.Normal;
    }
}