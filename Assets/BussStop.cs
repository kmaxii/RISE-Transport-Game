using UnityEngine;
using UnityEngine.UI;

public class BussStop : MapInteractable
{

    [SerializeField] private Text stationName;

    
    public override void Interact()
    {
        throw new System.NotImplementedException();
    }

    public override string GetName()
    {
        return stationName.text;
    }
}