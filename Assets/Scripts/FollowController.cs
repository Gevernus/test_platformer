using UnityEngine;

public class FollowController : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private Vector3 _cachedOffset;

    private void Start()
    {
        _cachedOffset = offset;
    }

    void Update()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    public void Direction(bool forward)
    {
        offset.x = forward ? _cachedOffset.x : -_cachedOffset.x;
    }
}