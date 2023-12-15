using minimap;
using UnityEngine;
using UnityEngine.UI;

public class Interactable3dPoi : MapInteractable
{

    [SerializeField] private Text stationName;

    private PoiType _poiType;
    
    public PoiType PoiType
    {
        get => _poiType;
        set => _poiType = value;
    }

    public StopPoint StopPoint { get; set; }

    public override void Interact()
    {
        switch (_poiType)
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
