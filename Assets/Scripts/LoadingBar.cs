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

    [Tooltip("The speed to normally pass the time by. Min per sec")]
    [SerializeField] private float speed = 5;

    [Tooltip("Includes the one sec fade in and one sec fade out time")]
    [SerializeField] private float minimumTimeShown = 4;

    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] private BoolVariable isTimeWarping;

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
        isTimeWarping.Value = true;

        
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
        
        
        //Set a float "speedToUse" to be speed if the time to reach is greater than the minimum time shown
        //Otherwise set it to be the time to reach divided by the minimum time shown
        
        Debug.Log("Time it will take to reach: " + (to - startTime));
        
        float speedToUse = ((to - startTime) / speed) > minimumTimeShown ? speed : (to - startTime) / minimumTimeShown;
        
        //print speedtouse
        Debug.Log("Speed to use: " + speedToUse);
        
        while (currentTime < to)
        {
                
            currentTime += speedToUse * Time.deltaTime;
            //Set the slider value to how far it has loaded from startTime to final time
            slider.value = (currentTime - startTime) / (to - startTime);

            time.Time24H = new Time24H((int) currentTime / 60, (int) currentTime % 60);
            yield return null;
        }

        canvasGroup.blocksRaycasts = false;
        StartCoroutine(FadeCanvasGroup(false, 1));
        isTimeWarping.Value = false;

     //   ShowAllChildren(false);
    }
}