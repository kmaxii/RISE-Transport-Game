using MaxisGeneralPurpose.Scriptable_objects;
using UnityEngine;

public class IntTicker : MonoBehaviour
{
    [SerializeField] private IntVariable toIncrease;
    [SerializeField] private GameEvent onEvent;
    [SerializeField] private int increaseAmount;
    [SerializeField] private BoolVariable ifThis;
    [SerializeField] private bool isThis;

    [SerializeField] private Transform player;
    private void OnEnable()
    {
        onEvent.RegisterListener(OnEventRaised);
    }

    private void OnDisable()
    {
        onEvent.UnregisterListener(OnEventRaised);
    }

    private void OnEventRaised()
    {
        Debug.Log(Vector3.Distance(player.position, transform.position));
        if (ifThis.Value != isThis || !(Vector3.Distance(player.position, transform.position) > 2)) return;
        transform.position = player.position;
        toIncrease.Value += increaseAmount;
    }
}