using System;
using UnityEngine;

namespace Missions
{
    [CreateAssetMenu(fileName = "New MissionLocation", menuName = "Rise/Mission/MissionLocation")]
    public class MissionLocation : ScriptableObject
    {
    
        [Tooltip("Location name in lat long. Example: 57.70924687703637, 11.979398501480077. \nCopy paste from google maps by right clicking")]
        [SerializeField] private string location;

        public Vector2 Location => new Vector2(Convert.ToSingle(location.Split(',')[0]), Convert.ToSingle(location.Split(',')[1]));
        public string LocationString => location;
    }
}