using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask ground;

    [SerializeField] private float rayDistance = 1.0f;
    [SerializeField] private int rayCount = 30;
    [SerializeField] private float moveSpeed = 5.0f;
    
    
    private Vector3 _inputDirection;


    void FixedUpdate()
    {
        // Get input direction (modify based on your input system)
        _inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // Only move if there is input
        if (_inputDirection != Vector3.zero)
        {
            MovePlayer();
        }
    }

    void MovePlayer()
    {
        Vector3 bestDirection = Vector3.zero;
        float closestDot = -Mathf.Infinity;

        Transform selfTransform = transform;
        for (int i = 0; i < rayCount; i++)
        {
            float angle = (360f / rayCount) * i;
            Vector3 dir = Quaternion.Euler(0, angle, 0) * Vector3.forward;
            Ray ray = new Ray(transform.position + dir * rayDistance, Vector3.down);

            if (Physics.Raycast(ray, out var hit, rayDistance, ground))
            {
                Vector3 hitDirection = new Vector3(hit.point.x, transform.position.y, hit.point.z) - selfTransform.position;
                float dotProduct = Vector3.Dot(_inputDirection.normalized, hitDirection.normalized);

                if (dotProduct > closestDot)
                {
                    closestDot = dotProduct;
                    bestDirection = hitDirection;
                }
            }
        }

        if (bestDirection != Vector3.zero)
        {
            // Lerp towards the best direction
            selfTransform.position = Vector3.Lerp(transform.position, selfTransform.position + bestDirection,
                moveSpeed * Time.deltaTime);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < rayCount; i++)
        {
            float angle = (360f / rayCount) * i;
            Vector3 dir = Quaternion.Euler(0, angle, 0) * Vector3.forward;
            Vector3 rayOrigin = transform.position + dir * rayDistance;

            Gizmos.DrawLine(rayOrigin, rayOrigin + Vector3.down * rayDistance);
        }
    }
}