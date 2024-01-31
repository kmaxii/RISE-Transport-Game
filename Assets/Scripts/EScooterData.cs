using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;


[CreateAssetMenu(menuName = "Rise/EScooterData")]
public class EScooterData : ScriptableObject
{
    private Dictionary<string, List<GridInfo>> _gridData;

    [SerializeField] private string resourceName = "escooter_data";
    
    private void Awake()
    {
        TextAsset file = Resources.Load<TextAsset>(resourceName);
        if (file != null)
        {
            EScooterDataWrapper wrapper = JsonUtility.FromJson<EScooterDataWrapper>(file.text);
            _gridData = new Dictionary<string, List<GridInfo>>();
            foreach (var item in wrapper.gridDataList)
            {
                _gridData.Add(item.key, item.gridInfos);
            }
            Debug.Log("Loaded EScooter data of " + resourceName + " with " + _gridData.Count + " entries");
        }
        else
        {
            Debug.LogError("JSON file not found in Resources!");
        }
    }

    public List<GridInfo> GetData(int xGrid, int yGrid)
    {
        string key = $"{xGrid}_{yGrid}";
        return _gridData.TryGetValue(key, out var value) ? value : new List<GridInfo>();
    }
    
    
    [Serializable]
    public class GridInfo
    {
        public double timeStamp;
        public double xPos;
        public double yPos;
        public string idleTime;
        public int entryId;
    }

    private void OnValidate()
    {
        Awake();
    }
}
