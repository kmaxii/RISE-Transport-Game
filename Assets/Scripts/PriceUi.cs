using System.Collections.Generic;
using MaxisGeneralPurpose.Scriptable_objects;
using Scriptable_objects;
using TMPro;
using UnityEngine;

public class PriceUi : MonoBehaviour
{
    [SerializeField] private FloatVariable money;
    [SerializeField] private GameEvent cantAffordEvent;


    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text mainText;

    [NonReorderable] [SerializeField] List<PriceUiElement> priceUiElements;

    private PriceUiElement _showingElement;

    private void OnEnable()
    {
        StartListening();
    }

    private void OnDisable()
    {
        StopListening();
    }

    private void Show(PriceUiElement priceUiElement)
    {
        _showingElement = priceUiElement;
        titleText.text = priceUiElement.TitleText;
        mainText.text = priceUiElement.MainText;

        ShowChildren(true);
    }


    public void Trigger()
    {
        if (_showingElement == null)
        {
            Debug.LogError("No element is showing in cost UI. Can not trigger.");
            return;
        }

        if (!CanAfford(_showingElement.price))
            return;

        money.Value -= _showingElement.price.Value;
        _showingElement.triggerOnAccept.Raise();
        CloseUI();
    }


    private void ShowChildren(bool value)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(value);
        }
    }

    private bool CanAfford(FloatVariable floatVariable)
    {
        if (!(money.Value - floatVariable.Value < 0)) return true;
        cantAffordEvent.Raise();
        return false;
    }

    private void Hide(PriceUiElement priceUiElement)
    {
        CloseUI();
    }

    public void CloseUI()
    {
        _showingElement = null;
        ShowChildren(false);
    }

    private void StartListening()
    {
        foreach (var priceUiElement in priceUiElements)
        {
            StartListening(priceUiElement);
        }
    }

    private void StartListening(PriceUiElement priceUiElement)
    {
        priceUiElement.StartListening();
        priceUiElement.ShowPriceUiElement += Show;
        priceUiElement.HidePriceUiElement += Hide;
    }


    private void StopListening(PriceUiElement priceUiElement)
    {
        priceUiElement.StopListening();
        priceUiElement.ShowPriceUiElement -= Show;
        priceUiElement.HidePriceUiElement -= Hide;
    }

    private void StopListening()
    {
        foreach (var priceUiElement in priceUiElements)
        {
            StopListening(priceUiElement);
        }
    }
}