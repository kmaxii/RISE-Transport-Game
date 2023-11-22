using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGameObject : MonoBehaviour
{

    [SerializeField] private Transform target;
    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPos = target.position;
        var transformRef = transform;
        newPos.y = transformRef.position.y;
        transformRef.position = newPos;
    }
}
