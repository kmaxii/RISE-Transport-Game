using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class MapInteractor : MonoBehaviour
{

    private readonly HashSet<MapInteractable> _interactables = new HashSet<MapInteractable>();

    private bool _canInteract = false;
    
    public bool CanInteract => _canInteract;

    [SerializeField] private UnityEvent canNowInteract;
    [SerializeField] private UnityEvent canNoLongerInteract;
    


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("MapInteractable"))
            return;

        MapInteractable mapInteractable = other.GetComponent<MapInteractable>();
        
        _interactables.Add(mapInteractable);
        

        if (!_canInteract)
        {
            _canInteract = true;
            canNowInteract.Invoke();
        }
    }

    public void InteractWithClosest()
    {
        if (!_canInteract)
            return;
        
        MapInteractable closest = null;
        float closestDistance = float.MaxValue;
        
        foreach (MapInteractable interactable in _interactables)
        {
            float distance = Vector3.Distance(transform.position, interactable.transform.position);
            
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = interactable;
            }
        }

        if (closest != null)
            closest.Interact();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("MapInteractable"))
            return;
        
        MapInteractable mapInteractable = other.GetComponent<MapInteractable>();

        _interactables.Remove(mapInteractable);

        UpdateInteractableState();
    }

    public void UpdateInteractableState()
    {
        //Go through the entire list and check if any of the objects have gotten destroyed
        foreach (var interactable in _interactables.Where(interactable => interactable == null))
        {
            _interactables.Remove(interactable);
        }


        if (_interactables.Count != 0) return;
        _canInteract = false;
        canNoLongerInteract.Invoke();
    }
    
    
}
