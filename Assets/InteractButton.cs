using System.Collections.Generic;
using MaxisGeneralPurpose.Scriptable_objects;
using QuickEye.Utility;
using UnityEngine;

public class InteractButton : MonoBehaviour
{
    [SerializeField] private UnityDictionary<BoolVariable, bool> valuesNeededToInteract;

    [SerializeField] private List<GameEvent> updateState;

    private bool _canInteract;

    private void OnEnable()
    {
        foreach (var gameEvent in updateState)
        {
            gameEvent.RegisterListener(UpdateState);
        }
    }

    private void OnDisable()
    {
        foreach (var gameEvent in updateState)
        {
            gameEvent.UnregisterListener(UpdateState);
        }
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

    private void ShowButton(bool value)
    {
        //Show or hide all children
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(value);
        }
    }
}