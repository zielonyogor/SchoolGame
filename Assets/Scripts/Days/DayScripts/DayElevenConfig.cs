using UnityEngine;

[CreateAssetMenu(fileName = "Day_11", menuName = "DayConfigs/Day_11")]
public class DayElevenConfig : Levelnfo
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