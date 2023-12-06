using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask ground;

    [SerializeField] private float rayDistance = 1.0f;
    [SerializeField] private int rayCount = 30;
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float rotationSpeed = 10.0f;

    [SerializeField] private float rayYOffset;


    private Vector3 _inputDirection;
    private Animator _animator;
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");

    private void Start()
    {
        if (!TryGetComponent(out _animator))
        {
            Debug.LogError(transform.name + " is missing an animator");
        }
    }

    void FixedUpdate()
    {
        _inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        bool isMoving = _inputDirection != Vector3.zero;

        if (isMoving)
            MovePlayer();
        
        _animator.SetBool(IsWalking, isMoving);
    }

    void MovePlayer()
    {
        Vector3 bestDirection = Vector3.zero;
        float closestDot = -Mathf.Infinity;

        Transform selfTransform = transform;

        Vector3 offset = new Vector3(0, rayYOffset, 0);

        for (int i = 0; i < rayCount; i++)
        {
            float angle = (360f / rayCount) * i;
            Vector3 dir = Quaternion.Euler(0, angle, 0) * Vector3.forward;
            Ray ray = new Ray(transform.position + dir * rayDistance + offset, Vector3.down);

            if (Physics.Raycast(ray, out var hit, rayDistance, ground))
            {
                Vector3 hitDirection = new Vector3(hit.point.x, transform.position.y, hit.point.z) -
                                       selfTransform.position;
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

            // Rotate the player to face the direction of movement
            Quaternion lookRotation = Quaternion.LookRotation(bestDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 offset = new Vector3(0, rayYOffset, 0);


        for (int i = 0; i < rayCount; i++)
        {
            float angle = (360f / rayCount) * i;
            Vector3 dir = Quaternion.Euler(0, angle, 0) * Vector3.forward;
            Vector3 rayOrigin = transform.position + dir * rayDistance + offset;

            Gizmos.DrawLine(rayOrigin, rayOrigin + Vector3.down * rayDistance);
        }
    }
}