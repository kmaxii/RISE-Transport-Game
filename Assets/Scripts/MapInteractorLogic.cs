using System;
using System.Collections.Generic;
using System.Linq;
using MaxisGeneralPurpose.Scriptable_objects;
using UnityEngine;

[Serializable]
public class MapInteractorLogic
{
    private readonly HashSet<MapInteractable> _interactables = new();
    
    [SerializeField] private GameEvent canNowInteract;
    [SerializeField] private GameEvent canNoLongerInteract;

    [Tooltip("The event that should be called to interact with the closest interactable.")]
    [SerializeField] private GameEvent interactWith;
    [SerializeField] private GameEvent clearList;
    
    [SerializeField] private BoolVariable canInteract;

    [SerializeField] private Transform player;

    
    public void AddInteractable(MapInteractable interactable)
    {
        _interactables.Add(interactable);
     
        Debug.Log("Can now interact");

        
        if (canInteract.Value) return;
        
        canInteract.Value = true;
        canNowInteract.Raise();
    }
    
    public void RemoveInteractable(MapInteractable interactable)
    {
        if (!_interactables.Remove(interactable)) return;
        
        UpdateInteractableState();
    }
    
    public void InteractWithClosest()
    {
        MapInteractable closest = GetClosest();
        
        if (closest != null)
            closest.Interact();
        
    }
    private MapInteractable GetClosest()
    {
        UpdateInteractableState();
        
        if (!canInteract.Value)
            return null;
        
        MapInteractable closest = null;
        float closestDistance = float.MaxValue;
        
        foreach (MapInteractable interactable in _interactables)
        {
            float distance = Vector3.Distance(player.position, interactable.transform.position);
            
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = interactable;
            }
        }

        return closest;
    }
    
    public void UpdateInteractableState()
    {
        //Go through the entire list reverse and check if any of the objects have gotten destroyed, if they have remove them.
        for (int i = _interactables.Count - 1; i >= 0; i--)
        {
            var interactable = _interactables.ElementAt(i);
            if (interactable == null)
            {
                _interactables.Remove(interactable);
            }
        }

        if (_interactables.Count != 0) return;
        canInteract.Value = false;
        canNoLongerInteract.Raise();
    }

    public void RegisterListeners()
    {
        interactWith.RegisterListener(InteractWithClosest);
        clearList.RegisterListener(ClearList);
    }
    
    public void UnregisterListeners()
    {
        interactWith.UnregisterListener(InteractWithClosest);
        clearList.UnregisterListener(ClearList);
    }
    

    public void ClearList()
    {
        _interactables.Clear();
        canInteract.Value = false;
        canNoLongerInteract.Raise();
    }

}