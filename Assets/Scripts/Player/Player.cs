using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Android;

public class Player : MonoBehaviour
{
    [SerializeField] private int xp = 0;
    [SerializeField] private int requiredXp = 100;
    [SerializeField] private int levelBase = 100;
    [SerializeField] private string stepcount = "0";

    [SerializeField] private List<GameObject> droids = new List<GameObject>();
    private int lvl = 1;
    private string path;

    void Start()
    {
        path = Application.persistentDataPath + "/player.dat";
        Load();
    }

    public string StepCount
    {
        get { return stepcount; }
    }
    public int Xp
    {
        get { return xp; }
    }
    public int RequiredXp
    {
        get { return requiredXp; }
    }
    public int LevelBase
    {
        get { return levelBase; }
    }
    public List<GameObject> Droids
    {
        get { return droids; }
    }

    public int Lvl
    {
        get { return lvl; }
    }
    public void AddSteps(string step)
    {
        int parsedStep;
        if (int.TryParse(step, out parsedStep))
        {
            stepcount = (int.Parse(stepcount) + parsedStep).ToString();
            Save();
        }
        else
        {
            Debug.LogError("Failed to parse step input.");
        }
    }
    public void AddXp(int xp)
    {
        this.xp += Mathf.Max(0, xp);
        InitLevelData();
        Save();
    }
    public void AddDroid(GameObject droid)
    {
        droids.Add(droid);
        Save();
    }
    private void InitLevelData()
    {
        lvl = (xp / levelBase) + 1;
        requiredXp = levelBase * lvl;
    }

    private void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(path);
        PlayerData data = new PlayerData(this);
        bf.Serialize(file, data);
        file.Close();
    }

    private void Load()
    {
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            xp = data.Xp;
            requiredXp = data.RequiredXp;
            levelBase = data.LevelBase;
            lvl = data.Lvl;
            stepcount = data.StepCount;
            // import player droids
        }
        else
        {
            InitLevelData();
        }
    }
}
