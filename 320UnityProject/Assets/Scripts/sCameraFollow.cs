using UnityEngine;

public class sCameraFollow : MonoBehaviour
{
    public Transform player;

    public Vector3 offset = new Vector3(-7.55f, 6.35f, -7.89f);
    public float followSpeed = 5f;

    private Vector3 targetOffset;

    void Start()
    {
        targetOffset = offset;
    }

    void LateUpdate()
    {
        //Smooth follow
        Vector3 desiredPosition = player.position + targetOffset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        //Always look at player
        transform.LookAt(player.position);
    }
}

