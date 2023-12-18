using TMPro;
using UnityEngine;

public class MissionListElement : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    public void SetText(string content)
    {
        text.text = content;
    }
}
