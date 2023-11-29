using System;
using Interfaces;
using MaxisGeneralPurpose.Event;
using MaxisGeneralPurpose.Scriptable_objects;
using TMPro;
using UnityEngine;

namespace MaxisGeneralPurpose
{
    public class TextWriterFromDataCarrier : MonoBehaviour, IEventListenerInterface
    {
        [SerializeField] private DataCarrier value;

        private TMP_Text _textMesh;


        private void Awake()
        {
            if (!_textMesh)
            {
                _textMesh = GetComponent<TMP_Text>();
            }
            OnEventRaised();
        }

        
        public void Setup(IntVariable intVariable)
        {
            if (!_textMesh)
            {
                _textMesh = GetComponent<TMP_Text>();
            }

            value = intVariable;
            OnEventRaised();
            value.raiseOnValueChanged.RegisterListener(this);
        }


        private void OnDisable()
        {
            value.raiseOnValueChanged.UnregisterListener(this);
        }

        public void OnEventRaised()
        {
            _textMesh.text = value.ToString();
        }
    }
}