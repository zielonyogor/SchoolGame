using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="DayOne", menuName= "DayConfigs/Day One")]
public class DayOneConfig : Levelnfo
{
    public int numberOfPuzzles;

    public override void LoadData()
    {
        base.LoadData();
        MiniGameManager.instance.numberOfPuzzles = numberOfPuzzles;
        Debug.Log("udalo sie");
    }
}


//[CreateAssetMenu(fileName = "DayFour", menuName = "DayConfigs/Day Four")]
//public class DayFourConfig : Levelnfo
//{
//    public int numberOfPuzzles;

//    public override void LoadData()
//    {
//        base.LoadData();
//        Debug.Log("udalo sie");
//    }
//}

//[CreateAssetMenu(fileName = "DayFive", menuName = "DayConfigs/Day Five")]
//public class DayFiveConfig : Levelnfo
//{
//    public int numberOfPuzzles;

//    public override void LoadData()
//    {
//        base.LoadData();
//        Debug.Log("udalo sie");
//    }
//}

//[CreateAssetMenu(fileName = "DaySix", menuName = "DayConfigs/Day Six")]
//public class DaySixConfig : Levelnfo
//{
//    public int numberOfPuzzles;

//    public override void LoadData()
//    {
//        base.LoadData();
//        Debug.Log("udalo sie");
//    }
//}
