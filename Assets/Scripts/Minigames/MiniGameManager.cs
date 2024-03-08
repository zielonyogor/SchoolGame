using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager instance { get; private set; }

    public DayData dayInfo;
    private int currentGameIndex = 0;

    public string cutsceneText;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void LoadDay(DayData newDay)
    {
        dayInfo = newDay;
        SceneManager.LoadScene(dayInfo.miniGames[currentGameIndex]);
    }

    public void LoadCutscene(string text)
    {
        cutsceneText = text;
        SceneManager.LoadScene("Cutscene");
    }

    public void NextLevel()
    {
        currentGameIndex++;
        if (currentGameIndex >= dayInfo.miniGames.Count)
        {
            currentGameIndex = 0;
            SceneManager.LoadScene("LevelMenu");
        }
        else
        {
            SceneManager.LoadScene(dayInfo.miniGames[currentGameIndex]);
        }
    }
}
