using Mapbox.Json;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Text xpText;
    [SerializeField] private Text levelText;
    
    [SerializeField] private GameObject menu;
    [SerializeField] private AudioClip menuButtonSound;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        Assert.IsNotNull(audioSource);
        Assert.IsNotNull(xpText);
        Assert.IsNotNull(levelText);
        
        Assert.IsNotNull(menu);
        Assert.IsNotNull(menuButtonSound);
    }
    private void Update()
    {
        updateLevel();
        
        updateXp();
    }

    public void updateLevel()
    {
        levelText.text = GameManager.Instance.CurrentPlayer.Lvl.ToString();
    }
    public void updateXp()
    {
        xpText.text = GameManager.Instance.CurrentPlayer.Xp + " / " + GameManager.Instance.CurrentPlayer.RequiredXp;
    }
    public void MenuButtonClicked()
    {
        audioSource.PlayOneShot(menuButtonSound);
        toggleMenu();
    }
    private void toggleMenu()
    {
        menu.SetActive(!menu.activeSelf);
    }
}
