using UnityEngine;

[CreateAssetMenu(fileName = "DayTwo", menuName = "DayConfigs/Day Two")]
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