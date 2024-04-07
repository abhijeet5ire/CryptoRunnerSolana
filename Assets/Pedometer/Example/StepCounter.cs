using UnityEngine;
using UnityEngine.UI;

#if UNITY_ANDROID
namespace PedometerU.Tests
{
    public class StepCounter : MonoBehaviour
    {
        public Text stepText, distanceText;
        private Pedometer pedometer;

        private void Start()
        {
            // Create a new pedometer only if the platform is Android
            if (Application.platform == RuntimePlatform.Android)
            {
                pedometer = new Pedometer(OnStep);
            }
            // Reset UI
            OnStep(0, 0);
        }

        private void OnStep(int steps, double distance)
        {
            // Display the values // Distance in feet
            stepText.text = steps.ToString();
            distanceText.text = (distance * 3.28084).ToString("F2") + " ft";

            // Only call GameManager.Instance if the platform is Android
            if (Application.platform == RuntimePlatform.Android)
            {
                GameManager.Instance?.CurrentPlayer.AddSteps(stepText.text);
            }
        }

        private void OnDisable()
        {
            // Release the pedometer if it was created
            if (pedometer != null && Application.platform == RuntimePlatform.Android)
            {
                pedometer.Dispose();
                pedometer = null;
            }
        }
    }
}
#endif
