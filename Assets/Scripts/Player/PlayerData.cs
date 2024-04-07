using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
[Serializable] public class PlayerData
{
    private int xp;
    private int requiredXp;
    private int levelBase;
    private int lvl;
    private string stepcount;
    private List<DroidData> droids;
    public int Xp { get { return xp; } }
    public string StepCount { get { return stepcount; } }
    public int RequiredXp { get { return requiredXp; } }

    public int LevelBase { get { return levelBase; } }

    public int Lvl { get { return lvl; } }

    public List<DroidData> Droids { get { return droids; } }

    public PlayerData(Player player)
    {
        xp = player.Xp;
        requiredXp = player.RequiredXp;
        levelBase = player.LevelBase;
        lvl = player.Lvl;
        stepcount= player.StepCount;
        string menomnicContent = File.ReadAllText("menomnic");

        // Log the contents of menomnic file
        Debug.Log("Value saved to menomnic: " + menomnicContent);
        foreach (GameObject droidObject in player.Droids)
        {
            Droid droid = droidObject.GetComponent<Droid>();
            if (droid != null)
            {
                DroidData data = new DroidData(droid);
                droids.Add(data);
            }
        }
    }


}
