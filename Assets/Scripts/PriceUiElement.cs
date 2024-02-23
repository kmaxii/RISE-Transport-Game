using System;
using System.Collections.Generic;
using MaxisGeneralPurpose.Event;
using MaxisGeneralPurpose.Scriptable_objects;
using QuickEye.Utility;
using Scriptable_objects;
using UnityEngine;

public delegate void PriceUiElementDelegate(PriceUiElement priceUiElement);

[Serializable]
public class PriceUiElement
{
    [SerializeField] private PriceUiTexts priceUiTexts;
    [SerializeField] private UnityDictionary<string, DataCarrier> textReplacements;

    [SerializeField]  private GameEvent showOnEvent;
    [SerializeField] private GameEvent hideOnEvent;
    public GameEvent triggerOnAccept;
    public FloatVariable price;

    public PriceUiElementDelegate ShowPriceUiElement;
    public PriceUiElementDelegate HidePriceUiElement;

    public void StartListening()
    {
        if (showOnEvent)
            showOnEvent.RegisterListener(Show);
        if (hideOnEvent)
            hideOnEvent.RegisterListener(Hide);

    }

    public void StopListening()
    {
        if (showOnEvent)
            showOnEvent.UnregisterListener(Show);
        if (hideOnEvent)
            hideOnEvent.UnregisterListener(Hide);

    }
    
    public void Show()
    {
        ShowPriceUiElement?.Invoke(this);
    }
    
    public void Hide()
    {
        HidePriceUiElement?.Invoke(this);
    }
    
    public string TitleText
    {
        get
        {
            string titleText = priceUiTexts.titleText;
            foreach (KeyValuePair<string, DataCarrier> replacement in textReplacements)
            {
                titleText = titleText.Replace(replacement.Key, replacement.Value.ToString());
            }

            return titleText;
        }
    }

    public string MainText
    {
        get
        {
            string mainText = priceUiTexts.mainText;
            foreach (KeyValuePair<string, DataCarrier> replacement in textReplacements)
            {
      
                mainText = mainText.Replace(replacement.Key, replacement.Value.ToString());
            }
            
            return mainText;
        }
    }
}