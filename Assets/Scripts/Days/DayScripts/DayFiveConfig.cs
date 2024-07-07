using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Day_5", menuName = "DayConfigs/Day_5")]
public class DayFiveConfig : Levelnfo
{
    public int numberOfMeds;

    public override void LoadData()
    {
        base.LoadData();
        MiniGameManager.instance.numberOfMeds = numberOfMeds;
    }
}