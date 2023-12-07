using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New MissionLocation", menuName = "Rise/MissionLocation")]
public class MissionLocation : ScriptableObject
{
    private Vector2 _loc;
    
    [Tooltip("Location name in lat long. Example: 57.70924687703637, 11.979398501480077. \nCopy paste from google maps by right clicking")]
    [SerializeField] private string location;

    public Vector2 Location => _loc;
    public string LocationString => location;
    
    private void Awake()
    {
        _loc = new Vector2(Convert.ToSingle(location.Split(',')[0]), Convert.ToSingle(location.Split(',')[1]));
    }
}