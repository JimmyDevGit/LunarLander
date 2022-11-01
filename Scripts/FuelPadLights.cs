using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FuelPadLights : MonoBehaviour
{
    public Sprite lightsOn;
    public Sprite lightsOff;
    public Sprite depletedLights;
    public Light2D light1;
    public Light2D light2;
    public FuelPad fuelPad;
    private void Update()
    {
        if (fuelPad.fuelInTank <= 0)
        {
            DepletedLights();
        }
    }
    private void DepletedLights()
    {
        GetComponent<SpriteRenderer>().sprite = depletedLights;
        light1.color = new Color32(255, 0, 0, 255);
        light2.color = new Color32(255, 0, 0, 255);
    }
}
