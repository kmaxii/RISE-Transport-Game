using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomPropertyDrawer(typeof(Time24H))]
    public class TimeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            var hourRect = new Rect(position.x, position.y, 50, position.height);
            var minuteRect = new Rect(position.x + 55, position.y, 50, position.height);

            SerializedProperty hourProp = property.FindPropertyRelative("hour");
            SerializedProperty minuteProp = property.FindPropertyRelative("minute");

            // Handling the scroll wheel event
            HandleScrollWheel(hourRect, hourProp, 24);
            HandleScrollWheel(minuteRect, minuteProp, 60);

            // Drawing the int popup
            hourProp.intValue = EditorGUI.IntPopup(hourRect, hourProp.intValue, 
                Enumerable.Range(0, 24).Select(i => i.ToString("00")).ToArray(), 
                Enumerable.Range(0, 24).ToArray());

            minuteProp.intValue = EditorGUI.IntPopup(minuteRect, minuteProp.intValue, 
                Enumerable.Range(0, 60).Select(i => i.ToString("00")).ToArray(), 
                Enumerable.Range(0, 60).ToArray());

            EditorGUI.EndProperty();
        }

        private void HandleScrollWheel(Rect rect, SerializedProperty property, int max)
        {
            Event e = Event.current;

            if (rect.Contains(e.mousePosition) && e.type == EventType.ScrollWheel)
            {
                // Change the property value based on scroll direction
                if (e.delta.y > 0)
                {
                    property.intValue--;
                    if (property.intValue < 0) property.intValue = max - 1;
                }
                else
                {
                    property.intValue++;
                    if (property.intValue >= max) property.intValue = 0;
                }

                e.Use(); // Mark the event as used
            }
        }
    }

}