using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Networking;

namespace DataCollection
{
    [Serializable]
    public class UserDataList
    {
        public List<UserData> userDataList = new List<UserData>();
    }

    public class DataManager
    {
        private UserDataList userDataList = new UserDataList();
        private const string FilePath = "data.json";

        public void AddUserData(UserData data)
        {
            userDataList.userDataList.Add(data);
            Debug.Log("Saving data. Total data points saved: " + userDataList.userDataList.Count);
        }
        
        public void ClearUserData()
        {
            userDataList.userDataList.Clear();
        }

        public void SaveDataToFile()
        {
            string jsonData = JsonUtility.ToJson(userDataList, true);
            File.WriteAllText(FilePath, jsonData);
            Debug.Log("Data saved to file. Total data points saved: " + userDataList.userDataList.Count);
        }

        public void LoadDataFromFile()
        {
            if (File.Exists(FilePath))
            {
                string jsonData = File.ReadAllText(FilePath);
                userDataList = JsonUtility.FromJson<UserDataList>(jsonData) ?? new UserDataList();
                Debug.Log("Data loaded from file.");
            }
            else
            {
                Debug.LogWarning("Data file does not exist.");
            }
        }

// Add this method to validate all certificates
        private bool RemoteCertificateValidationCallback(System.Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true; // Accept all certificates
        }

        public IEnumerator SendDataToAPI(string apiUrl)
        {
            // Ignore SSL certificate errors (only for development)
            ServicePointManager.ServerCertificateValidationCallback = RemoteCertificateValidationCallback;

            string jsonData = JsonUtility.ToJson(userDataList);

            using UnityWebRequest webRequest = new UnityWebRequest(apiUrl, "POST");
            byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(jsonData);
            webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error sending data: {webRequest.error}");
            }
            else
            {
                Debug.Log("Data successfully sent to the server.");
            }
        }

        // Example method to delete the data file
        public void DeleteDataFile()
        {
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
                Debug.Log("Data file deleted successfully.");
            }
            else
            {
                Debug.LogWarning("Data file does not exist.");
            }
        }
    }
}