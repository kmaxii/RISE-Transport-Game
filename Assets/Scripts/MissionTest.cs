using Missions;
using UnityEngine;

public class MissionTest : MonoBehaviour
{

    [SerializeField] private Mission mission;


    [SerializeField] private bool change;

    private bool last;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (last != change)
        {
            last = change;
            PoiSpawner poiSpawner = GetComponent<PoiSpawner>();

            poiSpawner.SpawnMission(mission);

        }
    }
}