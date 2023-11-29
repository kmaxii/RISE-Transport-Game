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
        "eyJ4NXQiOiJaV05sTURNNE56SmpZelZrT1dFNU16RTFNalF5TTJaaE5XSm1ORE0zWkRVMk9HRXdOVGxqWVRjNE1tWTNPRGcwWW1JeFlqSTFPVGMzTjJWallqZzRNdyIsImtpZCI6IlpXTmxNRE00TnpKall6VmtPV0U1TXpFMU1qUXlNMlpoTldKbU5ETTNaRFUyT0dFd05UbGpZVGM0TW1ZM09EZzBZbUl4WWpJMU9UYzNOMlZqWWpnNE13X1JTMjU2IiwiYWxnIjoiUlMyNTYifQ.eyJzdWIiOiJKN0cyaThtN0pRM01ad1pQQWRVd3dRZl93TVVhIiwiYXV0IjoiQVBQTElDQVRJT04iLCJiaW5kaW5nX3R5cGUiOiJyZXF1ZXN0IiwiaXNzIjoiaHR0cHM6XC9cL2V4dC1hcGkudmFzdHRyYWZpay5zZVwvdG9rZW4iLCJ0aWVySW5mbyI6eyJVbmxpbWl0ZWQiOnsidGllclF1b3RhVHlwZSI6InJlcXVlc3RDb3VudCIsImdyYXBoUUxNYXhDb21wbGV4aXR5IjowLCJncmFwaFFMTWF4RGVwdGgiOjAsInN0b3BPblF1b3RhUmVhY2giOnRydWUsInNwaWtlQXJyZXN0TGltaXQiOjAsInNwaWtlQXJyZXN0VW5pdCI6bnVsbH19LCJrZXl0eXBlIjoiUFJPRFVDVElPTiIsInN1YnNjcmliZWRBUElzIjpbeyJzdWJzY3JpYmVyVGVuYW50RG9tYWluIjoiY2FyYm9uLnN1cGVyIiwibmFtZSI6ImFwaTAwMTMtcHIiLCJjb250ZXh0IjoiXC9wclwvdjQiLCJwdWJsaXNoZXIiOiJhZG1pbiIsInZlcnNpb24iOiJ2NCIsInN1YnNjcmlwdGlvblRpZXIiOiJVbmxpbWl0ZWQifSx7InN1YnNjcmliZXJUZW5hbnREb21haW4iOiJjYXJib24uc3VwZXIiLCJuYW1lIjoiYXBpMDA1OS1nZW8iLCJjb250ZXh0IjoiXC9nZW9cL3YyIiwicHVibGlzaGVyIjoiYWRtaW4iLCJ2ZXJzaW9uIjoidjIiLCJzdWJzY3JpcHRpb25UaWVyIjoiVW5saW1pdGVkIn1dLCJhdWQiOiJodHRwczpcL1wvZXh0LWFwaS52YXN0dHJhZmlrLnNlIiwibmJmIjoxNzAxMTg0ODExLCJhcHBsaWNhdGlvbiI6eyJvd25lciI6Iko3RzJpOG03SlEzTVp3WlBBZFV3d1FmX3dNVWEiLCJ0aWVyUXVvdGFUeXBlIjpudWxsLCJ0aWVyIjoiVW5saW1pdGVkIiwibmFtZSI6IlJpc2VUcmFuc3BvcnRHYW1lIiwiaWQiOjE3MjEsInV1aWQiOiIzOWU1MDM3My0xODE2LTQ3NjMtODdkMC0zOTFhNzRkZWY3MzEifSwiYXpwIjoiSjdHMmk4bTdKUTNNWndaUEFkVXd3UWZfd01VYSIsInNjb3BlIjoiYXBpbTpzdWJzY3JpYmUiLCJleHAiOjE3MDEyNzEyMTEsImlhdCI6MTcwMTE4NDgxMSwiYmluZGluZ19yZWYiOiJiMWJhOGM4NzA1N2E0ZDc0YmRjYWY2ZGI1ZDY1MWZlOCIsImp0aSI6IjYzMmFmY2M4LTk1MmEtNDU3OC05MjgzLTI0YzAyNjM3OGVhZiJ9.esU-cxlPWpqA2yf4nld87lalops9NLzws0xeGBVL0aYqr4czQey1wCLcP8uggoP5QLOMUupQcwqetxwpW0Y-DlOY2c-JNyoDisoG-O8BdBsbxjAAy38pl-bGrsdzjl6Kf9cco0bq1lF_GKaQ10rXH39Cgz19HrrABZoP_NQCGdJkrUUztdXx-JA6D_0t_KCTG1bOpAKePeWgqjUGQAdWwGXl2qG983Io_htJP-eJ4NmvEsxGVibib6Jr5KswBvHnNjgCGR60MQ-er7y8Z5iDFGRpBoWgvAGdshFZn-W5H0Ux2gNesURx5CkLRCqrv-YyXlY0dVDtJP9EvOD2-0vHZA";



    [ItemCanBeNull]
    public static async Task<JourneyResult> GetJourneyJson(string originGid,
        string destinationGid, int resultLimit = 1)
    {
        
        Debug.Log($"Sending request with {originGid} to {destinationGid}");
        try
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
            string url =
                $"{BaseUrl}/journeys?originGid={originGid}&destinationGid={destinationGid}&limit={resultLimit}";

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