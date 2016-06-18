using UnityEngine;
using HoloToolkit.Unity;

public class RecognitionStatus : Singleton<RecognitionStatus>
{
    public GameObject recognitionLabel;

    TextMesh content;

    // Use this for initialization
    void Start () {
        content = recognitionLabel.GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update () {
        RaycastHit hitInfo = GazeManager.Instance.HitInfo;
        content.text = hitInfo.point.x + ", " + hitInfo.point.y + ", " + hitInfo.point.z;
    }
}
