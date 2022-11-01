using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private bool levelComplete;
    private int fuelHS;
    private TimeSpan timeHS;
    private TimeSpan targetTimeNumber;
    private int targetFuelNumber;

    public string levelToLoadName;

    public int currentSceneNumber;
    public int sceneCount;

    private Image Star1;
    private Image Star2;
    private Image Star3;

    public TextMeshProUGUI totalStarsText;

    public int levelsCompleted = 0;
    public int totalStars = 0;

    private int iPlus1;

    public Canvas touchControls;
    void Start()
    {
        touchControls.enabled = false;
        sceneCount = 21;
        currentSceneNumber = SceneManager.GetActiveScene().buildIndex;
        for(int i = 1; i < sceneCount; i++)
        {
            Star1 = GameObject.Find("L" + i + "Star1").GetComponent<Image>();
            Star2 = GameObject.Find("L" + i + "Star2").GetComponent<Image>();
            Star3 = GameObject.Find("L" + i + "Star3").GetComponent<Image>();
            levelToLoadName = "Level"+i;
            LoadPlayer();

            if (levelComplete == true)
            {
                levelsCompleted += 1;
                totalStars++;
                Star1.color = new Color32(255, 255, 255, 255);
            }
            else
            {
                Star1.color = new Color32(0, 0, 0, 255);
                iPlus1 = i + 1;
                if (GameObject.Find("Level"+ iPlus1) != null)
                {
                    GameObject.Find("Level" + iPlus1).GetComponent<Button>().interactable = false;
                }
            }
            if (timeHS < targetTimeNumber && timeHS != TimeSpan.Zero)
            {
                totalStars++;
                Star2.color = new Color32(255, 255, 255, 255);
            }
            if (fuelHS > targetFuelNumber && fuelHS != 0)
            {
                totalStars++;
                Star3.color = new Color32(255, 255, 255, 255);
            }
        }
        totalStarsText.text = "X " + totalStars+" / 60";
    }
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayerMenu();
        if(data != null)
        {
            levelComplete = data.levelComplete;
            fuelHS = data.fuelHS;
            timeHS = data.timeHS;
            targetTimeNumber = data.targetTimeNumber;
            targetFuelNumber = data.targetFuelNumber;
        }
        else
        {
            levelComplete = false;
            fuelHS = 0;
            timeHS = new TimeSpan(0);
        }
    }
}
