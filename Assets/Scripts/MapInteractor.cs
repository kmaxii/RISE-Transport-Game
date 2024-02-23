using System.Collections.Generic;
using MaxisGeneralPurpose.Scriptable_objects;
using UnityEngine;

public class MapInteractor : MonoBehaviour
{
    [SerializeField] private GameEvent bussTaken;

    [SerializeField] private MapInteractorLogic bussInteractor;
    [SerializeField] private MapInteractorLogic scooterInteractor;
    [SerializeField] private MapInteractorLogic missionInteractor;

    [SerializeField] private List<GameEvent> updateInteractableStates;
    [SerializeField] private List<GameEventWithData> updateInteractableStates2;
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("MapInteractable")
            && !other.CompareTag("BussStation")
            && !other.CompareTag("Mission"))
            return;


        if (!other.TryGetComponent<MapInteractable>(out var mapInteractable))
        {
            return;
        }

        switch (other.tag)
        {
            case "BussStation":
                bussInteractor.AddInteractable(mapInteractable);
                break;
            case "Mission":
                missionInteractor.AddInteractable(mapInteractable);
                break;
            case "MapInteractable":
                scooterInteractor.AddInteractable(mapInteractable);
                break;
        }
    }
    
    
    
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("MapInteractable")
            && !other.CompareTag("BussStation")
            && !other.CompareTag("Mission"))
            return;

        if (!other.TryGetComponent<MapInteractable>(out var mapInteractable))
        {
            return;
        }

        switch (other.tag)
        {
            case "BussStation":
                bussInteractor.RemoveInteractable(mapInteractable);
                break;
            case "Mission":
                missionInteractor.RemoveInteractable(mapInteractable);
                break;
            case "MapInteractable":
                scooterInteractor.RemoveInteractable(mapInteractable);
                break;
        }
    }

    private void OnEnable()
    {
        bussInteractor.RegisterListeners();
        scooterInteractor.RegisterListeners();
        missionInteractor.RegisterListeners();
        
        foreach (var updateInteractableState in updateInteractableStates)
        {
            updateInteractableState.RegisterListener(bussInteractor.UpdateInteractableState);
            updateInteractableState.RegisterListener(scooterInteractor.UpdateInteractableState);
            updateInteractableState.RegisterListener(missionInteractor.UpdateInteractableState);
        }
        
        foreach (var updateInteractableState in updateInteractableStates2)
        {
            updateInteractableState.RegisterListener(bussInteractor.UpdateInteractableState);
            updateInteractableState.RegisterListener(scooterInteractor.UpdateInteractableState);
            updateInteractableState.RegisterListener(missionInteractor.UpdateInteractableState);
        }
    }

    private void OnDisable()
    {
        bussInteractor.UnregisterListeners();
        scooterInteractor.UnregisterListeners();
        missionInteractor.UnregisterListeners();
        
        foreach (var updateInteractableState in updateInteractableStates)
        {
            updateInteractableState.UnregisterListener(bussInteractor.UpdateInteractableState);
            updateInteractableState.UnregisterListener(scooterInteractor.UpdateInteractableState);
            updateInteractableState.UnregisterListener(missionInteractor.UpdateInteractableState);
        }
        
        foreach (var updateInteractableState in updateInteractableStates2)
        {
            updateInteractableState.UnregisterListener(bussInteractor.UpdateInteractableState);
            updateInteractableState.UnregisterListener(scooterInteractor.UpdateInteractableState);
            updateInteractableState.UnregisterListener(missionInteractor.UpdateInteractableState);
        }
    }
}