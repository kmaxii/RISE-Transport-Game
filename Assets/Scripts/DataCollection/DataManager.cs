using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

        public IEnumerator SendDataToAPI(string apiUrl)
        {
            string jsonData = JsonUtility.ToJson(userDataList);

            using (UnityWebRequest webRequest = UnityWebRequest.Put(apiUrl, jsonData))
            {
                webRequest.SetRequestHeader("Content-Type", "application/json");
                yield return webRequest.SendWebRequest();

                if (webRequest.isNetworkError || webRequest.isHttpError)
                {
                    Debug.LogError($"Error sending data: {webRequest.error}");
                }
                else
                {
                    Debug.Log("Data successfully sent to the server.");
                }
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