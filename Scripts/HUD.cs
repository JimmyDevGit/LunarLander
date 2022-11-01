using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Slider fuelSlider;
    public Slider healthSlider;

    public void SetMaxFuel(int fuel)
    {
        fuelSlider.maxValue = fuel;
        fuelSlider.value = fuel;
    }
    public void SetFuel(int fuel)
    {
        fuelSlider.value = fuel;
    }
    public void SetMaxHealth(float health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }
    public void SetHealth(float health)
    {
        healthSlider.value = health;
    }
}
