using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameMan : MonoBehaviour
{
    public TextMeshProUGUI levelTimer;
    private float elapsedTime = 0f;
    private TimeSpan timePlaying;
    public bool TimerRunning = false;

    private Scene currentScene;
    private int currentSceneNumber;
    private int nextSceneNumber;
    public string currentSceneName;

    public Canvas HUD;
    public Canvas PauseMenu;
    public GameObject thruster;
    public bool isPaused = false;

    public bool levelComplete = false;
    public int fuel;
    public TimeSpan time;
    public int fuelHS;
    public TimeSpan timeHS;

    public TextMeshProUGUI yoursTime;
    public TextMeshProUGUI yoursEconomy;
    public TextMeshProUGUI yoursTimeHS;
    public TextMeshProUGUI yoursEconomyHS;
    public TextMeshProUGUI targetTime;
    public TextMeshProUGUI targetEconomy;

    public TimeSpan targetTimeNumber;
    public int targetTimeInt;
    public int targetFuelNumber;

    public Image star1;
    public Image star2;
    public Image star3;
    private bool targetTimeBeat = false;
    private bool targetFuelBeat = false;
    public Button endScreenNextLevelButton;
    public Button endScreenRestart;
    public Button endScreenMenu;
    public Animator animStar1;
    public Animator animStar2;
    public Animator animStar3;
    public Image greenTick2;
    public Image greenTick3;
    public GameObject player;
    public PlayerController playerController;
    public Canvas touchControls;
    public bool touchInput;
    private bool timerWasRunning = false;

    private void Start()
    {
        touchControls = GameObject.Find("TouchControllCanvas").GetComponent<Canvas>();
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            touchInput = true;
        }
        else
        {
            touchInput = false;
        }
        EnableTouchControls();
        currentScene = SceneManager.GetActiveScene();
        currentSceneNumber = SceneManager.GetActiveScene().buildIndex;
        nextSceneNumber = currentSceneNumber += 1;
        currentSceneName = currentScene.name;
        //To not get null reference exception when on training Levels (scenes 21,22)
        if(currentSceneNumber < 22)
        {
            targetTimeNumber = new TimeSpan(0, targetTimeInt, 0);
            //time = new TimeSpan(0, time, 0);
            yoursTime.enabled = false;
            yoursEconomy.enabled = false;
            yoursTimeHS.enabled = false;
            yoursEconomyHS.enabled = false;
        }
        endScreenNextLevelButton.gameObject.SetActive(false);
        endScreenRestart.gameObject.SetActive(false);
        endScreenMenu.gameObject.SetActive(false);
        if (currentSceneNumber == 23)
        {
            Pause();
            GameObject.Find("StartScreen").GetComponent<Canvas>().enabled = true;
            GameObject.Find("HUD").GetComponent<Canvas>().enabled = false;
        }
    }
    private void Update()
    {
        if(player != null)
        {
            if (currentSceneNumber >= 22 && playerController.currentFuel == 0)
            {
                Pause();
                GameObject.Find("ProblemScreen").GetComponent<Canvas>().enabled = true;
            }
        }
        if (isPaused == false);
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                TimerRunning = true;
                timerWasRunning = true;
            }
        }
        if (TimerRunning == true)
        {
            StartCoroutine(UpdateTimer());
        }


        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused == true)
            {
                UnPause();
            }
            else if(isPaused == false)
            {
                Pause();
            }
        }
    }
    private IEnumerator UpdateTimer()
    {
        elapsedTime += Time.deltaTime;
        timePlaying = TimeSpan.FromSeconds(elapsedTime);
        string timePlayingStr = timePlaying.ToString("mm':'ss':'ff");
        levelTimer.text = timePlayingStr;
        time = TimeSpan.FromSeconds(elapsedTime * 60);
        yield return null;
    }
    //used only for training level2
    public void PlayThisLevel()
    {
        UnPause();
        GameObject.Find("StartScreen").GetComponent<Canvas>().enabled = false;
        GameObject.Find("TrainingCanvas").GetComponent<Canvas>().enabled = true;
        GameObject.Find("HUD").GetComponent<Canvas>().enabled = true;
    }
    public void RestartLevel()
    {
        UnPause();
        SceneManager.LoadScene(currentSceneName);
    }
    public void LoadMenu()
    {
        UnPause();
        DisableTouchControls();
        SceneManager.LoadScene("Menu");
    }
    public void LoadNextLevel()
    {
        if (nextSceneNumber < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneNumber);
        }
        else
        {
            //No Next Scene
        }
    }
    public void Pause()
    {
        AudioManager.instance.Stop("LowFuelAlarm");
        DisableTouchControls();
        isPaused = true;
        if(player != null)
        {
            player.GetComponent<PlayerController>().PausePlayer();
        }
        PauseMenu.enabled = true;
        HUD.enabled = false;
        if (player != null)
        {
            thruster.SetActive(false);
        }
        TimerRunning = false;
    }
    public void UnPause()
    {
        EnableTouchControls();
        isPaused = false;
        if (player != null)
        {
            playerController.UnPausePlayer();
        }
        PauseMenu.enabled = false;
        HUD.enabled = true;
        if (player != null)
        {
            thruster.SetActive(true);
        }
        //Heres ya fugin problem mate
        if (timerWasRunning == true)
        {
            TimerRunning = true;
        }
    }
    public void TouchStartTimer()
    {
        if(isPaused == false)
        {
            TimerRunning = true;
            timerWasRunning = true;
        }
    }
    public void LevelComplete()
    {
        Pause();
        if (currentSceneNumber < 22)
        {
            CheckHS();
            fuel = playerController.currentFuel;
            levelComplete = true;
            StartCoroutine(StarReveal());

            //Update fuelHS
            if (fuel > fuelHS)
            {
                fuelHS = fuel;
            }
            //Update timeHS
            if (time < timeHS || timeHS == TimeSpan.Zero)
            {
                timeHS = time;
            }
            //Check fuel VS target fuel
            if (fuel > targetFuelNumber)
            {
                yoursEconomy.color = new Color32(0, 255, 0, 255);
            }
            else
            {
                yoursEconomy.color = new Color32(255, 0, 0, 255);
            }
            //Check fuelHS VS target fuel
            if (fuelHS > targetFuelNumber)
            {
                targetFuelBeat = true;
                //Show Star 2
                yoursEconomyHS.color = new Color32(0, 255, 0, 255);
            }
            else
            {
                yoursEconomyHS.color = new Color32(255, 0, 0, 255);
            }
            //Check time VS target time
            if (time < targetTimeNumber)
            {
                yoursTime.color = new Color32(0, 255, 0, 255);
            }
            else
            {
                yoursTime.color = new Color32(255, 0, 0, 255);
            }
            //Check timeHS VS target time
            if (timeHS < targetTimeNumber)
            {
                targetTimeBeat = true;
                //Show Star 3
                yoursTimeHS.color = new Color32(0, 255, 0, 255);
            }
            else
            {
                yoursTimeHS.color = new Color32(255, 0, 0, 255);
            }
            SavePlayer();
            LoadPlayer();
        }
        else if (currentSceneNumber >= 22)
        {
            endScreenRestart.gameObject.SetActive(true);
            endScreenMenu.gameObject.SetActive(true);
            if (SceneManager.sceneCountInBuildSettings -1 == currentSceneNumber)
            {
                endScreenNextLevelButton.gameObject.SetActive(true);
            }
        }
        GameObject.Find("EndScreen").GetComponent<Canvas>().enabled = true;
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        levelComplete = data.levelComplete;
        fuel = data.fuel;
        time = data.time;
        yoursTime.text = time.ToString("hh':'mm':'ss");
        yoursEconomy.text = fuel.ToString();
        yoursTimeHS.text = timeHS.ToString("hh':'mm':'ss");
        yoursEconomyHS.text = fuelHS.ToString();
    }
    public void CheckHS()
    {
        if (SaveSystem.LoadPlayer() != null)
        {
            PlayerData data = SaveSystem.LoadPlayer();
            fuelHS = data.fuelHS;
            timeHS = data.timeHS;
        }
        targetEconomy.text = targetFuelNumber.ToString();
        targetTime.text = targetTimeNumber.ToString();
    }

    public IEnumerator BlowUp()
    {
        yield return new WaitForSeconds(2);
        RestartLevel();
    }

    IEnumerator StarReveal()
    {
        yield return new WaitForSeconds(1);
        star1.color = new Color32(255, 255, 255, 255);
        animStar1.SetBool("StarExpand", true);
        greenTick2.enabled = true;
        greenTick3.enabled = true;
        AudioManager.instance.Play("StarSound");
        yield return new WaitForSeconds(.75f);
        yoursTime.enabled = true;
        yoursTimeHS.enabled = true;
        if (targetTimeBeat == true)
        {
            star2.color = new Color32(255, 255, 255, 255);
            animStar2.SetBool("StarExpand", true);
            AudioManager.instance.Play("StarSound2");
        }
        yield return new WaitForSeconds(.75f);
        yoursEconomy.enabled = true;
        yoursEconomyHS.enabled = true;
        if (targetFuelBeat == true)
        {
            star3.color = new Color32(255, 255, 255, 255);
            animStar3.SetBool("StarExpand", true);
            AudioManager.instance.Play("StarSound3");
        }
        yield return new WaitForSeconds(.75f);
        endScreenNextLevelButton.gameObject.SetActive(true);
        endScreenRestart.gameObject.SetActive(true);
        endScreenMenu.gameObject.SetActive(true);
    }
    public void EnableTouchControls()
    {
        if(touchInput == true)
        {
            touchControls.enabled = true;
        }
    }
    public void DisableTouchControls()
    {
        touchControls.enabled = false;
    }
}
