using minimap;
using UnityEngine;
using UnityEngine.UI;

public class BussStop : MapInteractable
{

    [SerializeField] private Text stationName;

    public StopPoint StopPoint { get; set; }

    public override void Interact()
    {
        FullScreenMap.Instance.InteractingBussStop = this;
    }

    public override string GetName()
    {
        return stationName.text;
    }
}
