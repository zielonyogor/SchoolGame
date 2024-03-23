using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenu, calendar, howToPlay, credits;

    [SerializeField] Animator notebookAnimator;

    private void Start()
    {
        if (MiniGameManager.instance.isPlaying)
        {
            notebookAnimator.SetTrigger("isPlaying");
        }
    }

    public void ExitGame()
    {
        SaveSystem.SaveGame();
        Application.Quit();
    }
}
