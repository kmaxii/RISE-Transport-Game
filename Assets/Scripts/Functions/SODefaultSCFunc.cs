using MaxisGeneralPurpose.Scriptable_objects;
using UnityEngine;

namespace Functions
{
            /*
    * The formula for calculating stress and comfort change is shown below:

        y = 0.3bc(x + 0.5i0.05x)

        y: The net change in stress/comfort

        b: Boolean. 1 if change is increase of stress/comfort, -1 if decrease

        x: The gross change given by the event

        c: Comfortability level of the used mode of transport of the persona. MIN –1, MAX 5. The values shown to the player range from 1-5, and hide the actual value. A shown value of 1 corresponds to any comfortability level –1 <= c < 0.5, a 2 corresponds to 0.5 <= c < 2 and so on. If stress is changed outside of using a transport, this value should be 2

        i: Importance level of the the persona. MIN 1, MAX 5.
     */

    [CreateAssetMenu(menuName = "Rise/Functions/DefaultFunc")]
    public class SoDefaultSCFunc : SoStatsChangeFunction
    {
        [Header("Effect")] [Tooltip("(b) 1 if change is increase of stress/comfort, -1 if decrease")] [SerializeField]
        private bool increase;

        [Tooltip("(x) The gross change given by the event")] [SerializeField]
        private float grossChange;

        [Tooltip(
            "c: Comfortability level of the used mode of transport of the persona. MIN –1, MAX 5. The values shown to the player range from 1-5, and hide the actual value. A shown value of 1 corresponds to any comfortability level –1 <= c < 0.5, a 2 corresponds to 0.5 <= c < 2 and so on. If stress is changed outside of using a transport, this value should be 2")]
        [SerializeField]
        private FloatVariable comfortabilityLevel;

        [Tooltip("i: Importance level of the the persona. MIN 1, MAX 5.")] [SerializeField]
        private FloatVariable importanceLevel;
        
        public override float ExecuteFunction()
        {
            return ExecuteFunction(increase, grossChange, (comfortabilityLevel? comfortabilityLevel.Value : 1), (importanceLevel? importanceLevel.Value : 1));
        }

        /// <summary>
        /// Calculates the net change in stress/comfort based on the provided parameters.
        /// </summary>
        /// <param name="b">A boolean indicating whether the change is an increase (true) or decrease (false) of stress/comfort.</param>
        /// <param name="x">The gross change given by the event.</param>
        /// <param name="c">The comfortability level of the used mode of transport of the persona. The values range from -1 to 5. If stress is changed outside of using a transport, this value should be 2.</param>
        /// <param name="i">The importance level of the persona. The values range from 1 to 5.</param>
        /// <returns>The net change in stress/comfort.</returns>
        /// <remarks>
        /// The formula for calculating the net change in stress/comfort is: y = 0.3bc(x + 0.5i0.05x)
        /// </remarks>
        public static float ExecuteFunction(bool b, float x, float c, float i)
        {
            return 0.3f * (b ? 1 : -1) * c * (x + 0.5f * i * 0.05f * x);
        }
    }
}