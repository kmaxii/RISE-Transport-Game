using System;
using UnityEngine;

namespace vasttrafik
{
    public class VasttrafikTest : MonoBehaviour
    {
        private async void Awake()
        {
            var token = VasttrafikAccessToken.GetAccessToken();
            Debug.Log("Token: " + token);
        }
    }
}