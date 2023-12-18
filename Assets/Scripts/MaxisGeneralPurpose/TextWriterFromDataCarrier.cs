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

        [SerializeField] private string postFix;


        private void Awake()
        {
            if (!_textMesh)
            {
                _textMesh = GetComponent<TMP_Text>();
            }
            OnEventRaised();
        }

        private void OnEnable()
        {
            value.raiseOnValueChanged.RegisterListener(this);

        }

        private void OnDisable()
        {
            value.raiseOnValueChanged.UnregisterListener(this);
        }

        public void OnEventRaised()
        {
            _textMesh.text = value + postFix;
        }
    }
}