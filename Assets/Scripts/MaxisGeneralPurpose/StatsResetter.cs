using System;
using MaxisGeneralPurpose.Scriptable_objects;
using Scriptable_objects;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;
using Interfaces;


namespace MaxisGeneralPurpose
{
    public class StatsResetter : MonoBehaviour
    {
        [NonReorderable] [SerializeField] private IntVariableAndInt[] toSetIntVariable;
        [NonReorderable] [SerializeField] private FloatVariableAndInt[] toSetFloats;

        [NonReorderable] [SerializeField] private TimeVariable time;
        [SerializeField] private Time24H startTime;

        [SerializeField] private GameEvent startMusicEvent;

        // Start is called before the first frame update
        void Start()
        {
            foreach (var toSet in toSetIntVariable)
            {
                toSet.intVariable.Value = toSet.value;
            }
            foreach (var toSet in toSetFloats)
            {
                toSet.floatVariable.Value = toSet.value;
            }

            time.Time24H = startTime;
        }
        private void OnEnable()
        {
            startMusicEvent.Raise();
        }
    }

    [Serializable]
    public struct IntVariableAndInt
    {
        public int value;
        public IntVariable intVariable;
    }
    
    [Serializable]
    public struct FloatVariableAndInt
    {
        public float value;
        public FloatVariable floatVariable;
    }


}