using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenu, calendar, howToPlay, credits;

    [SerializeField] Animator notebookAnimator;
    public void OpenLevelSelector()
    {
        notebookAnimator.SetTrigger("showLevelSelector");
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
