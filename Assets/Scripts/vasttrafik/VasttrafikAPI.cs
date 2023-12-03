using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace vasttrafik
{
    public static class VasttrafikAPI
    {
        private static readonly HttpClient Client = new HttpClient();
        private const string BaseUrl = "https://ext-api.vasttrafik.se/pr/v4";
        

        [ItemCanBeNull]
        public static async Task<JourneyResult> GetJourneyJson(string originGid,
            string destinationGid, int resultLimit = 1, string time = null)
        {
        
            Debug.Log($"Sending request with {originGid} to {destinationGid}");
            try
            {
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await VasttrafikAccessToken.GetAccessTokenAsync());
                string url =
                    $"{BaseUrl}/journeys?originGid={originGid}" +
                    $"&destinationGid={destinationGid}" +
                    $"&limit={resultLimit}" +
                    $"&onlyDirectConnections=false" +
                    $"&includeNearbyStopAreas=true";

                
                if (time != null)
                    url += $"&dateTime={Uri.EscapeDataString(time)}";
                
                
                HttpResponseMessage response = await Client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
          
                    string result = await response.Content.ReadAsStringAsync();
                    JourneyResult journeyResult = JsonUtility.FromJson<JourneyResult>(result);
                
                
                    /*Debug.Log(journeyResult.results.Count);
                try
                {
                    await File.WriteAllTextAsync("C:\\Users\\1\\Downloads\\filePath.txt", journeyResult.ToString());
                    Debug.Log("File saved successfully.");
                }
                catch (System.Exception ex)
                {
                    Debug.Log("Error saving file: " + ex.Message);
                }
                
                Debug.Log("Journey: " + journeyResult.ToString());*/
                
                    return journeyResult;
                }
                else
                {
                    Debug.Log($"Error: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.Log($"Exception occurred: {ex.Message}");
                return null;
            }
        }
    }
}