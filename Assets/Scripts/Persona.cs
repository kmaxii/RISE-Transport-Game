using UnityEngine;

[CreateAssetMenu(fileName = "New-Persona", menuName = "Rise/Persona")]
public class Persona : ScriptableObject
{
    [Range(0, 100)] [SerializeField] private int sweatThreshold = 50;
    [Range(0, 100)] [SerializeField] private int wetnessThreshold = 50;
    [Range(0, 100)] [SerializeField] private int tirednessThreshold = 50;
    [Range(0, 100)] [SerializeField] private int timeImportance = 50;
    [Range(0, 100)] [SerializeField] private int stressImportance = 50;
    [Range(0, 100)] [SerializeField] private int comfortImportance = 50;
    [Range(0, 100)] [SerializeField] private int busComfortability = 50;
    [Range(0, 100)] [SerializeField] private int tramComfortability = 50;
    [Range(0, 100)] [SerializeField] private int eScooterComfortability = 50;
    [Range(0, 100)] [SerializeField] private int fitness = 50;
    
}