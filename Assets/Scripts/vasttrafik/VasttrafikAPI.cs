using System;
using System.IO;
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
        private const string BaseUrl = "https://ext-api.vasttrafik.se/pr/v4/journeys";
        
        [ItemCanBeNull]
        public static async Task<JourneyDetails> GetJourneyDetailsJson(string detailsReference)
        {
            try
            {
                Client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", await VasttrafikAccessToken.GetAccessTokenAsync());
                string url =
                    $"{BaseUrl}/{detailsReference}/details?includes=triplegcoordinates&includes=servicejourneycoordinates";

                Debug.Log("Url: " + url);

                HttpResponseMessage response = await Client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    JourneyDetails journeyResult = JsonUtility.FromJson<JourneyDetails>(result);
                    

                    
                 try
                 {
                     await File.WriteAllTextAsync("C:\\Users\\1\\Downloads\\filePath.txt", result);
                     Debug.Log("File saved successfully.");
                 }
                 catch (Exception ex)
                 {
                     Debug.Log("Error saving file: " + ex.Message);
                 }
                 
                    
                    return journeyResult;
                }

                Debug.Log($"Error: {response.StatusCode}");
                return null;
            }
            catch (Exception ex)
            {
                Debug.Log($"Exception occurred: {ex.Message}");
                return null;
            }
        }

        [ItemCanBeNull]
        public static async Task<JourneyResult> GetJourneyJson(string originGid,
            string destinationGid, int resultLimit = 1, string time = null)
        {
            Debug.Log($"Sending request with {originGid} to {destinationGid}");
            try
            {
                Client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", await VasttrafikAccessToken.GetAccessTokenAsync());
                string url =
                    $"{BaseUrl}?originGid={originGid}" +
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


                    /*
                    try
                    {
                        await File.WriteAllTextAsync("C:\\Users\\1\\Downloads\\filePath.txt", result);
                        Debug.Log("File saved successfully.");
                    }
                    catch (Exception ex)
                    {
                        Debug.Log("Error saving file: " + ex.Message);
                    }
                    */


                    return journeyResult;
                }

                Debug.Log($"Error: {response.StatusCode}");
                return null;
            }
            catch (Exception ex)
            {
                Debug.Log($"Exception occurred: {ex.Message}");
                return null;
            }
        }
    }
}