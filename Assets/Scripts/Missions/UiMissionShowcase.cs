using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Missions
{
    public class UiMissionShowcase : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        private DayMission _showingMission;

        [SerializeField] private Image _image;

        public void Show(DayMission dayMission)
        {
            _showingMission = dayMission;
            text.text = dayMission.ToString();
            _image.sprite = dayMission.Mission.Sprite;
        }
    }
}