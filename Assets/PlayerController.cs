using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float maxDistanceToGround = 1.0f;
    public float moveSmoothTime = 0.1f;

    [SerializeField] private LayerMask ground;

    private HashSet<Collider> _currentColliding = new HashSet<Collider>();
    private SphereCollider _sphereCollider;

    private Vector3 _velocity;

    private void Awake()
    {
        _sphereCollider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _currentColliding.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        _currentColliding.Remove(other);
    }

    void FixedUpdate()
    {
        MovePlayer();
      //  KeepPlayerOnGround();
    }

    void MovePlayer()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 targetMovement = new Vector3(moveHorizontal, 0.0f, moveVertical);//.normalized * speed;

        Vector3 targetPos = FindNearestRoadPoint(transform.position - targetMovement);
        targetMovement = transform.position - targetPos;
        
        
        
        _velocity = Vector3.Lerp(_velocity, targetMovement, moveSmoothTime);
        transform.position += _velocity * Time.fixedDeltaTime;
    }

    void KeepPlayerOnGround()
    {
        if (!Physics.Raycast(transform.position, -Vector3.up, out RaycastHit hit, maxDistanceToGround, ground))
        {
            Vector3 nearestPoint = FindNearestRoadPoint(transform.position);
            transform.position = nearestPoint; //Vector3.MoveTowards(transform.position, nearestPoint, speed * Time.fixedDeltaTime);
        }
    }

    private Vector3 FindNearestRoadPoint(Vector3 pos)
    {
        float closestDistance = Mathf.Infinity;
        Vector3 closestPoint = Vector3.zero;

        foreach (var col in _currentColliding)
        {
            if (CheckSphereExtra(col, _sphereCollider, pos, out Vector3 closestPointOnCollider, out Vector3 surfaceNormal))
            {
                float distance = Vector3.Distance(transform.position, closestPointOnCollider);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPoint = closestPointOnCollider;
                }
            }
        }

        Debug.Log(closestDistance);


        closestPoint.y = 1;
        return closestPoint;
    }

    private static bool CheckSphereExtra(Collider targetCollider, SphereCollider sphereCollider, Vector3 pos,
        out Vector3 closestPoint, out Vector3 surfaceNormal)
    {
        closestPoint = Vector3.zero;
        surfaceNormal = Vector3.zero;

      //  Vector3 spherePos = sphereCollider.transform.position;

    //    Vector3 spherePosToPos = pos - spherePos;
        
        if (Physics.ComputePenetration(targetCollider, targetCollider.transform.position,
                targetCollider.transform.rotation, sphereCollider, pos, Quaternion.identity,
                out surfaceNormal, out var surfacePenetrationDepth))
        {
            closestPoint = pos + (surfaceNormal * (sphereCollider.radius - surfacePenetrationDepth));
            surfaceNormal = -surfaceNormal;

            return true;
        }

        return false;
    }
}