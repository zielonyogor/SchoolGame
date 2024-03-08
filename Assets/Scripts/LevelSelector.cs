using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    //Wouldn't it be better to already have here MinigameManager???
    //Need to make instance of MiniGameManager right at the start of the game 
    public void LoadScene(DayData day)
    {
        MiniGameManager.instance.LoadDay(day);
    }

    public void LoadCutscene(string text)
    {
        MiniGameManager.instance.LoadCutscene(text);
    }
}
