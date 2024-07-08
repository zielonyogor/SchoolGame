using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager instance { get; private set; }

    private int currentGameIndex = 0;

    public string cutsceneText;

    public List<string> miniGames;
    public float time = 0;

    [Header("Timing Game Object")]
    [SerializeField] private GameObject timingObject;

    [Header("MiniGame variables")]
    public int numberOfPuzzles;
    public int numberOfMeds;
    public string dialogueText;
    public int numberOfQuestions;
    public int numberOfBooks;
    public int bookSpeed;
    public int tableSize;
    public SlidingMapLayout slidingMapLayout;

    [Header("Days variables")]
    public bool isPlaying = false;
    public GameData gameData = new GameData();



    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        gameData = SaveSystem.LoadGameData();
    }

    public void LoadDay(Levelnfo newDay)
    {
        newDay.LoadData();
        SceneManager.LoadScene(miniGames[currentGameIndex]);
        //Should add (maybe here) handling Anxiety Game
    }

    public void LoadCutscene(string text)
    {
        cutsceneText = text;
        if (gameData.day == Constants.lastDay)
        {
            
            if (gameData.errors == 0)
            {
                cutsceneText = "WOW I can't believe it!\nPeople here like me.\n" +
                    "I've made some really cool friends.\nThere's really no cons.\n" +
                    "I'LL STAY";
            }
            else cutsceneText = "Eh...\nI guess it's not that bad.\nSome people don't like me that much." +
                    "\nSome think I'm cringe.\nBut I don't really want to change school again..." +
                    "\nI decided.\nI'LL STAY.";
        }
        SceneManager.LoadScene("Cutscene");
    }

    //probably will imiplement some sort of remembering what scenarios were done
    public void LoadPhoneEvent()
    {
        SceneManager.LoadScene("PhoneScene");
    }

    public void NextLevel()
    {
        currentGameIndex++;
        gameData.consecutiveErrors = 0;
        if (currentGameIndex >= miniGames.Count)
        {
            isPlaying = true;
            currentGameIndex = 0;
            gameData.day += 1;
            SaveSystem.SaveGame();
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
        gameData.errors += 1;
        gameData.consecutiveErrors += 1;
        if (gameData.consecutiveErrors == Constants.maxConsecutiveErrors)
        {
            SaveSystem.DeleteSaveFile();
            isPlaying = false;
            LoadCutscene("AAAAAAAAAA\nI hate it here!\nI've made a fool out of myself\n" +
                "I can't be here any longer!\nI NEED to change school again!");
        }
        else
        {
            NextLevel();
        }
    }

    public void ExitCutscene()
    {
        if (gameData.day >= Constants.lastDay || gameData.consecutiveErrors >= Constants.maxConsecutiveErrors)
        {
            SaveSystem.DeleteSaveFile();
            isPlaying = false;
        }
        else if (gameData.day != 1)
        {
            gameData.day += 1;
            gameData.consecutiveErrors = 0;
        }
        SaveSystem.SaveGame();
        SceneManager.LoadScene("LevelMenu"); 
    }


    public void AddError()
    {
        gameData.consecutiveErrors++;
        gameData.errors++;
        if (gameData.consecutiveErrors == Constants.maxConsecutiveErrors)
        {
            SaveSystem.DeleteSaveFile();
            isPlaying = false;
            LoadCutscene("AAAAAAAAAA\nI hate it here!\nI've made a fool out of myself\n" +
                "I can't be here any longer!\nI NEED to change school again!");
        }

    }
}
