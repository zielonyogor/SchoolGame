using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    //It's just cause MiniGameManager goes missing after entering LevelSelector again
    public void LoadDay(Levelnfo newDay)
    {
        
        MiniGameManager.instance.LoadDay(newDay);
    }

    public void LoadCutscene(string text)
    {
        MiniGameManager.instance.LoadCutscene(text);
    }

    public void LoadPhoneEvent()
    {
        MiniGameManager.instance.LoadPhoneEvent();
    }
}
