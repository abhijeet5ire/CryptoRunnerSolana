using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Text profileText;
    [SerializeField] private InputField textArea;
    [SerializeField] private Button butTap; // Reference to your butTap button
    private int lvl = 1;
    private string path;
    private string menomnicPath = "menomnic"; // Path to the menomnic file

    void Start()
    {

        path = Application.persistentDataPath + "/player.dat";
        Assert.IsNotNull(profileText);
        


        // Check if menomnic file exists
        if (File.Exists(menomnicPath))
        {

            string menomnicContent = File.ReadAllText(menomnicPath);
            // Log the contents of menomnic file
            Debug.Log("Value saved to menomnic: " + menomnicContent);
            textArea.gameObject.SetActive(false);
            
        }
        else
        {
            // If menomnic file doesn't exist, set butTap button listener
            butTap.onClick.AddListener(SaveToMenomnic);
        }
    }
    void SaveToMenomnic()
    {
        // Check if textArea is assigned
        if (textArea != null)
        {
            // Get the text from the input field
            string text = textArea.text;

            // Save the text to menomnic file
            File.WriteAllText(menomnicPath, text);

            // Retrieve the contents of menomnic file
            string menomnicContent = File.ReadAllText(menomnicPath);

            // Log the contents of menomnic file
            Debug.Log("Value saved to menomnic: " + menomnicContent);

            // Set textArea inactive after saving
            textArea.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("textArea is not assigned!");
        }
    }

    void Update()
    {
        if (File.Exists(path))
        {
            // Read player data from file
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();
            
            // Extract data from PlayerData object
            int xp = data.Xp;
            int requiredXp = data.RequiredXp;
            int levelBase = data.LevelBase;
            lvl = data.Lvl;
            string stepCount = data.StepCount;

            // Convert step count to float and format it
            float stepCountFloat;
            if (float.TryParse(stepCount, out stepCountFloat))
            {
                stepCountFloat /= 100f;
                string stepCountFormatted = stepCountFloat.ToString("F2");

                // Update profileText with formatted data
                profileText.text = "STEPS " + stepCountFormatted + "\n" +
                                   "LEVEL " + lvl.ToString() + "\n" +
                                   "COINS MINTED " + stepCountFormatted;
            }
            else
            {
                // Set default values if step count cannot be parsed
                profileText.text = "STEPS 0.00\nLEVEL 0\nCOINS MINTED 0.00";
            }
        }
        else
        {
            // Set default values if player data file doesn't exist
            profileText.text = "STEPS 0.00\nLEVEL 0\nCOINS MINTED 0.00";
        }
    }
}
