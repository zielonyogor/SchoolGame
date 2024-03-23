using UnityEngine;

[CreateAssetMenu(fileName = "Day_6", menuName = "DayConfigs/Day_6")]
public class DaySixConfig : Levelnfo
{
    public int numberOfPuzzles;

    public override void LoadData()
    {
        base.LoadData();
        MiniGameManager.instance.numberOfPuzzles = numberOfPuzzles;
    }
}