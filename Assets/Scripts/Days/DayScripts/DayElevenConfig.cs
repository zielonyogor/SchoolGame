using UnityEngine;

[CreateAssetMenu(fileName = "Day_11", menuName = "DayConfigs/Day_11")]
public class DayElevenConfig : Levelnfo
{
    public int numberOfPuzzles;
    public string dialogueText;

    public override void LoadData()
    {
        base.LoadData();
        MiniGameManager.instance.numberOfPuzzles = numberOfPuzzles;
        MiniGameManager.instance.dialogueText = dialogueText;
    }
}