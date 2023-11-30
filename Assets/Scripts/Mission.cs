using Editor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mission", menuName = "Rise/Mission")]
public class Mission : ScriptableObject
{
    [SerializeField] private string missionName = "exampleName";

    [Tooltip("Does task appear at the start of the day? If not it will appear randomly")] [SerializeField]
    private bool isFixed;

    [SerializeField] private Time24H timeItTakes = new Time24H(1, 0);

    [Tooltip("If true, needs to be performed before set time. If false, can be performed any time")] [SerializeField]
    private bool isSetTime;

    [Tooltip("At what time the task starts being available")] [SerializeField]
    private Time24H earliestTime; // Visibility managed in custom editor

    [Tooltip("When is the task considered failed?")] [SerializeField]
    private Time24H latestTime = new Time24H(23, 59); // Visibility managed in custom editor

    [Tooltip("Does doing this task create other tasks")] [SerializeField]
    private bool hasChainedTask;

    [SerializeField] private Mission childMission; // Visibility managed in custom editor

    [Tooltip("If the task is dependent on another task")] [SerializeField]
    private bool isChainedTask;

    [SerializeField] private Mission parentMission; // Visibility managed in custom editor

    [Header("Successful")] [Tooltip("The money amount change after mission. Use - to make it go down")] [SerializeField]
    private int moneyReward;

    [Tooltip("The stress amount change after mission. Use - to make it go down")] [SerializeField]
    private int stressChange;

    [Tooltip("The comfort amount change after mission. Use - to make it go down")] [SerializeField]
    private int comfortChange;

    [Tooltip("The energy amount change after mission. Use - to make it go down")] [SerializeField]
    private int energyChange;

    [Header("Failure")] [Tooltip("The money change on failure. Use - to make it go down")] [SerializeField]
    private int moneyPunishment;

    [Tooltip("The stress change on failure. Use - to make it go down")] [SerializeField]
    private int stressPunishment;

    [Tooltip("The comfort change on failure. Use - to make it go down")] [SerializeField]
    private int comfortPunishment;

    private void OnValidate()
    {
        if (moneyPunishment > 0)
        {
            moneyPunishment = 0;
            Debug.LogWarning("Money punishment should be positive");
        }
    }

    public override string ToString()
    {
        //Return a string with mission name and time it has to be done if there is one
        if (isFixed)
        {
            return missionName + " at " + timeItTakes;
        }

        return missionName;
    }
}