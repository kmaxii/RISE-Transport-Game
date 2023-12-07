using System;
using UnityEngine;

namespace vasttrafik
{
    public class VasttrafikTest : MonoBehaviour
    {
        private async void Awake()
        {
            var token = await VasttrafikAccessToken.GetAccessTokenAsync();
            Debug.Log("Token: " + token);
        }
    }
}