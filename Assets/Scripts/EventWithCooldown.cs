using System;
using System.Collections;
using System.Collections.Generic;
using MaxisGeneralPurpose.Scriptable_objects;
using UnityEngine;

public class EventWithCooldown : MonoBehaviour
{

    [SerializeField] private List<GameEvent> gameEventWhichTrigger;

    [SerializeField] private GameEvent gameEventToTrigger;
    
    [SerializeField] private float cooldownTime = 1f;
    
    private bool _isOnCooldown = false;
    
    private void OnEnable()
    {
        foreach (var gameEvent in gameEventWhichTrigger)
        {
            gameEvent.RegisterListener(TriggerEventWithCooldown);
        }
    }
    
    private void OnDisable()
    {
        foreach (var gameEvent in gameEventWhichTrigger)
        {
            gameEvent.UnregisterListener(TriggerEventWithCooldown);
        }
    }
    
    public void TriggerEventWithCooldown()
    {
        if (!_isOnCooldown)
        {
            _isOnCooldown = true;

            gameEventToTrigger.Raise();

            Invoke(nameof(ResetCooldown), cooldownTime);
        }
    }

    public void ResetCooldown()
    {
        _isOnCooldown = false;
    }


}
