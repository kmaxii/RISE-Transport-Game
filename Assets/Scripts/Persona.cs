using MaxisGeneralPurpose.Scriptable_objects;
using UnityEngine;

[CreateAssetMenu(fileName = "New-Persona", menuName = "Rise/Persona")]
public class Persona : ScriptableObject
{
    [Header("Stats Thresholds")]
    [Range(0, 100)] [SerializeField] private float sweatThreshold = 50;
    [SerializeField] private FloatVariable sweatThresholdVariable;
    [Header("Wetness Thresholds")]
    [Range(0, 100)] [SerializeField] private float wetnessThreshold = 50;
    [SerializeField] private FloatVariable wetnessThresholdVariable;
    [Header("Tiredness Thresholds")]
    [Range(0, 100)] [SerializeField] private float tirednessThreshold = 50;
    [SerializeField] private FloatVariable tirednessThresholdVariable;
    [Header("Time Importance")]
    [Range(1, 5)] [SerializeField] private float timeImportance = 0;
    [SerializeField] private FloatVariable timeImportanceVariable;
    [Header("Stress Importance")]
    [Range(1, 5)] [SerializeField] private float stressImportance = 0;
    [SerializeField] private FloatVariable stressImportanceVariable;
    public FloatVariable StressImportanceVariable => stressImportanceVariable;
    [Header("Comfort Importance")]
    [Range(1, 5)] [SerializeField] private float comfortImportance = 0;
    [SerializeField] private FloatVariable comfortImportanceVariable;
    public FloatVariable ComfortImportanceVariable => comfortImportanceVariable;
    [Header("Bus Comfortability")]
    [Range(-1, 5)] [SerializeField] private float busComfortability = 0;
    [SerializeField] private FloatVariable busComfortabilityVariable;
    [Header("E-Scooter Comfortability")]
    [Range(-1, 5)] [SerializeField] private float eScooterComfortability = 0;
    [SerializeField] private FloatVariable eScooterComfortabilityVariable;
    [Header("Fitness")]
    [Range(0, 100)] [SerializeField] private float fitness = 50;
    [SerializeField] private FloatVariable fitnessVariable;


    private void OnValidate()
    {
       SetFloatVariables();
    }

    private void OnEnable()
    {
        SetFloatVariables();
    }

    private void SetFloatVariables()
    {
        sweatThresholdVariable.Value = sweatThreshold;
        wetnessThresholdVariable.Value = wetnessThreshold;
        tirednessThresholdVariable.Value = tirednessThreshold;
        timeImportanceVariable.Value = timeImportance;
        stressImportanceVariable.Value = stressImportance;
        comfortImportanceVariable.Value = comfortImportance;
        busComfortabilityVariable.Value = busComfortability;
        eScooterComfortabilityVariable.Value = eScooterComfortability;
        fitnessVariable.Value = fitness;
    }
}