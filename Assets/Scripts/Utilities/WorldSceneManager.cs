using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSceneManager : PocketDroidsSceneManager
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void droidTapped(GameObject droid)
    {
        List<GameObject> list = new List<GameObject>();
        list.Add(droid);
        DontDestroyOnLoad(droid);
        SceneTransitionManager.Instance.GoToScene(PocketDroidConstants.SCENE_CAPTURE, list);
    }

    public override void PlayerTapped()
    {
        throw new System.NotImplementedException();
    }
}