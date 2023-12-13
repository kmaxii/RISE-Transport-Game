using System.Collections;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class LineRendererHandler : MonoBehaviour
{
    [SerializeField] private UILineRenderer travelLinePrefab;
    [SerializeField] private List<UILineRenderer> travelLinesList;

    public void AddLines(List<Vector2> coordsList, Color color) {
        // given a set of coordinates, this method should take the coordinates and put them into a array caller bussLines.Points.
        UILineRenderer newTravelLine = travelLinePrefab;
        
        newTravelLine.Points = new Vector2[coordsList.Count];
        for (int i = 0; i < coordsList.Count; i++) {
            newTravelLine.Points[i] = coordsList[i];
        }
        newTravelLine.color = color;
        
        travelLinesList.Add(Instantiate(newTravelLine, transform)); 
    }


    public void ClearLines() {
        for (int i = travelLinesList.Count - 1; i >= 0; i--) {
            travelLinesList[i].Destroy();
            travelLinesList.RemoveAt(i);
        }
    }

    
}
