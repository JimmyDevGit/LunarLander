using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class FuelPad : MonoBehaviour
{
    public CapsuleCollider2D LegLeft;
    public CapsuleCollider2D LegRight;
    private bool landedLeft;
    private bool landedRight;
    public int fuelInTank = 250;
    private int currentFuel;
    private bool playSoundOncePerLanding;
    private bool playFillSoundOncePerLanding;
    public TextMeshPro fuelInTankText;
    private int fuelInTankColor;
    private int fuelInTankHalved;
    public Image fuelFill;
    private Color fuelColor = new Color32(19,77,143,255);
    private bool fillDelay = false;
    private bool fillDelayCalled = false;
    public Light2D fuelInTankLight;
    public PlayerController playerController;
    private void Start()
    {
        if(gameObject.tag == "FuelPadLarge")
        {
            fuelInTank = 500;
        }
        fuelInTankText = gameObject.GetComponentInChildren<TextMeshPro>();
        fuelFill = GameObject.FindGameObjectWithTag("FuelFill").GetComponent<Image>();
    }
    private void FixedUpdate()
    {
        fuelInTankText.text = fuelInTank.ToString();
        if (landedRight == true && landedLeft == true)
        {
            currentFuel = playerController.currentFuel;
            if(fuelInTank > 0 && currentFuel < 1000)
            {
                if(fillDelayCalled == false)
                {
                    StartCoroutine(FillDelay());
                }
                if(fillDelay == true)
                {
                    playerController.GetFuel();
                    fuelInTank--;
                    if (fuelInTank > 0 && currentFuel < 1000)
                    {
                        playerController.GetFuel();
                        fuelInTank--;
                    }
                    if (fuelInTank > 0 && currentFuel < 1000)
                    {
                        playerController.GetFuel();
                        fuelInTank--;
                    }
                    if (fuelInTank > 0 && currentFuel < 1000)
                    {
                        playerController.GetFuel();
                        fuelInTank--;
                    }
                    if (fuelInTank > 0 && currentFuel < 1000)
                    {
                        playerController.GetFuel();
                        fuelInTank--;
                    }
                    if (fuelInTankColor > 77)
                    {
                        fuelFill.color = new Color32(0, (byte)fuelInTankColor, 255, 255);
                    }
                    else
                    {
                        fuelFill.color = new Color32(0, 77, 255, 255);

                    }
                    if (gameObject.tag == "FuelPadLarge")
                    {
                        fuelInTankHalved = fuelInTank / 2;
                        fuelInTankColor = 250 - fuelInTankHalved;
                        fuelInTankText.color = new Color32((byte)fuelInTankColor, (byte)fuelInTankHalved, 0, 255);
                        fuelInTankLight.color = new Color32((byte)fuelInTankColor, (byte)fuelInTankHalved, 0, 255);
                    }
                    if (gameObject.tag == "FuelPad")
                    {
                        fuelInTankColor = 250 - fuelInTank;
                        fuelInTankText.color = new Color32((byte)fuelInTankColor, (byte)fuelInTank, 0, 255);
                        fuelInTankLight.color = new Color32((byte)fuelInTankColor, (byte)fuelInTank, 0, 255);
                    }
                    if (playFillSoundOncePerLanding == true)
                    {
                        AudioManager.instance.Play("GasFill");
                        playFillSoundOncePerLanding = false;
                    }
                }
            }
            if(fuelInTank <= 0)
            {
                if (playSoundOncePerLanding == true)
                {
                    AudioManager.instance.Stop("GasFill");
                    AudioManager.instance.Play("EmptyTankSound");
                    playSoundOncePerLanding = false;
                    fuelFill.color = fuelColor;
                }
            }
            if (currentFuel >= 1000)
            {
                if (playSoundOncePerLanding == true)
                {
                    AudioManager.instance.Stop("GasFill");
                    AudioManager.instance.Play("TankFull");
                    playSoundOncePerLanding = false;
                    fuelFill.color = fuelColor;
                }
            }
        }
        if(landedLeft == false || landedRight == false)
        {
            playSoundOncePerLanding = true;
            playFillSoundOncePerLanding = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == LegLeft)
        {
                landedLeft = true;
        }
        if (collision.collider == LegRight)
        {
                landedRight = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        fuelFill.color = fuelColor;
        fillDelay = false;
        StopCoroutine(FillDelay());
        fillDelayCalled = false;
        AudioManager.instance.Stop("GasFill");
        if (collision.collider == LegLeft)
        {
            landedLeft = false;
        }
        if (collision.collider == LegRight)
        {
            landedRight = false;
        }
    }
    IEnumerator FillDelay()
    {
        fillDelayCalled = true;
        yield return new WaitForSeconds(.2f);
        fillDelay = true;
    }
}
