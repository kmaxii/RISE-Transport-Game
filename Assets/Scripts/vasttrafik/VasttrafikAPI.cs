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

        private const string AccessToken =
            "eyJ4NXQiOiJaV05sTURNNE56SmpZelZrT1dFNU16RTFNalF5TTJaaE5XSm1ORE0zWkRVMk9HRXdOVGxqWVRjNE1tWTNPRGcwWW1JeFlqSTFPVGMzTjJWallqZzRNdyIsImtpZCI6IlpXTmxNRE00TnpKall6VmtPV0U1TXpFMU1qUXlNMlpoTldKbU5ETTNaRFUyT0dFd05UbGpZVGM0TW1ZM09EZzBZbUl4WWpJMU9UYzNOMlZqWWpnNE13X1JTMjU2IiwiYWxnIjoiUlMyNTYifQ.eyJzdWIiOiJKN0cyaThtN0pRM01ad1pQQWRVd3dRZl93TVVhIiwiYXV0IjoiQVBQTElDQVRJT04iLCJiaW5kaW5nX3R5cGUiOiJyZXF1ZXN0IiwiaXNzIjoiaHR0cHM6XC9cL2V4dC1hcGkudmFzdHRyYWZpay5zZVwvdG9rZW4iLCJ0aWVySW5mbyI6eyJVbmxpbWl0ZWQiOnsidGllclF1b3RhVHlwZSI6InJlcXVlc3RDb3VudCIsImdyYXBoUUxNYXhDb21wbGV4aXR5IjowLCJncmFwaFFMTWF4RGVwdGgiOjAsInN0b3BPblF1b3RhUmVhY2giOnRydWUsInNwaWtlQXJyZXN0TGltaXQiOjAsInNwaWtlQXJyZXN0VW5pdCI6bnVsbH19LCJrZXl0eXBlIjoiUFJPRFVDVElPTiIsInN1YnNjcmliZWRBUElzIjpbeyJzdWJzY3JpYmVyVGVuYW50RG9tYWluIjoiY2FyYm9uLnN1cGVyIiwibmFtZSI6ImFwaTAwMTMtcHIiLCJjb250ZXh0IjoiXC9wclwvdjQiLCJwdWJsaXNoZXIiOiJhZG1pbiIsInZlcnNpb24iOiJ2NCIsInN1YnNjcmlwdGlvblRpZXIiOiJVbmxpbWl0ZWQifSx7InN1YnNjcmliZXJUZW5hbnREb21haW4iOiJjYXJib24uc3VwZXIiLCJuYW1lIjoiYXBpMDA1OS1nZW8iLCJjb250ZXh0IjoiXC9nZW9cL3YyIiwicHVibGlzaGVyIjoiYWRtaW4iLCJ2ZXJzaW9uIjoidjIiLCJzdWJzY3JpcHRpb25UaWVyIjoiVW5saW1pdGVkIn1dLCJhdWQiOiJodHRwczpcL1wvZXh0LWFwaS52YXN0dHJhZmlrLnNlIiwibmJmIjoxNzAxMzM2NDQxLCJhcHBsaWNhdGlvbiI6eyJvd25lciI6Iko3RzJpOG03SlEzTVp3WlBBZFV3d1FmX3dNVWEiLCJ0aWVyUXVvdGFUeXBlIjpudWxsLCJ0aWVyIjoiVW5saW1pdGVkIiwibmFtZSI6IlJpc2VUcmFuc3BvcnRHYW1lIiwiaWQiOjE3MjEsInV1aWQiOiIzOWU1MDM3My0xODE2LTQ3NjMtODdkMC0zOTFhNzRkZWY3MzEifSwiYXpwIjoiSjdHMmk4bTdKUTNNWndaUEFkVXd3UWZfd01VYSIsInNjb3BlIjoiYXBpbTpzdWJzY3JpYmUiLCJleHAiOjE3MDE0MjI4NDEsImlhdCI6MTcwMTMzNjQ0MSwiYmluZGluZ19yZWYiOiI2Njk4MjIzM2Y0ODNkYzFmNzU5ODA0ODI5M2FmOTEwNCIsImp0aSI6ImVmNTAyYWU4LTRmOTEtNDhjYi04YWUxLTljZGRiOTUzYjk1ZiJ9.Prd-0sBkj_HWkK4Th2dc4ocM_CITPZE-9bl5MX6usddbZuwZGXpWe8GYoSfsbEul6Foc0PbXSR2qHFY8XV2Fv-s1KETx8kr1fXkAc-e8xDT580JKyfTzC1pzuBXvEmVWe47FUBUbTmPJz20sMJFOn9qmgMWhoMvdKakoz2hJ4rJ5WcXjR5jFvZkd8wqZSdQQ6zIN_fWN_U4-oaqfju1xUL9FMyhg7et41iXgRXpa26A9yxdM7r_1m40DJT5L90WV6ueke7mPAlFl1wiqwr9X8Z66dM28PNlZz-rcexCH5RgO2htRqQIQRS6kC4_6IpzhEyOVrWdpvOXeuFCl1dBD5w";


        [ItemCanBeNull]
        public static async Task<JourneyResult> GetJourneyJson(string originGid,
            string destinationGid, int resultLimit = 1, string time = null)
        {
        
            Debug.Log($"Sending request with {originGid} to {destinationGid}");
            try
            {
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
                string url =
                    $"{BaseUrl}/journeys?originGid={originGid}&destinationGid={destinationGid}&limit={resultLimit}";

                Debug.Log("Time: " + time);
                
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