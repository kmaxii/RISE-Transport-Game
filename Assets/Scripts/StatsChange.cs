using System;
using Functions;
using MaxisGeneralPurpose.Scriptable_objects;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class StatsChange
{
    [SerializeField] private GameEvent onEvent;

    [FormerlySerializedAs("toChange")] [SerializeField] private FloatVariable change;
    
    [FormerlySerializedAs("statsChangeFunction")] [SerializeField] private SoStatsChangeFunction with;

    [SerializeField] private BoolVariable ifThis;

    [SerializeField] private bool isThis;
    
    public void RegisterListener()
    {
        onEvent.RegisterListener(ApplyChange);
    }

    public void UnregisterListener()
    {
        onEvent.UnregisterListener(ApplyChange);
    }

    public void ApplyChange()
    {
        if (!ifThis || ifThis.Value == isThis)
            change.Value += with.ExecuteFunction();
    }
    
}