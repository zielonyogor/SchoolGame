using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DayTwo", menuName = "DayConfigs/Day Two")]
public class DayTwoConfig : Levelnfo
{
    public int numberOfMeds;
    public string dialogueText;

    public override void LoadData()
    {
        base.LoadData();
        MiniGameManager.instance.numberOfMeds = numberOfMeds;
        MiniGameManager.instance.dialogueText = dialogueText;
        Debug.Log("udalo sie");
    }
}
