using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager instance { get; private set; }

    private int currentGameIndex = 0;

    public string cutsceneText;

    public List<string> miniGames;
    public float time = 0;

    [Header("MiniGame variables")]
    public int numberOfPuzzles;
    public int numberOfMeds;
    public string dialogueText;
    public int numberOfQuestions;
    public int numberOfBooks;
    public int bookSpeed;
    public int tableSize;
    public SlidingMapLayout slidingMapLayout;

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

    public void LoadDay(Levelnfo newDay)
    {
        newDay.LoadData();
        Debug.Log(miniGames.Count);
        SceneManager.LoadScene(miniGames[currentGameIndex]);
        //Should add (maybe here) handling Anxiety Game
    }

    public void LoadCutscene(string text)
    {
        cutsceneText = text;
        SceneManager.LoadScene("Cutscene");
    }

    public void NextLevel()
    {
        currentGameIndex++;
        if (currentGameIndex >= miniGames.Count)
        {
            currentGameIndex = 0;
            SceneManager.LoadScene("LevelMenu");
        }
        else
        {
            SceneManager.LoadScene(miniGames[currentGameIndex]);
        }
    }

    public void HandleGameLoss()
    {
        //currentGameIndex++;
        if (currentGameIndex >= miniGames.Count)
        {
            currentGameIndex = 0;
            SceneManager.LoadScene("LevelMenu");
        }
        else
        {
            SceneManager.LoadScene(miniGames[currentGameIndex]);
        }
    }
}
