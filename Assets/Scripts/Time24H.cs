﻿using System;
using UnityEngine;

namespace Editor
{
    [Serializable]
    public class Time24H
    {
        public int hour;
        public int minute;

        public Time24H(int hour, int minute)
        {
            this.hour = Mathf.Clamp(hour, 0, 23);
            this.minute = Mathf.Clamp(minute, 0, 59);
        }

        public override string ToString()
        {
            return $"{hour:00}:{minute:00}";
        }

    
        public static Time24H operator +(Time24H a, Time24H b)
        {
            int totalMinutes = a.minute + b.minute;
            int totalHours = a.hour + b.hour + totalMinutes / 60;

            // Adjust for 24-hour format
            totalHours %= 24;
            totalMinutes %= 60;

            return new Time24H(totalHours, totalMinutes);
        }
        
        public static bool WillHourResetToZero(Time24H a, Time24H b)
        {
            int totalMinutes = a.minute + b.minute;
            int totalHours = a.hour + b.hour + totalMinutes / 60;

            return totalHours % 24 == 0;
        }
    }
}