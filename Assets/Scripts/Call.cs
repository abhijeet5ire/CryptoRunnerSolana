using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Call : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject loadingTap;
    [SerializeField] private GameObject butTap;

    private void Start()
    {
        loadingScreen.SetActive(false); // Hide loading screen initially
        loadingTap.SetActive(true);
        butTap.SetActive(true);
    }

    // Function to be invoked when the button is clicked
    public void StartLoadingMainMenu()
    {
        StartCoroutine(LoadMainMenu());
    }

    IEnumerator LoadMainMenu()
    {
        loadingScreen.SetActive(true); // Show loading screen

        // Simulate loading time
        yield return new WaitForSeconds(2f); // Adjust the time as needed

        // Check and request permission for activity recognition
        AndroidRuntimePermissions.Permission activityPermission = AndroidRuntimePermissions.RequestPermission("android.permission.ACTIVITY_RECOGNITION");
        if (activityPermission == AndroidRuntimePermissions.Permission.Granted)
        {
            Debug.Log("We have permission to access the step counter");
        }
        else
        {
            Debug.Log("Permission state for activity recognition: " + activityPermission); // No permission
        }

        // Check and request permission for location access
        AndroidRuntimePermissions.Permission locationPermission = AndroidRuntimePermissions.RequestPermission("android.permission.ACCESS_FINE_LOCATION");
        if (locationPermission == AndroidRuntimePermissions.Permission.Granted)
        {
            Debug.Log("We have permission to access the location");
        }
        else
        {
            Debug.Log("Permission state for location: " + locationPermission); // No permission
        }

        // Unload the current scene
        SceneManager.LoadScene("World", LoadSceneMode.Single);
    }

}
