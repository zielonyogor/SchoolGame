using UnityEngine;

[CreateAssetMenu(fileName = "Day_6", menuName = "DayConfigs/Day_6")]
public class DaySixConfig : Levelnfo
{
    public int numberOfMeds;

    public override void LoadData()
    {
        base.LoadData();
        MiniGameManager.instance.numberOfMeds = numberOfMeds;
    }
}