using UnityEngine;

[CreateAssetMenu(fileName = "Day_9", menuName = "DayConfigs/Day_9")]
public class DayNineConfig : Levelnfo
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