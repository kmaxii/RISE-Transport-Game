using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float rotationSpeed = 10.0f;

    
    [Header("Raycast settings")]
    [SerializeField] private LayerMask ground;

    [Tooltip("The distance the starting ray is away from the player.")]
    [SerializeField] private float rayDistance = 1.0f;

    [Tooltip("Any extra y to start the raycasts at")]
    [SerializeField] private float rayYOffset;
    
    [Tooltip("The amount of rays that are shot down in the circle around the player, split perfectly in a round circle")]
    [SerializeField] private int rayCount = 30;

    [Tooltip("Max number of times to expand the search radius in case none of the rays hit a road")]
    [SerializeField] private int maxIterations = 5; 
    [Tooltip("Distance to increment the search radius")]
    [SerializeField] private float distanceIncrement = 0.5f;  

    


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
        bool foundRoad = false;
        float currentRayDistance = rayDistance;
        Vector3 bestDirection = Vector3.zero;

        Transform selfTransform = transform;
        Vector3 offset = new Vector3(0, rayYOffset, 0);

        for (int iteration = 0; iteration < maxIterations && !foundRoad; iteration++)
        {
            float closestDot = -Mathf.Infinity;

            for (int i = 0; i < rayCount; i++)
            {
                float angle = (360f / rayCount) * i;
                Vector3 dir = Quaternion.Euler(0, angle, 0) * Vector3.forward;
                Ray ray = new Ray(transform.position + dir * currentRayDistance + offset, Vector3.down);

                if (Physics.Raycast(ray, out var hit, currentRayDistance, ground))
                {
                    Vector3 hitDirection = new Vector3(hit.point.x, transform.position.y, hit.point.z) - selfTransform.position;
                    float dotProduct = Vector3.Dot(_inputDirection.normalized, hitDirection.normalized);

                    if (dotProduct > closestDot)
                    {
                        closestDot = dotProduct;
                        bestDirection = hitDirection;
                        foundRoad = true;
                    }
                }
            }
            // Increase the ray distance for the next iteration which occurs if not ray has hit
            currentRayDistance += distanceIncrement;
        }

        if (bestDirection != Vector3.zero)
        {
            // Lerp towards the best direction
            selfTransform.position = Vector3.Lerp(transform.position, selfTransform.position + bestDirection, moveSpeed * Time.deltaTime);

            // Rotate the player to face the direction of movement
            Quaternion lookRotation = Quaternion.LookRotation(bestDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }

    void OnDrawGizmos()
    {
        Vector3 offset = new Vector3(0, rayYOffset, 0);
        float currentRayDistance = rayDistance;

        for (int iteration = 0; iteration < maxIterations; iteration++)
        {
            // Use a different color for each iteration for clarity
            Gizmos.color = Color.Lerp(Color.red, Color.blue, (float)iteration / maxIterations);

            for (int i = 0; i < rayCount; i++)
            {
                float angle = (360f / rayCount) * i;
                Vector3 dir = Quaternion.Euler(0, angle, 0) * Vector3.forward;
                Vector3 rayOrigin = transform.position + dir * currentRayDistance + offset;

                Gizmos.DrawLine(rayOrigin, rayOrigin + Vector3.down * currentRayDistance);
            }

            currentRayDistance += distanceIncrement;
        }
    }
}