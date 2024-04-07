using UnityEngine;
using UnityEngine.UI;

public class ToastManager : MonoBehaviour
{
    public static ToastManager Instance;

    public GameObject toastPrefab;
    public Transform canvasTransform;
    public float displayTime = 2.0f;

    private void Awake()
    {
        // Ensure there is only one instance of ToastManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowToast(string message)
    {
        // Instantiate the toast prefab
        GameObject toastObject = Instantiate(toastPrefab, canvasTransform);
        Text toastText = toastObject.GetComponentInChildren<Text>();

        if (toastText != null)
        {
            toastText.text = message;
        }
        else
        {
            Debug.LogError("Text component not found in the toastPrefab!");
        }
        // Destroy the toast after a certain time
        Destroy(toastObject, displayTime);
    }
}
