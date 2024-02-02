using System;
using System.Collections;
using Interfaces;
using MaxisGeneralPurpose.Scriptable_objects;
using Scriptable_objects;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UiFloatVariableFiller : MonoBehaviour, IEventListenerInterface
{
    [SerializeField] private FloatVariable floatVariable;
    [SerializeField] private float maxValue = 100f;

    [SerializeField] private Image whiteHealthLost;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image blackBackground;

    [SerializeField] private TMP_Text variableName;


    [FormerlySerializedAs("timeBeforeWhiteHealthLoss")] [Tooltip("The amount of time before the white health disappear")] [Range(0, 5)] [SerializeField]
    private float timeBeforeWhiteLoss = 1;

    [FormerlySerializedAs("timeBeforeWhiteHealthGain")] [Tooltip("The amount of time before the white health disappear when gaining health")] [Range(0, 5)] [SerializeField]
    private float timeBeforeWhiteGain = 0.1f;

    [FormerlySerializedAs("whiteHealthLossTime")] [Tooltip("The amount of time it takes the white health to disappear")] [Range(0, 5)] [SerializeField]
    private float whiteLossTime = 0.5f;

    [FormerlySerializedAs("whiteHealthGainTime")]
    [Tooltip("The amount of time it takes the white health to disappear when gaining hp")]
    [Range(0, 5)]
    [SerializeField]
    private float whiteGainTime = 0.4f;

    private float _time;

    private float _healthPercentBefore = 1f;

    public static UiFloatVariableFiller Instance;

    // Start is called before the first frame update
    private void Start()
    {
        Instance = this;
        //We call this once to update our health to the correct amount when entering a scene
        OnValueChange();
    }

    private void Awake()
    {
        variableName.text = floatVariable.name;
    }

    void OnEnable()
    {
        Assert.IsNotNull(floatVariable.raiseOnValueChanged,
            $"Put a value in the raiseOnValueChanged field in {floatVariable.name}");

        floatVariable.raiseOnValueChanged.RegisterListener(OnValueChange);
    }


    private void OnDisable()
    {
        floatVariable.raiseOnValueChanged.UnregisterListener(OnValueChange);
    }


    public void OnValueChange()
    {
        StopAllCoroutines();

        //Percent as a decimal number
        float percent = floatVariable.Value / maxValue;

        //We increased the stat
        if (percent >= 1 || _healthPercentBefore < percent)
            IncreasedStat(percent);
        else
            DecreasedStat(percent);

        _healthPercentBefore = percent;
    }

    private void IncreasedStat(float newLivePercent)
    {
        whiteHealthLost.fillAmount = newLivePercent;
        StartCoroutine(LerpFill(healthBar, _healthPercentBefore, newLivePercent, timeBeforeWhiteGain,
            whiteLossTime));
    }

    private void DecreasedStat(float newLivePercent)
    {
        healthBar.fillAmount = newLivePercent;

        whiteHealthLost.fillAmount = _healthPercentBefore;


        StartCoroutine(LerpFill(whiteHealthLost, _healthPercentBefore, newLivePercent - 0.1f,
            timeBeforeWhiteLoss,
            whiteGainTime));


        blackBackground.fillAmount = 1f;
    }

    private static IEnumerator LerpFill(Image image, float startPercent, float endPercent, float initialDelay,
        float duration)
    {
        yield return new WaitForSeconds(initialDelay);

        float time = 0;

        while (time < 1)
        {
            time += Time.deltaTime * (1 / duration);
            image.fillAmount = Mathf.Lerp(startPercent, endPercent, time);
            yield return new WaitForEndOfFrame();
        }

        image.fillAmount = endPercent;
    }

    public void ShowWhatHealthWillBeLost(float amount)
    {
        float newLifePercent = (floatVariable.Value - amount) / maxValue;

        healthBar.fillAmount = newLifePercent;

        //Filled from reverse
        blackBackground.fillAmount = 1 - (_healthPercentBefore);

        //We set white thing to 0 as we don't want to see it with the blue
        whiteHealthLost.fillAmount = 0f;
    }

    public void HideWhatHealthWillBeLost()
    {
        healthBar.fillAmount = floatVariable.Value / maxValue;
    }
}