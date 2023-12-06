using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerFollower : MonoBehaviour
{
    
    [Header("CameraSettings")]
    [SerializeField]
    Camera cam;
    Vector3 previousPos = Vector3.zero;
    Vector3 deltaPos = Vector3.zero;

    void CamControl()
    {
        deltaPos = transform.position - previousPos;
        deltaPos.y = 0;
        cam.transform.position = Vector3.Lerp(cam.transform.position, cam.transform.position + deltaPos, Time.time);
        previousPos = transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        CamControl();
    }
}
