using System.Collections;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class LineRendererHandler : MonoBehaviour {
    [SerializeField] private UILineRenderer travelLinePrefab;
    [SerializeField] private List<UILineRenderer> travelLinesList;

    private int busCount = 0;
    private int tramCount = 0;

    [Tooltip("Tram, Bus, Walk, notOnTram, notOnBus, Default")] [SerializeField]
    private List<Color> travelColors;

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

    public void AddLines(List<Vector2> coordsList, string travelType) {
        // given a set of coordinates, this method should take the coordinates and put them into a array caller bussLines.Points.
        UILineRenderer newTravelLine = travelLinePrefab;
        

        switch (travelType) {
            case "tram":
                newTravelLine.color = tramCount % 2 == 0 ? travelColors[0] : travelColors[1];
                tramCount++;
                break;
            case "bus":
                newTravelLine.color = busCount % 2 == 0 ? travelColors[2] : travelColors[3];
                busCount++;
                break;
            case "walk":
                newTravelLine.color = travelColors[4];
                break;
            case "notontram":
                newTravelLine.color = travelColors[5];
                break;
            case "notonbus":
                newTravelLine.color = travelColors[6];
                break;
            default:
                newTravelLine.color = travelColors[7];
                break;
        }

        newTravelLine.Points = new Vector2[coordsList.Count];
        for (int i = 0; i < coordsList.Count; i++) {
            newTravelLine.Points[i] = coordsList[i];
        }

        travelLinesList.Add(Instantiate(newTravelLine, transform));
    }


    public void ClearLines() {
        for (int i = travelLinesList.Count - 1; i >= 0; i--) {
            travelLinesList[i].Destroy();
            travelLinesList.RemoveAt(i);
        }
        busCount = 0;
        tramCount = 0;
    }
}