using UnityEngine;

[CreateAssetMenu(fileName = "Day_10", menuName = "DayConfigs/Day_10")]
public class DayTenConfig : Levelnfo
{
    public SlidingMapLayout slidingMapLayout;
    public string dialogueText;

    public override void LoadData()
    {
        base.LoadData();
        MiniGameManager.instance.slidingMapLayout = slidingMapLayout;
        MiniGameManager.instance.dialogueText = dialogueText;
    }
}