using UnityEngine;

[CreateAssetMenu(fileName = "Day_6", menuName = "DayConfigs/Day_6")]
public class DaySixConfig : Levelnfo
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