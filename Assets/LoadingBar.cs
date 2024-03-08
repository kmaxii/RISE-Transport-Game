using System.Collections;
using MaxisGeneralPurpose.Scriptable_objects;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    [SerializeField] private TimeVariable time;


    [SerializeField] private TimeVariable lastBusArriveTime;
    [SerializeField] private GameEvent traveldByBusEvent;

    [SerializeField] private float speed = 5;

    [SerializeField] private CanvasGroup canvasGroup;

    private void OnEnable()
    {
        traveldByBusEvent.RegisterListener(ShowScreen);
    }

    private void OnDisable()
    {
        traveldByBusEvent.UnregisterListener(ShowScreen);
    }

    private void ShowScreen()
    {
        ShowAllChildren(true);
        
        StartCoroutine(FadeCanvasGroup(true, 1));

        canvasGroup.blocksRaycasts = true;
        
        StartCoroutine(Loading(lastBusArriveTime.Time24H));
    }

    private void ShowAllChildren(bool show)
    {
        //Go through every child of the transform
        for (int i = 0; i < transform.childCount; i++)
        {
            //Set the child to be active or not
            transform.GetChild(i).gameObject.SetActive(show);
        }
    }
    
    
    private IEnumerator FadeCanvasGroup(bool fadeIn, float duration)
    {
        float start = fadeIn ? 0 : 1;
        float end = fadeIn ? 1 : 0;
        float currentTime = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, currentTime / duration);
            yield return null;
        }

   
    }
    

    private IEnumerator Loading(Time24H toTime)
    {
        float startTime = time.Time24H.TotalMinutes;
        float to = toTime.TotalMinutes;
        var currentTime = startTime;
        while (currentTime < to)
        {
            currentTime += speed * Time.deltaTime;
            //Set the slider value to how far it has loaded from startTime to final time
            slider.value = (currentTime - startTime) / (to - startTime);

            time.Time24H = new Time24H((int) currentTime / 60, (int) currentTime % 60);
            yield return null;
        }

        canvasGroup.blocksRaycasts = false;
        StartCoroutine(FadeCanvasGroup(false, 1));

     //   ShowAllChildren(false);
    }
}