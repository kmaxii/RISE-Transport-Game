using System;
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
    }
}