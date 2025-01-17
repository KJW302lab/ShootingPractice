using UnityEngine;

public class GunRaycaster : MonoBehaviour
{
    [SerializeField] Gradient notDetected;
    [SerializeField] Gradient detected;

    [SerializeField] Transform rayOrigin;
    [SerializeField] float rayDistance = 30f;
    [SerializeField] LayerMask targetLayer;

    private LineRenderer _lineRenderer;
    RaycastHit _hit;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();

        _lineRenderer.positionCount = 2;
        _lineRenderer.startWidth = 0.05f;
        _lineRenderer.endWidth = 0.05f;
        _lineRenderer.useWorldSpace = true;
    }

    private void Update()
    {
        Vector3 rayStart = rayOrigin.position;
        Vector3 rayDirection = rayOrigin.forward;

        _lineRenderer.SetPosition(0, rayStart);

        if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out _hit, rayDistance, targetLayer))
        {
            Debug.Log($"Hit {_hit.collider.name}");

            _lineRenderer.SetPosition(1, _hit.point);
            _lineRenderer.colorGradient = detected;
        }
        else
        {
            _lineRenderer.SetPosition(1, rayStart + rayDirection * rayDistance);
            _lineRenderer.colorGradient = notDetected;
        }
    }
}
