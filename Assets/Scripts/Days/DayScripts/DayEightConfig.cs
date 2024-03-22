using UnityEngine;

[CreateAssetMenu(fileName = "Day_8", menuName = "DayConfigs/Day_8")]
public class DayEightConfig : Levelnfo
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