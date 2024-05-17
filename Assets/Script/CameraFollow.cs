
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.5f;
    [SerializeField] private Vector3 offset;

    void Update()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }

    }
    public void FindPlayer()
    {
        target = FindAnyObjectByType<PlayerController>().transform;
    }

}
