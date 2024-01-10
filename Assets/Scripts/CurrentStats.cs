using MaxisGeneralPurpose.Scriptable_objects;
using Scriptable_objects;
using UnityEngine;

[CreateAssetMenu(fileName = "CurrentStats", menuName = "Rise/CurrentStats")]
public class CurrentStats : ScriptableObject
{
    [SerializeField] private TimeVariable timeVariable;
    public  TimeVariable TimeVariable => timeVariable;

    [SerializeField] private FloatVariable money;
    public  FloatVariable Money => money;
    [SerializeField] private IntVariable stress;
    public  IntVariable Stress => stress;
    [SerializeField] private IntVariable comfort;
    public  IntVariable Comfort => comfort;

}