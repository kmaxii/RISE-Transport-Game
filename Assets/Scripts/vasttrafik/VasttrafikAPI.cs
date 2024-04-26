using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace vasttrafik
{
    public static class VasttrafikAPI
    {
        private const string BaseUrl = "https://ext-api.vasttrafik.se/pr/v4/journeys";

        public static IEnumerator GetJourneyDetailsJson(string detailsReference, Action<JourneyDetails> callback)
        {
            string accessToken =  VasttrafikAccessToken.GetAccessToken();
            string url = $"{BaseUrl}/{detailsReference}/details?includes=triplegcoordinates&includes=servicejourneycoordinates";

            Debug.Log("Url: " + url);

            using (UnityWebRequest request = UnityWebRequest.Get(url))
            {
                request.SetRequestHeader("Authorization", $"Bearer {accessToken}");

                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    string result = request.downloadHandler.text;
                    JourneyDetails journeyResult = JsonUtility.FromJson<JourneyDetails>(result);
                    callback(journeyResult);
                }
                else
                {
                    Debug.Log($"Error: {request.responseCode}");
                    callback(null);
                }
            }
        }

        public static IEnumerator GetJourneyJson(string originGid, string destinationGid, int resultLimit, string time, Action<JourneyResult> callback)
        {
            Debug.LogError($"Sending request with {originGid} to {destinationGid}");

            string accessToken = VasttrafikAccessToken.GetAccessToken();
            string url = $"{BaseUrl}?originGid={originGid}" +
                         $"&destinationGid={destinationGid}" +
                         $"&limit={resultLimit}" +
                         $"&originWalk=1,0,500" +
                         $"&destWalk=1,0,500" +
                         $"&onlyDirectConnections=false" +
                         $"&includeNearbyStopAreas=true";

            if (time != null)
            {
                //The time variable is of format: 2024-04-26T07:32:00.000+02:00
                //We need to change this if it right is summer time to +03:00. 
                time = time.Replace("+02:00", "+03:00");
                
                
                url += $"&dateTime={Uri.EscapeDataString(time)}";

            }

            using (UnityWebRequest request = UnityWebRequest.Get(url))
            {
                request.SetRequestHeader("Authorization", $"Bearer {accessToken}");

                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    string result = request.downloadHandler.text;
                    JourneyResult journeyResult = JsonUtility.FromJson<JourneyResult>(result);
                    callback(journeyResult);
                }
                else
                {
                    Debug.LogError($"Error: {request.responseCode}");
                    callback(null);
                }
            }
        }
    }
}