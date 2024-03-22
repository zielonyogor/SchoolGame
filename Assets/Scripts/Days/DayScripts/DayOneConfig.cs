using UnityEngine;

[CreateAssetMenu(fileName ="Day_1", menuName= "DayConfigs/Day_1")]
public class DayOneConfig : Levelnfo
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
