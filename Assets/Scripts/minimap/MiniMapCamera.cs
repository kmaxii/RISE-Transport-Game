using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    private static MiniMapCamera _instance;

    public static MiniMapCamera Instance => _instance;
    void Awake()
    {
        _instance = this;
    }

}