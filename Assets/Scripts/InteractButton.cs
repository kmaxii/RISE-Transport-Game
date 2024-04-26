using System;
using System.Collections.Generic;
using MaxisGeneralPurpose.Event;
using MaxisGeneralPurpose.Scriptable_objects;
using QuickEye.Utility;
using UnityEngine;

public class InteractButton : MonoBehaviour
{
    [SerializeField] private UnityDictionary<BoolVariable, bool> valuesNeededToInteract;

    [SerializeField] private List<GameEvent> updateState;
    [SerializeField] private List<GameEventWithData> updateState2;

    private bool _canInteract;

    private void OnEnable()
    {
        foreach (var gameEvent in updateState)
        {
            gameEvent.RegisterListener(UpdateState);
        }
        
        foreach (var gameEvent in updateState2)
        {
            gameEvent.RegisterListener(UpdateState2);
        }
    }

    private void OnDisable()
    {
        foreach (var gameEvent in updateState)
        {
            gameEvent.UnregisterListener(UpdateState);
        }
        
        foreach (var gameEvent in updateState2)
        {
            gameEvent.UnregisterListener(UpdateState2);
        }
    }
    
    private void UpdateState2(DataCarrier data)
    {
       UpdateState();
    }

    private void UpdateState()
    {
        bool canInteract = true;
        foreach (var valueNeeded in valuesNeededToInteract)
        {
            if (valueNeeded.Key.Value != valueNeeded.Value)
            {
                canInteract = false;
                break;
            }
        }

        if (_canInteract == canInteract)
            return;

        _canInteract = canInteract;

        ShowButton(canInteract);
    }

    private void Update()
    {
        UpdateState();
    }

    private void ShowButton(bool value)
    {
        //Show or hide all children
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(value);
        }
    }
}