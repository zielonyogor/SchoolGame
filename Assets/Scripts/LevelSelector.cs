using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public void LoadDay(Levelnfo newDay)
    {
        //It's just cause MiniGameManager goes missing after entering LevelSelector again
        MiniGameManager.instance.LoadDay(newDay);
    }

    public void LoadCutscene(string text)
    {
        MiniGameManager.instance.LoadCutscene(text);
    }
}
