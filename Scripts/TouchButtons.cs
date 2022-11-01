using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchButtons : MonoBehaviour
{
    public static TouchButtons instance;

    private bool thrusting;
    private bool rotateLeft;
    private bool rotateRight;
    private bool thrustingD;
    private bool rotateLeftD;
    private bool rotateRightD;
    private bool timerStarted;
    public Thruster thrusterScript;
    public PlayerController playerController;
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(GameObject.Find("SpaceShip") != null)
        {
            thrusterScript = GameObject.Find("Thruster").GetComponent<Thruster>();
            playerController = GameObject.Find("SpaceShip").GetComponent<PlayerController>();
            timerStarted = false;
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void FixedUpdate()
    {
        if(thrusting == true)
        {
            playerController.Thrust();
            playerController.ThrustSound();
        }
        if (rotateLeft == true)
        {
            playerController.RotateLeft();
        }
        if (rotateRight == true)
        {
            playerController.RotateRight();
        }
    }
    private void Update()
    {
        if (thrustingD == true)
        {
            playerController.ThrustDown();
            thrusterScript.ThrustRay();
        }
        if (rotateLeftD == true)
        {
            playerController.RotateLeftDown();
        }
        if (rotateRightD == true)
        {
            playerController.RotateRightDown();
        }
    }
    public void Thrust()
    {
        thrusting = true;
        if(timerStarted == false)
        {
            StartGameManThrustTimer();
        }
    }
    public void ThrustDown()
    {
        thrustingD = true;
    }
    public void ThrustUp()
    {
        thrusting = false;
        thrustingD = false;
        playerController.ThrustUp();
    }
    public void RotateLeft()
    {
        rotateLeft = true;
    }
    public void RotateLeftDown()
    {
        rotateLeftD = true;
    }
    public void RotateLeftUp()
    {
        rotateLeft = false;
        rotateLeftD = false;
        playerController.RotateLeftUp();
    }
    public void RotateRight()
    {
        rotateRight = true;
        
    }
    public void RotateRightDown()
    {
        rotateRightD = true;
    }
    public void RotateRightUp()
    {
        rotateRight = false;
        rotateRightD = false;
        playerController.RotateRightUp();
    }
    private void StartGameManThrustTimer()
    {
        GameObject.Find("GameManager").GetComponent<GameMan>().TouchStartTimer();
        timerStarted = true;
    }
}
