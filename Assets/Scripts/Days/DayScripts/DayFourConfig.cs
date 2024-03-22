using UnityEngine;

[CreateAssetMenu(fileName = "Day_4", menuName = "DayConfigs/Day_4")]
public class DayFourConfig : Levelnfo
{
    public int numberOfQuestions;

    public override void LoadData()
    {
        base.LoadData();
        MiniGameManager.instance.numberOfQuestions = numberOfQuestions;
    }
}