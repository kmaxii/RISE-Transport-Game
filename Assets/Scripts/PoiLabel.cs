using Mapbox.Examples.Scripts;
using minimap;
using UnityEngine;

public class PoiLabel : PoiLabelTextSetter
{

    [SerializeField] private Interactable3dPoi interactable3dPoi;

    public PoiType poiType;

    public void SetType(PoiType toSet)
    {
        poiType = toSet;
    }
}