using UnityEngine;
using UnityEngine.Events;

public class CameraRotationTrigger : MonoBehaviour
{
    public sCameraInterior cameraController;
    public Vector3 roomOffset = new Vector3(0, 8, -6);
    public Vector3 roomRotation = new Vector3(45, 90, 0);

    bool entered = false;
    public UnityEvent onEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cameraController.SetCameraTarget(roomOffset, roomRotation);
            other.GetComponent<Player>().rotateControls = true;

            if (!entered)
                onEnter.Invoke();
            entered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cameraController.ResetCamera();
            other.GetComponent<Player>().rotateControls = false;
        }
    }
}
