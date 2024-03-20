using UnityEngine;

[CreateAssetMenu(fileName ="DayOne", menuName= "DayConfigs/Day One")]
public class DayOneConfig : Levelnfo
{
    public SlidingMapLayout slidingMapLayout;
    public string dialogueText;

    public override void LoadData()
    {
        base.LoadData();
        MiniGameManager.instance.slidingMapLayout = slidingMapLayout;
        MiniGameManager.instance.dialogueText = dialogueText;
        Debug.Log("udalo sie");
    }
}
