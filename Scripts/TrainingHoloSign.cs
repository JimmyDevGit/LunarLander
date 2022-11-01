using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;

public class TrainingHoloSign : MonoBehaviour
{
    public SpriteRenderer childrenSprites;
    public Sprite red;
    public Sprite green;
    private GameObject ship;
    public Transform lowPoint;
    public Transform highPoint;
    public Light2D lights;
    public GameObject takeOff;
    public GameObject landing;
    public GameObject redArrow;
    public TextMeshProUGUI countdownTimerText;
    public TextMeshProUGUI countdownTimer;
    private float countdownTime;
    private float newCountdownTime;
    private int newCountdownTimeInt;
    private bool timerFinished;
    // Start is called before the first frame update
    void Start()
    {
        countdownTime = 10;
        childrenSprites = gameObject.GetComponentInChildren<SpriteRenderer>();
        lights = gameObject.GetComponentInChildren<Light2D>();
        ship = GameObject.Find("SpaceShip");
    }

    // Update is called once per frame
    void Update()
    {
        if(ship != null)
        {
            if (ship.transform.position.y > lowPoint.position.y && ship.transform.position.y < highPoint.position.y || timerFinished == true)
            {
                if (timerFinished == true)
                {
                    takeOff.SetActive(false);
                    landing.SetActive(true);
                    redArrow.SetActive(true);
                    countdownTimer.text = "";
                    countdownTimerText.text = "Return To Base";
                }
                if (countdownTime > 0)
                {
                    newCountdownTime = countdownTime -= Time.deltaTime;
                    newCountdownTimeInt = (int)newCountdownTime;
                    timerFinished = false;
                    countdownTimer.text = newCountdownTimeInt.ToString();
                }
                else if (countdownTime <= 0)
                {
                    timerFinished = true;
                }

                childrenSprites.sprite = green;
                lights.color = new Color(0, 1, 0, 1);
            }
            else
            {
                childrenSprites.sprite = red;
                countdownTime = 10;
                countdownTimer.text = countdownTime.ToString();
                lights.color = new Color(1, 0, 0, 1);
            }
        }
    }
}
