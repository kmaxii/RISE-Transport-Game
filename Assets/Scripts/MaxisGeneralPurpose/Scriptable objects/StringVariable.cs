using MaxisGeneralPurpose.Event;
using UnityEngine;

namespace MaxisGeneralPurpose.Scriptable_objects
{
    [CreateAssetMenu(menuName = "Custom/data/string")]
    public class StringVariable : DataCarrier
    {
        [SerializeField] private string value;


        public string Value
        {
            get => value;
            set
            {
                this.value = value;
                if (raiseOnValueChanged)
                    raiseOnValueChanged.Raise();
            }
        }

        public override string ToString()
        {
            return value;
        }
    }
}