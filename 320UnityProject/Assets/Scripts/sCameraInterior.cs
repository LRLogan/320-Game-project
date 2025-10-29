using UnityEngine;

public class sCameraInterior : MonoBehaviour
{
    [Header("References")]
    public Transform player;

    [Header("Default Settings")]
    public Vector3 defaultOffset = new Vector3(0, 10, -10);
    public Vector3 defaultRotation = new Vector3(45, 45, 0);

    [Header("Transition Settings")]
    public float transitionSpeed = 2f;

    private Vector3 targetOffset;
    private Quaternion targetRotation;

    private void Start()
    {
        targetOffset = defaultOffset;
        targetRotation = Quaternion.Euler(defaultRotation);
    }

    private void LateUpdate()
    {
        if (player == null) return;

        // Follow player
        Vector3 desiredPosition = player.position + targetOffset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * transitionSpeed);

        // Smooth rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * transitionSpeed);
    }

    /// <summary>
    /// Call this to change the camera's rotation and offset.
    /// </summary>
    public void SetCameraTarget(Vector3 newOffset, Vector3 newRotation)
    {
        targetOffset = newOffset;
        targetRotation = Quaternion.Euler(newRotation);
    }

    /// <summary>
    /// Call this to reset to the default camera.
    /// </summary>
    public void ResetCamera()
    {
        targetOffset = defaultOffset;
        targetRotation = Quaternion.Euler(defaultRotation);
    }
}
