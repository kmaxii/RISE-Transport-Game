using UnityEngine;

namespace vasttrafik
{
    public class VasttrafikTest : MonoBehaviour
    {
        private async void Start()
        {
            JourneyResult journeyResult = await VasttrafikAPI.GetJourneyJson();

            Debug.Log("Size: " + journeyResult.results.Count);

            foreach (var journeyResultResult in journeyResult.results)
            {
                var departureTime = journeyResultResult.tripLegs[0].origin.plannedTime;
                var arrivalTime = journeyResultResult.tripLegs[^1].destination.plannedTime;
                Debug.Log($"departure {departureTime} arrival: {arrivalTime} estimated: {journeyResultResult.departureAccessLink}");
            }
        }
    }
}