using System;
using System.Globalization;
using MaxisGeneralPurpose.Event;
using UnityEngine;

namespace MaxisGeneralPurpose.Scriptable_objects
{
    [CreateAssetMenu(menuName = "Custom/data/float")]
    public class FloatVariable : DataCarrier
    {
        [SerializeField] private float value;

        
        public float Value
        {
            get => value;
            set
            {
                if (Math.Abs(this.value - value) < 0.001f) return;
                this.value = value;
                if (raiseOnValueChanged)
                    raiseOnValueChanged.Raise();
            }
        }
        public override string ToString()
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }
    }
}