using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace DataCollection
{
    public static class DataManager
    {
        private const string FilePath = "data.json";

        public static void SaveUserData(UserData data)
        {
            string jsonData = JsonUtility.ToJson(data, true);
            File.WriteAllText(FilePath, jsonData);
        }

        public static IEnumerator SendDataToAPI(string apiUrl)
        {
            if (!File.Exists(FilePath))
            {
                Debug.LogError("File does not exist.");
                yield break; // Exit the coroutine early
            }

            string content = File.ReadAllText(FilePath);
            UserData data = JsonUtility.FromJson<UserData>(content);

            // Convert UserData object back to JSON string to send it to the API
            string jsonData = JsonUtility.ToJson(data);

            using UnityWebRequest webRequest = UnityWebRequest.Put(apiUrl, jsonData);
            webRequest.SetRequestHeader("Content-Type", "application/json");
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.LogError($"Error: {webRequest.error}");
            }
            else
            {
                Debug.Log("Data successfully sent to the server.");
            }
        }
    }
}