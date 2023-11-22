using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapIcon : MonoBehaviour
{
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position - Vector3.up);
    }
}
