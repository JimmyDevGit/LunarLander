using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{

    public bool levelComplete;
    public int fuel;
    public TimeSpan time;
    public int fuelHS;
    public TimeSpan timeHS;
    public TimeSpan targetTimeNumber;
    public int targetFuelNumber;

    public PlayerData(GameMan player)
    {
        levelComplete = player.levelComplete;
        fuel = player.fuel;
        time = player.time;
        fuelHS = player.fuelHS;
        timeHS = player.timeHS;
        targetTimeNumber = player.targetTimeNumber;
        targetFuelNumber = player.targetFuelNumber;
    }
}
