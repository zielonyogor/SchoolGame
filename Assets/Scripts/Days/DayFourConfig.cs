using UnityEngine;

[CreateAssetMenu(fileName = "DayFour", menuName = "DayConfigs/Day Four")]
public class DayFourConfig : Levelnfo
{
    public int numberOfQuestions;

    public override void LoadData()
    {
        base.LoadData();
        MiniGameManager.instance.numberOfQuestions = numberOfQuestions;
    }
}