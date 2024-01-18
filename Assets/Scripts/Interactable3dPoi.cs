using minimap;
using UnityEngine;
using UnityEngine.UI;

public class Interactable3dPoi : MapInteractable
{

    [SerializeField] private Text stationName;
    [SerializeField] private PoiLabel poiLabel;

    private PoiType _poiType;
    
    public PoiType PoiType
    {
        get => _poiType;
        set
        {
            Debug.Log("Setting poi type to: " + value);
            _poiType = value;
        }
    }

    public StopPoint StopPoint { get; set; }

    public override void Interact()
    {
        Debug.Log("The type is: " + poiLabel.poiType + " and the name is: " + stationName.text);
        switch (poiLabel.poiType)
        {
            case PoiType.BussStation:
                FullScreenMap.Instance.InteractingInteractable3dPoi = this;
                break;
            case PoiType.Mission:
                DayHandler.Instance.FinishMission(GetName());
                break;
        }
    }

    public override string GetName()
    {
        return stationName.text;
    }
}
