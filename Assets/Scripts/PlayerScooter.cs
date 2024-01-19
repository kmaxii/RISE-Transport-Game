using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using MaxisGeneralPurpose.Scriptable_objects;
using Scriptable_objects;
using UnityEngine;

public class PlayerScooter : MonoBehaviour 
{
    
    [SerializeField] private GameEvent timePassedEvent;

    private Time24H _startTime;
    
    [SerializeField] private TimeVariable currentTime;

    [SerializeField] private FloatVariable pickUpCost;
    [SerializeField] private FloatVariable costPerMinute;
    
    [SerializeField] private FloatVariable money;

    [SerializeField] private GameEvent cantAffordEvent;
    [SerializeField] private GameEvent dismountScooterEvent;
    
    private void OnEnable()
    {
        
        if (money.Value - pickUpCost.Value < 0)
        {
            cantAffordEvent.Raise();
            return;
        }
        
        
        _startTime = currentTime.Time24H;

        money.Value -= pickUpCost.Value;
        timePassedEvent.RegisterListener(OnEventRaised);
    }

    private void OnDisable()
    {
        timePassedEvent.UnregisterListener(OnEventRaised);
    }

    public void OnEventRaised()
    {
        if (money.Value - pickUpCost.Value < 0)
        {
            cantAffordEvent.Raise();
            dismountScooterEvent.Raise();
            return;
        }
        
        money.Value -= costPerMinute.Value;
    }
}
