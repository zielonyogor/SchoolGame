using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenu, calendar, howToPlay, credits;
    public void OpenLevelSelector()
    {
        LeanTween.moveLocalX(mainMenu, -1800.0f, 3f);
    }

    public void OpenHowToPlay()
    {
    }

    public void OpenCredits()
    {
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
