using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Missions
{
    public class UiMissionShowcase : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        private DayMission _showingMission;

        [FormerlySerializedAs("_image")] [SerializeField] private Image image;

        private string _missionString;

        private Vector3[] _missionLocations;

        private bool _hasInitialized;
        private void InitializeLocations(AbstractMap map)
        {
            _missionLocations = new Vector3[_showingMission.Mission.MissionLocations.Length];
         
            for (var i = 0; i < _showingMission.Mission.MissionLocations.Length; i++)
            {
                Vector2d loc = Conversions.StringToLatLon(_showingMission.Mission.MissionLocations[i].LocationString);
                Vector3 pos = map.GeoToWorldPosition(loc);
                _missionLocations[i] = pos;
            }
            
        }

        public void Show(DayMission dayMission, AbstractMap map)
        {
            _showingMission = dayMission;
            _missionString = dayMission.ToString();
            text.text = dayMission.ToString();
            image.sprite = dayMission.Mission.Sprite;
        }

        public void SetDistance(Vector3 playerPosition, AbstractMap map)
        {
            if (!_hasInitialized)
            {
                InitializeLocations(map);
                _hasInitialized = true;
            }
            
            float closestDistance = float.MaxValue;
            //Get the mission from the _showingMission.Mission.MissionLocations array that is closest to the player
            foreach (var loc in _missionLocations)
            {
                float distance = Vector3.Distance(playerPosition, loc);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                }
            }
            
            //Remove decimal points
            closestDistance = Mathf.Round(closestDistance);
            //If it is above 1000m, show it in km
            if (closestDistance > 1000)
            {
                closestDistance /= 1000;
                
                //Set it to be two decimal points
                closestDistance = Mathf.Round(closestDistance * 100) / 100;
                text.text = _missionString + " " + closestDistance + "km";
                return;
            }
            
            text.text = _missionString + " " + closestDistance + "m";
        }
    }
}