using Scriptable_objects;
using UnityEngine;

[CreateAssetMenu(fileName = "CurrentStats", menuName = "Rise/CurrentStats")]
public class CurrentStats : ScriptableObject
{
    [SerializeField] private TimeVariable timeVariable;
    public  TimeVariable TimeVariable => timeVariable;

    [SerializeField] private FloatVariable money;
    public  FloatVariable Money => money;
    [SerializeField] private FloatVariable stress;
    public  FloatVariable Stress => stress;
    [SerializeField] private FloatVariable comfort;
    public  FloatVariable Comfort => comfort;

}