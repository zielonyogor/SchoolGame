using UnityEngine;

[CreateAssetMenu(fileName = "Day_2", menuName = "DayConfigs/Day_2")]
public class DayTwoConfig : Levelnfo
{
    public int numberOfQuestions;

    public override void LoadData()
    {
        base.LoadData();
        MiniGameManager.instance.numberOfQuestions = numberOfQuestions;
        Debug.Log("udalo sie");
    }
}
