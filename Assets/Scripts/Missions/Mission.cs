using UnityEngine;

namespace Missions
{
    [CreateAssetMenu(fileName = "New_Mission", menuName = "Rise/Mission")]
    public class Mission : ScriptableObject
    {
        [SerializeField] private string missionName = "exampleName";
        public string MissionName => missionName;

        [Tooltip("The picture of how this mission")]
        [SerializeField] private Sprite sprite;
        public Sprite Sprite => sprite;

        [SerializeField] private Color color;
        public Color Color => color;
    
    
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


        [Header("Time")]
        [SerializeField] private Time24H timeItTakes = new Time24H(1, 0);
        
        public Time24H TimeItTakes => timeItTakes;
        
        
        [Header("Successful")] [Tooltip("The money amount change after mission. Use - to make it go down")] [SerializeField]
        private int moneyReward;
        public  int MoneyReward => moneyReward;

        [Tooltip("The stress amount change after mission. Use - to make it go down")] [SerializeField]
        private int stressChange;
        public  int StressChange => stressChange;

        [Tooltip("The comfort amount change after mission. Use - to make it go down")] [SerializeField]
        private int comfortChange;
        public  int ComfortChange => comfortChange;

        [Header("Failure")] [Tooltip("The money change on failure. Use - to make it go down")] [SerializeField]
        private int moneyPunishment;
        public  int MoneyPunishment => moneyPunishment;

        [Tooltip("The stress change on failure. Use - to make it go down")] [SerializeField]
        private int stressPunishment;
        public  int StressPunishment => stressPunishment;

        [Tooltip("The comfort change on failure. Use - to make it go down")] [SerializeField]
        private int comfortPunishment;
        public  int ComfortPunishment => comfortPunishment;


        public void FinishMission(CurrentStats currentStats)
        {
            currentStats.TimeVariable.Time24H += timeItTakes;
            currentStats.Money.Value += moneyReward;
            currentStats.Stress.Value += stressChange;
            currentStats.Comfort.Value += comfortChange;
        }


        public void FailMission(CurrentStats currentStats)
        {
            currentStats.Money.Value += moneyPunishment;
            currentStats.Stress.Value += stressPunishment;
            currentStats.Comfort.Value += comfortPunishment;
        }
        
        
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
            return missionName;
        }
    }
}