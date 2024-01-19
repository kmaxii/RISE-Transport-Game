using System;
using System.Globalization;
using Interfaces;
using MaxisGeneralPurpose.Scriptable_objects;
using Scriptable_objects;
using TMPro;
using UnityEngine;

public class ShowEscooterPriceUi : MonoBehaviour
{
    [SerializeField] private FloatVariable initScooterPrice;
    [SerializeField] private FloatVariable pricePerMin;
    [SerializeField] private GameEvent showScooterUiEvent;
    [SerializeField] private GameEvent mountScooterEvent;

    [SerializeField] private TMP_Text text;
    private String _initialText;

    private void Awake()
    {
        _initialText = text.text;
    }

    private void OnEnable()
    {
        showScooterUiEvent.RegisterListener(OnEventRaised);
    }

    private void OnDisable()
    {
        showScooterUiEvent.UnregisterListener(OnEventRaised);
    }

    private void ShowChildren(bool value)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(value);
        }
    }
    
    public void OnEventRaised()
    {
        text.text = _initialText.Replace("<InitPrice>", initScooterPrice.Value + "")
            .Replace("<perMinPrice>", pricePerMin.Value + "");
        ShowChildren(true);
    }
}