using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneTransitionManager : Singleton<SceneTransitionManager>
{
    private AsyncOperation sceneAsync;

    public void GoToScene(string sceneName, List<GameObject> objectsToMove)
    {
        StartCoroutine(LoadScene(sceneName, objectsToMove));
    }

    private IEnumerator LoadScene(string sceneName, List<GameObject> objectsToMove)
    {
        // Log the count and names of GameObjects in the list
        Debug.Log("Objects to move count: " + objectsToMove.Count);
        foreach (GameObject obj in objectsToMove)
        {
            if (obj != null)
            {
                Debug.Log("Object to move: " + obj.name);
            }
            else
            {
                Debug.LogWarning("Encountered a null GameObject in the list.");
            }
        }

        
        sceneAsync = SceneManager.LoadSceneAsync(sceneName);
        yield return sceneAsync;

        Scene sceneToLoad = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(sceneToLoad);

        foreach (GameObject obj in objectsToMove)
        {
            if (obj != null)
            {
                // Retain the GameObject across scene changes
                DontDestroyOnLoad(obj);
              
                SceneManager.MoveGameObjectToScene(obj, sceneToLoad);
                obj.transform.position = new Vector3(0f, 0.5f, 1.0f);
                float rotationAngle = 180f; 
                               
                Quaternion rotation = Quaternion.Euler(0f, rotationAngle, 0f);
                                
                obj.transform.rotation *= rotation;
            }
            else
            {
                Debug.LogWarning("Attempted to move a null GameObject.");
            }
        }
    }



}