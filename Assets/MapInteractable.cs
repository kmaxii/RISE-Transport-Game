using System;
using UnityEngine;

public abstract class MapInteractable : MonoBehaviour
{
    public abstract void Interact();

    public abstract String GetName();
}