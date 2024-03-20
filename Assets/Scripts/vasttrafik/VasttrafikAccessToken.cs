using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace vasttrafik
{
    public class VasttrafikAccessToken : MonoBehaviour
    {
        
        private const string AuthUrl = "https://ext-api.vasttrafik.se/token";
        private const string AuthenticationKey = "SjdHMmk4bTdKUTNNWndaUEFkVXd3UWZfd01VYTpSNzYwR3BWM2NmVWFTSk5BT0hLQjhaSnpxc2dh";

        private static string _accessToken = "";
        private const string GrantType = "grant_type=client_credentials";

        private void Awake()
        {
            StartCoroutine(RequestToken());
        }
        private IEnumerator RequestToken()
        {
            using (UnityWebRequest request = new UnityWebRequest(AuthUrl, "POST"))
            {
                byte[] bodyRaw = Encoding.UTF8.GetBytes(GrantType);
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
                request.SetRequestHeader("Authorization", "Basic " + AuthenticationKey);

                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    string tokenResponse = request.downloadHandler.text;
                    TokenResponse response = JsonUtility.FromJson<TokenResponse>(tokenResponse);

                    _accessToken = response.access_token;
                }
                else
                {
                    Debug.LogError("Token request failed: " + request.error);
                }
            }
        }
        
        
        public static string GetAccessToken()
        {
            return _accessToken;
        }

    }
}