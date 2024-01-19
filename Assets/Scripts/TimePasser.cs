using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePasser : MonoBehaviour
{

    [SerializeField] private TimeVariable timeVariable;

    [SerializeField] private int secondPerRealSecond = 30;

    private float _currentSecondsPassed;

    private void Update()
    {
        _currentSecondsPassed += secondPerRealSecond * Time.deltaTime;
        if (_currentSecondsPassed >= 60)
        {
            timeVariable.Time24H += new Time24H(0, 1);
            _currentSecondsPassed -= 60;
        }
    }
    
}
