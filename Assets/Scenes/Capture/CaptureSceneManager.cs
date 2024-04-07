using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CaptureSceneManager : PocketDroidsSceneManager
{

    [SerializeField] private int maxThrowAttempts = 3;
    [SerializeField] private GameObject orb;
    [SerializeField] private Vector3 spawnPoint;
    private int currentThrowAttempts;
    private CaptureSceneStatus status = CaptureSceneStatus.InProgress;
    public int MaxThrowAttempts
    {
        get { return maxThrowAttempts; }
    }
    public int CurrentThrowAttempts
    {
        get { return currentThrowAttempts; }
    }

    public CaptureSceneStatus Status
    {
        get { return status; }
    }
    private void Start()
    {
        CalculateMaxThrows();
        currentThrowAttempts = maxThrowAttempts;
    }

    private void CalculateMaxThrows()
    {
        maxThrowAttempts += GameManager.Instance.CurrentPlayer.Lvl / 5;
    }

    public void OrbDestroyed()
    {
        currentThrowAttempts--;
        if (currentThrowAttempts <= 0)
        {
            if (status != CaptureSceneStatus.Successful)
            {
                status = CaptureSceneStatus.Failed;
                Invoke("MoveToWorldScene", 2.0f);
            }
        }
        else
        {
            Instantiate(orb, spawnPoint, Quaternion.identity);
        }
    }

    public override void PlayerTapped()
    {
        print("CaptureSceneManager.PlayerTapped activated");
    }
    public override void droidTapped(GameObject droid)
    {
        print("CaptureSceneManager.droidTapped activated");
    }

    public override void DroidCollision(GameObject droid, Collision other)
    {
        status = CaptureSceneStatus.Successful;
      
    }


    private void MoveToWorldScene()
    {
        // Load objects asynchronously
        StartCoroutine(LoadObjects(() =>
        {
            // Objects are loaded, load scenario and move to world scene
            SceneTransitionManager.Instance.GoToScene(PocketDroidConstants.SCENE_WORLD, new List<GameObject>());
        }));
    }

    private IEnumerator LoadObjects(Action onComplete)
    {
        // Load objects here asynchronously
        // For example, you can use Unity's Resource.LoadAsync or AssetBundle.LoadAssetAsync

        // For demonstration purposes, let's wait for 3 seconds
        yield return new WaitForSeconds(3);

        // Call the onComplete callback when objects are loaded
        onComplete?.Invoke();
    }

}