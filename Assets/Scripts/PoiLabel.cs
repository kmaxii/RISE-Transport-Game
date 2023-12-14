using Mapbox.Examples.Scripts;
using minimap;
using UnityEngine;

public class PoiLabel : PoiLabelTextSetter
{

    [SerializeField] private Interactable3dPoi interactable3dPoi;


    public void SetType(PoiType poiType)
    {
        interactable3dPoi.PoiType = poiType;
    }
}