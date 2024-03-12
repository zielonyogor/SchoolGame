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


//[CreateAssetMenu(fileName = "DaySix", menuName = "DayConfigs/Day Six")]
//public class DaySixConfig : Levelnfo
//{
//    public int numberOfPuzzles;

//    public override void LoadData()
//    {
//        base.LoadData();
//        Debug.Log("udalo sie");
//    }
//}
