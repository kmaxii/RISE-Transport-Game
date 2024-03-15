using System;

namespace DataCollection
{
    [Serializable]
    public class UserData
    {
        public string time;
        public double longitude;
        public double latitude;
        public string busTransport;
        public string activityChange;
        public float money;
        public bool isOnScooter;
        public string activities;

        public UserData(string time, double longitude, double latitude, string busTransport, string activityChange, float money, bool isOnScooter, string activities)
        {
            this.time = time;
            this.longitude = longitude;
            this.latitude = latitude;
            this.busTransport = busTransport;
            this.activityChange = activityChange;
            this.money = money;
            this.isOnScooter = isOnScooter;
            this.activities = activities;
        }
    }

}