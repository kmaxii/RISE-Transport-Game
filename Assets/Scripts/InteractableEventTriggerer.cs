using MaxisGeneralPurpose.Scriptable_objects;
using Scriptable_objects;
using UnityEngine;
using UnityEngine.Serialization;

public class InteractableEventTriggerer : MapInteractable
{

    [FormerlySerializedAs("mountScooterEvent")] [SerializeField] private GameEvent eventToTrigger;
    public override void Interact()
    {
        eventToTrigger.Raise();
    }

    public override string GetName()
    {
        return transform.name;
    }
}