using System;
using Mapbox.Unity.Map;
using MaxisGeneralPurpose.Scriptable_objects;
using UnityEngine;

namespace DataCollection
{
    public class DataSaver : MonoBehaviour
    {
        [SerializeField] private GameEvent timePassedEvent;

        [SerializeField] private TimeVariable time;

        [SerializeField] private Transform player;

        [SerializeField] private AbstractMap map;

        [SerializeField] private FloatVariable money;

        [SerializeField] private BoolVariable isOnScooter;

        [SerializeField] private DayHandler dayHandler;
        private void OnEnable()
        {
            timePassedEvent.RegisterListener(SaveData);
        }
        
        private void OnDisable()
        {
            timePassedEvent.UnregisterListener(SaveData);
        }
        
        private void SaveData()
        {
            
            var latLong = map.WorldToGeoPosition(player.position);
            UserData data = new UserData(time.Time24H.ToString(), 
                latLong.x, latLong.y, 
                "bus", 
                "activity", 
                money.Value, 
                isOnScooter.Value, 
                dayHandler.ToString());
            DataManager.SaveUserData(data);
        }
    }
}