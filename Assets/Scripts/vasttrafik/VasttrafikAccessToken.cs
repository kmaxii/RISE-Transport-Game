using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Networking;

namespace vasttrafik
{
    public class VasttrafikAccessToken
    {
        
        private const string AuthUrl = "https://ext-api.vasttrafik.se/token";
        private const string ClientId = "J7G2i8m7JQ3MZwZPAdUwwQf_wMUa";
        private const string Secret = "R760GpV3cfUaSJNAOHKB8ZJzqsga";
        private const string AuthenticationKey = "SjdHMmk4bTdKUTNNWndaUEFkVXd3UWZfd01VYTpSNzYwR3BWM2NmVWFTSk5BT0hLQjhaSnpxc2dh";

        IEnumerator GetAccessToken()
        {
            WWWForm form = new WWWForm();
            form.AddField("grant_type", "client_credentials");
            form.AddField("client_id", ClientId);
            form.AddField("client_secret", Secret);

            UnityWebRequest www = UnityWebRequest.Post(AuthUrl, form);
            www.SetRequestHeader("Authorization", "Basic " + AuthenticationKey);

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string responseText = www.downloadHandler.text;
                TokenResponse tokenResponse = JsonUtility.FromJson<TokenResponse>(responseText);
                string accessToken = tokenResponse.access_token;

                // Use the access token for your API requests
            }
        }
        
        /*private static async Task<string> GetAccessToken()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, AuthUrl);
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", AuthenticationKey);
            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", ClientId },
                { "client_secret", Secret }
            });

            HttpResponseMessage response = await Client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonUtility.FromJson<TokenResponse>(responseContent);
                return tokenResponse.access_token;
            }

            throw new InvalidOperationException("Unable to retrieve access token.");
        }*/
    }
}