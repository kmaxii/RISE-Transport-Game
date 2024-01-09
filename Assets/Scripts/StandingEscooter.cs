using Scriptable_objects;
using UnityEngine;

public class StandingEscooter : MapInteractable
{

    [SerializeField] private GameEvent mountScooterEvent;
    public override void Interact()
    {
        mountScooterEvent.Raise();
        Destroy(gameObject);
    }

    public override string GetName()
    {
        return "Escooter";
    }
}