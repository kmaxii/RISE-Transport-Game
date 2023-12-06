using UnityEngine;

public class MeshColliderSetter : MonoBehaviour
{
    // Time in seconds to delay the execution
    public float delayTime = 5f;

    void Start()
    {
        // Call the AddMeshCollider method after a specified delay
        Invoke(nameof(AddMeshCollider), delayTime);
    }

    void AddMeshCollider()
    {
        // Add a MeshCollider component to the GameObject this script is attached to
       // MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
        
        Debug.Log("Added mesh collider to " + transform.name);

        /*
        // Get the MeshFilter component from the GameObject
        MeshFilter meshFilter = GetComponent<MeshFilter>();

        // Check if the MeshFilter exists and has a valid mesh
        if (meshFilter != null && meshFilter.sharedMesh != null)
        {
            // Set the mesh of the MeshCollider to the mesh of the MeshFilter
            meshCollider.sharedMesh = meshFilter.sharedMesh;
        }
        else
        {
            Debug.LogError("MeshFilter not found or has no mesh!");
        }*/
    }
}