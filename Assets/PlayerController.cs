using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float maxDistanceToGround = 1.0f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        MovePlayer();
        KeepPlayerOnGround();
    }

    void MovePlayer()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;
    }

    void KeepPlayerOnGround()
    {
        RaycastHit hit;

        if (!Physics.Raycast(transform.position, -Vector3.up, out hit, maxDistanceToGround,
                LayerMask.GetMask("Road")))
        {
            Vector3 nearestPoint = FindNearestPointOnGround();
            transform.position = nearestPoint;
        }
    }

    Vector3 FindNearestPointOnGround()
    {
        Collider[] colliders =
            Physics.OverlapSphere(transform.position, maxDistanceToGround, LayerMask.GetMask("Road"));
        float closestDistance = Mathf.Infinity;
        Vector3 closestPoint = Vector3.zero;

        
        foreach (var col in colliders)
        {
            Vector3 closestPointOnCollider = col.ClosestPoint(transform.position);
            float distance = Vector3.Distance(transform.position, closestPointOnCollider);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPoint = closestPointOnCollider;
            }
        }

        return closestPoint;
    }
}