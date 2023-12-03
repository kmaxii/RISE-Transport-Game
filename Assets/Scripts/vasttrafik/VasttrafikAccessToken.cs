using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UnityEngine;

namespace vasttrafik
{
    public class VasttrafikAccessToken
    {
        
        private const string AuthUrl = "https://ext-api.vasttrafik.se/token";
        private const string AuthenticationKey = "SjdHMmk4bTdKUTNNWndaUEFkVXd3UWZfd01VYTpSNzYwR3BWM2NmVWFTSk5BT0hLQjhaSnpxc2dh";

        private static string _accessToken = "";
        
        public static async Task<string> GetAccessTokenAsync()
        {
            if (_accessToken != "")
                return _accessToken;
            
            using var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, AuthUrl);
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", AuthenticationKey);
            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" }
            });

            HttpResponseMessage response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("Failed to retrieve access token");
            }

            string responseContent = await response.Content.ReadAsStringAsync();
            TokenResponse tokenResponse = JsonUtility.FromJson<TokenResponse>(responseContent);

            _accessToken = tokenResponse.access_token;
            
            return _accessToken;
        }

    }
}