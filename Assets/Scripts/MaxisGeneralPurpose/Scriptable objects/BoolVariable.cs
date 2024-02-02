using MaxisGeneralPurpose.Event;
using UnityEngine;

namespace MaxisGeneralPurpose.Scriptable_objects
{
    [CreateAssetMenu(menuName = "Custom/data/bool")]
    public class BoolVariable : DataCarrier
    {
        [SerializeField] private bool value;

        public bool Value
        {
            get => value;
            set
            {
                if (value == this.value) return;

                this.value = value;
                if (raiseOnValueChanged)
                    raiseOnValueChanged.Raise();
            }
        }


        public override string ToString()
        {
            return value.ToString();
        }
    }
}