using Mapbox.Unity.Map;
using MaxisGeneralPurpose.Scriptable_objects;
using Missions;
using UnityEngine;

namespace DataCollection
{
    public class DataSaver : MonoBehaviour
    {
        [SerializeField] private GameEvent timePassedEvent;

        [SerializeField] private GameEventWithData missionFinished;
        [SerializeField] private GameEventWithData missionFailed;
        private string _missionChangeText = "";

        [SerializeField] private TimeVariable time;

        [SerializeField] private Transform player;

        [SerializeField] private AbstractMap map;

        [SerializeField] private FloatVariable money;

        [SerializeField] private BoolVariable isOnScooter;

        [SerializeField] private DayHandler dayHandler;

        [SerializeField] private StringVariable lastBusInfo;

        [SerializeField] private BoolVariable isTimeWarping;

        private DataManager _dataManager;
        
        private string lastActivities = "";

        private void Awake()
        {
            _dataManager = new DataManager();
            _dataManager.DeleteDataFile();
        }

        private void OnEnable()
        {
            timePassedEvent.RegisterListener(SaveData);
            missionFinished.RegisterListener(OnFinishedMission);
            missionFailed.RegisterListener(OnFailedMission);
        }

        private void OnDisable()
        {
            timePassedEvent.UnregisterListener(SaveData);
            missionFinished.UnregisterListener(OnFinishedMission);
            missionFailed.UnregisterListener(OnFailedMission);
        }

        private void OnFinishedMission(object data)
        {
            DayMission dayMission = (DayMission) data;

            if (_missionChangeText != "")
                _missionChangeText += ", ";

            _missionChangeText += "Finished: " + dayMission.Mission.MissionName;
        }

        private void OnFailedMission(object data)
        {
            DayMission dayMission = (DayMission) data;

            if (_missionChangeText != "")
                _missionChangeText += ", ";

            _missionChangeText += "Failed: " + dayMission.Mission.MissionName;
        }

        private void SaveData()
        {
            if (isTimeWarping.Value)
            {
                return;
            }

            string dayHandlerActivities = dayHandler.ToString();
            string activities = lastActivities == dayHandlerActivities ? "" : dayHandlerActivities;
            lastActivities = dayHandlerActivities;

            var latLong = map.WorldToGeoPosition(player.position);
            UserData data = new UserData(time.Time24H.ToString(),
                latLong.y, 
                latLong.x,
                lastBusInfo.Value,
                _missionChangeText,
                money.Value,
                isOnScooter.Value,
                activities);

            lastBusInfo.Value = "";
            _missionChangeText = "";
            _dataManager.AddUserData(data);
        }

        private void OnApplicationQuit()
        {
            _dataManager.SaveDataToFile();
        }
    }
}