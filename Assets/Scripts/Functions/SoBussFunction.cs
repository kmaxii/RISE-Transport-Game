using System;
using Functions;
using MaxisGeneralPurpose.Scriptable_objects;
using UnityEngine;

[CreateAssetMenu(menuName = "Rise/Functions/BussFunction")]
public class SoBussFunction : SoStatsChangeFunction
{

    [SerializeField] private IntVariable numberOfSwitches;
    
    [SerializeField] private float comfortChange;
    [SerializeField] private float stressChange;
    public override float ExecuteFunction()
    {
        return ExecuteFunction(comfortChange, stressChange, numberOfSwitches.Value);
    }


    /// <summary>
    /// Calculates the total comfort/stress change.
    /// </summary>
    /// <param name="a">The comfort/stress change of an event.</param>
    /// <param name="b">The comfort/stress change of a byte.</param>
    /// <param name="n">The number of trips.</param>
    /// <returns>The total comfort/stress change.</returns>
    /// <remarks>
    /// The formula used for the calculation is: y = nA + (n-1)B
    /// </remarks>
    private static float ExecuteFunction(float a, float b, int n)
    {
        return n * a + (n - 1) * b;
    }
}