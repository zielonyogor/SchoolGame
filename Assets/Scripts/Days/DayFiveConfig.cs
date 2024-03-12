using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DayFive", menuName = "DayConfigs/Day Five")]
public class DayFiveConfig : Levelnfo
{
    public int numberOfPuzzles;

    public override void LoadData()
    {
        base.LoadData();
        Debug.Log("udalo sie");
    }
}