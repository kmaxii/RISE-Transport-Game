using TMPro;
using UnityEngine;

namespace Missions
{
    public class UiMissionShowcase : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        private DayMission _showingMission;

        public void Show(DayMission dayMission)
        {
            _showingMission = dayMission;
            text.text = dayMission.ToString();
        }
    }
}