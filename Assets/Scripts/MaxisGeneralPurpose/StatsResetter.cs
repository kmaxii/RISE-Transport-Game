using System;
using Editor;
using MaxisGeneralPurpose.Scriptable_objects;
using UnityEngine;

namespace MaxisGeneralPurpose
{
    public class StatsResetter : MonoBehaviour
    {
        [NonReorderable] [SerializeField] private IntVariableAndInt[] toSetArray;

        [NonReorderable] [SerializeField] private TimeVariable time;
        [SerializeField] private Time24H startTime;

        // Start is called before the first frame update
        void Start()
        {
            foreach (var toSet in toSetArray)
            {
                toSet.intVariable.Value = toSet.value;
            }

            time.Time24H = startTime;
        }
    }

    [Serializable]
    public struct IntVariableAndInt
    {
        public int value;
        public IntVariable intVariable;
    }
}