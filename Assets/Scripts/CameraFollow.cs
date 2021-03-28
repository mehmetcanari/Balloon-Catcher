using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public Vector3 smoothedPos;
    public float smoothSpeed = 0.125f;

    private void LateUpdate()
    {
        
    }

    public void lookAtPlayer()
    {
        Vector3 desiredPos = target.position + offset;
        smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
        transform.position = smoothedPos;
    }

}
