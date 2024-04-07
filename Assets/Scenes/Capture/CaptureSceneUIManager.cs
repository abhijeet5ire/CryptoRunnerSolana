using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.IO;
public class CaptureSceneUIManager : MonoBehaviour
{
    [SerializeField] private CaptureSceneManager manager;
    [SerializeField] private GameObject successScreen;
    [SerializeField] private GameObject failScreen;
    [SerializeField] private GameObject gameScreen;
    [SerializeField] private Text orbCountText;
    [SerializeField] private Text LoadingText;

    private void Awake()
    {
        Assert.IsNotNull(manager);
        Assert.IsNotNull(successScreen);
        Assert.IsNotNull(failScreen);
        Assert.IsNotNull(gameScreen);
    }

    void Update()
    {
        switch (manager.Status)
        {
            case CaptureSceneStatus.InProgress:
                HandleInProgress();
                break;
            case CaptureSceneStatus.Successful:
                HandleSuccess();
                break;
            case CaptureSceneStatus.Failed:
                HandleFailure();
                break;
            default:
                break;
        }
    }

    private void HandleFailure()
    {
        UpdateVisibleScreen();
    }

    private void HandleSuccess()
    {   
        
        UpdateVisibleScreen();
        LoadingText.text="LoadingText....";
        if(manager.Status == CaptureSceneStatus.Successful)
        {
            StartCoroutine(MakeApiRequest());
        }
        LoadingText.text = "Minting .....";
    }
    private IEnumerator MakeApiRequest()
    {
        string mnemonicContent = File.ReadAllText("menomnic");
        Debug.Log(mnemonicContent);
        LoadingText.text = "Minting NFT";
        // URL of the API endpoint
        string apiUrl = $"https://solanamintnft.onrender.com/mintnft?recipient={mnemonicContent}"; 

        // Make the GET request
        UnityWebRequest request = UnityWebRequest.Get(apiUrl);

        // Send the request and wait for the response
        yield return request.SendWebRequest();

        // Check for errors
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError("Error making API request: " + request.error);
        }
        else
        {
            LoadingText.text = "Connecting to Server";
            StartCoroutine(ShowToastOnMainThread(request.downloadHandler.text));
            LoadingText.text = "Minted";
            // Handle the API response here (you can access it using request.downloadHandler.text)
            Debug.Log("API Response: " + request.downloadHandler.text);
        }

        // Continue with your existing logic or scene transition
        Invoke("MoveToWorldScene", 4.0f);
    }
    private void MoveToWorldScene()
    {
        StartCoroutine(LoadWorldScene());
    }

    private IEnumerator LoadWorldScene()
    {
        // Display a loading screen or loading bar if needed
        LoadingText.text = "Loading World Scene...";

        // Asynchronously load the world scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(PocketDroidConstants.SCENE_WORLD);

        // Wait until the scene is fully loaded
        while (!asyncLoad.isDone)
        {
            // Update the loading progress if needed
            // This could involve updating a loading bar
            // loadingProgress = asyncLoad.progress;

            yield return null;
        }

        // Everything is loaded, proceed with scene transition
        Debug.Log("World Scene Loaded!");
    }
    private void HandleInProgress()
    {
        UpdateVisibleScreen();
        orbCountText.text = manager.CurrentThrowAttempts.ToString();
    }

    private void UpdateVisibleScreen()
    {
        successScreen.SetActive(manager.Status == CaptureSceneStatus.Successful);
        failScreen.SetActive(manager.Status == CaptureSceneStatus.Failed);
        gameScreen.SetActive(manager.Status == CaptureSceneStatus.InProgress);
    }

    private IEnumerator ShowToastOnMainThread(string message)
    {
        // Wait for the next frame to switch back to the main thread
        yield return null;

        // Show the toast message
        ToastManager.Instance.ShowToast(message);
    }

}