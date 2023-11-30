using Editor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mission", menuName = "Rise/Mission")]
public class Mission : ScriptableObject
{
    [SerializeField] private string missionName = "exampleName";

    [Tooltip("The picture of how this mission")]
    [SerializeField] private Sprite sprite;
    
    [Header("Appearance")]
    [SerializeField] private MissionLocation[] missionLocations;
    
    [Tooltip("If false, a random location is chosen. If true, can be done at all")]
    [SerializeField] private bool canBeDoneAtAllLocation; // Visibility managed in custom editor

    public  MissionLocation[] MissionLocations
    {
        get
        {
            if (missionLocations.Length <= 1 || canBeDoneAtAllLocation)
                return missionLocations;
            
            //Return an array with one element that is a random location from missionLocations
            MissionLocation[] newMissionLocations = new MissionLocation[1];
            newMissionLocations[0] = missionLocations[Random.Range(0, missionLocations.Length)];
            return newMissionLocations;
        }
    }

    [Tooltip("Does task appear at the start of the day? If not it will appear randomly")] [SerializeField]
    private bool isFixed;

    [Header("Time")]
    
    [SerializeField] private Time24H timeItTakes = new Time24H(1, 0);

    [Tooltip("If true, needs to be performed before set time. If false, can be performed any time")] [SerializeField]
    private bool isSetTime;

    [Tooltip("At what time the task starts being available")] [SerializeField]
    private Time24H earliestTime; // Visibility managed in custom editor

    [Tooltip("When is the task considered failed?")] [SerializeField]
    private Time24H latestTime = new Time24H(23, 59); // Visibility managed in custom editor

    [Header("Chained Task")]
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