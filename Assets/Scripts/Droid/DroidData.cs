using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class DroidData 
{
    private float spawnRate;
    private float catchRate;
    private int attack;
    private int defense;
    private int hp;
    private string crySound;

    public float SpawnRate
    { get { return spawnRate; } }
    public float CatchRate
    { get { return catchRate; } }
    public int Attack
    { get { return attack; } }
    public int Defense
    { get { return defense; } }
    public int Hp
    { get { return hp; } }
    public string CrySound
    { get { return crySound; } }

    public DroidData(Droid droid)
    {
        spawnRate = droid.SpawnRate;
        catchRate = droid.CatchRate;
        attack = droid.Attack;
        defense = droid.Defense;
        hp = droid.Hp;
        AudioClip clip;
        crySound = droid.CrySound.name;
    }
}
