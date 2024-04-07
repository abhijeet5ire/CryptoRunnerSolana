using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
public class Droid : MonoBehaviour
{

    [SerializeField] private float spawnRate = .10f;
    [SerializeField] private float catchRate = .10f;
    [SerializeField] private int attack = 0;
    [SerializeField] private int defense = 0;
    [SerializeField] private int hp = 10;
    [SerializeField] private AudioClip crySound;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Assert.IsNotNull(audioSource);
        Assert.IsNotNull(crySound);
    }
    public float SpawnRate
    {
        get { return spawnRate; }
    }

    public float CatchRate
    {
        get { return catchRate; }
    }

    public int Attack
    {
        get { return Attack; }
    }

    public int Defense
    {
        get { return Defense; }
    }

    public int Hp
    {
        get { return hp; }
    }

    public AudioClip CrySound
    {
        get { return crySound; }
    }
    private void OnMouseDown()
    {
        PocketDroidsSceneManager[] managers = FindObjectsOfType<PocketDroidsSceneManager>();
        audioSource.PlayOneShot(crySound);
        foreach (PocketDroidsSceneManager pocket in managers)
        {
            if (pocket.gameObject.activeSelf)
            {
                pocket.droidTapped(this.gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        PocketDroidsSceneManager[] managers = FindObjectsOfType<PocketDroidsSceneManager>();

        foreach (PocketDroidsSceneManager pocket in managers)
        {
            if (pocket.gameObject.activeSelf)
            {
                pocket.DroidCollision(this.gameObject, collision);
            }
        }
    }
}
