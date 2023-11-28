using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using vasttrafik;

public class VasttrafikAPI
{
     private static readonly HttpClient Client = new HttpClient();
    private const string BaseUrl = "https://ext-api.vasttrafik.se/pr/v4";

    private const string AccessToken =
        "eyJ4NXQiOiJaV05sTURNNE56SmpZelZrT1dFNU16RTFNalF5TTJaaE5XSm1ORE0zWkRVMk9HRXdOVGxqWVRjNE1tWTNPRGcwWW1JeFlqSTFPVGMzTjJWallqZzRNdyIsImtpZCI6IlpXTmxNRE00TnpKall6VmtPV0U1TXpFMU1qUXlNMlpoTldKbU5ETTNaRFUyT0dFd05UbGpZVGM0TW1ZM09EZzBZbUl4WWpJMU9UYzNOMlZqWWpnNE13X1JTMjU2IiwiYWxnIjoiUlMyNTYifQ.eyJzdWIiOiJKN0cyaThtN0pRM01ad1pQQWRVd3dRZl93TVVhIiwiYXV0IjoiQVBQTElDQVRJT04iLCJiaW5kaW5nX3R5cGUiOiJyZXF1ZXN0IiwiaXNzIjoiaHR0cHM6XC9cL2V4dC1hcGkudmFzdHRyYWZpay5zZVwvdG9rZW4iLCJ0aWVySW5mbyI6eyJVbmxpbWl0ZWQiOnsidGllclF1b3RhVHlwZSI6InJlcXVlc3RDb3VudCIsImdyYXBoUUxNYXhDb21wbGV4aXR5IjowLCJncmFwaFFMTWF4RGVwdGgiOjAsInN0b3BPblF1b3RhUmVhY2giOnRydWUsInNwaWtlQXJyZXN0TGltaXQiOjAsInNwaWtlQXJyZXN0VW5pdCI6bnVsbH19LCJrZXl0eXBlIjoiUFJPRFVDVElPTiIsInN1YnNjcmliZWRBUElzIjpbeyJzdWJzY3JpYmVyVGVuYW50RG9tYWluIjoiY2FyYm9uLnN1cGVyIiwibmFtZSI6ImFwaTAwMTMtcHIiLCJjb250ZXh0IjoiXC9wclwvdjQiLCJwdWJsaXNoZXIiOiJhZG1pbiIsInZlcnNpb24iOiJ2NCIsInN1YnNjcmlwdGlvblRpZXIiOiJVbmxpbWl0ZWQifSx7InN1YnNjcmliZXJUZW5hbnREb21haW4iOiJjYXJib24uc3VwZXIiLCJuYW1lIjoiYXBpMDA1OS1nZW8iLCJjb250ZXh0IjoiXC9nZW9cL3YyIiwicHVibGlzaGVyIjoiYWRtaW4iLCJ2ZXJzaW9uIjoidjIiLCJzdWJzY3JpcHRpb25UaWVyIjoiVW5saW1pdGVkIn1dLCJhdWQiOiJodHRwczpcL1wvZXh0LWFwaS52YXN0dHJhZmlrLnNlIiwibmJmIjoxNzAwODQyMTMyLCJhcHBsaWNhdGlvbiI6eyJvd25lciI6Iko3RzJpOG03SlEzTVp3WlBBZFV3d1FmX3dNVWEiLCJ0aWVyUXVvdGFUeXBlIjpudWxsLCJ0aWVyIjoiVW5saW1pdGVkIiwibmFtZSI6IlJpc2VUcmFuc3BvcnRHYW1lIiwiaWQiOjE3MjEsInV1aWQiOiIzOWU1MDM3My0xODE2LTQ3NjMtODdkMC0zOTFhNzRkZWY3MzEifSwiYXpwIjoiSjdHMmk4bTdKUTNNWndaUEFkVXd3UWZfd01VYSIsInNjb3BlIjoiYXBpbTpzdWJzY3JpYmUiLCJleHAiOjE3MDA5Mjg1MzIsImlhdCI6MTcwMDg0MjEzMiwiYmluZGluZ19yZWYiOiI1YzU5ZmU0NGFlZmNlMDNlZDEwZjg5MzUxZmZjMTc4OCIsImp0aSI6IjJjNDRkYzI2LWFiM2MtNDFlOS1iZjRiLWYyMmNkNDZhN2JhOCJ9.bsnfHMgZnB1bG2TRIMPtYmKElpgDx3rR9CN-W3dVU8PkG1ZsJ7RoUXeYgaLzM-WqV1lcU4NVa4B3JBo1BE2qjDvSCgY9JFpoqrBIDJMTtJswFTaS0qKXuKRrOJ5Fha7P6KRXYZ7t9JPNQ8kXNBmSaGSPJxLbpQYRou4nzxJdOcMqrPfY6hNbvHtd2MU58tZnkB3ES1dSqWAbt85p3cs-IAyMzlp_YvmM34oIijIppz6sPWtqW5xP5LvJBuLk933D1CQ_6MMEaRpz7WFMGXw4GoC1rCBfN9OlXzwRmYL8gKHk2jX_i1SyilHE2xZ43H-dHgCysgT5Y5crxhCnKIqo2g";

    
    
    
    [ItemCanBeNull]
    public static async Task<JourneyResult> GetJourneyJson(string originGid = "9022014001004001",
        string destinationGid = "9022014001950001", int resultLimit = 1)
    {
        try
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
            string url =
                $"{BaseUrl}/journeys?originGid={originGid}&destinationGid={destinationGid}&limit={resultLimit}";

            HttpResponseMessage response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
          
                string result = await response.Content.ReadAsStringAsync();
                Debug.Log("Got result: " +result);
                try
                {
                    await File.WriteAllTextAsync("C:\\Users\\1\\Downloads\\filePath.txt", result);
                    Debug.Log("File saved successfully.");
                }
                catch (System.Exception ex)
                {
                    Debug.Log("Error saving file: " + ex.Message);
                }
                JourneyResult journeyResult = JsonUtility.FromJson<JourneyResult>(result);
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