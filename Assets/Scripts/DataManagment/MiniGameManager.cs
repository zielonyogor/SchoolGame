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


    private void Start()
    {
        Screen.SetResolution(1200, 720, false);
    }

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
                cutsceneText = "WOW I can't believe it!_People here like me._" +
                    "I've made some really cool friends._There's really no cons._" +
                    "I'LL STAY";
            }
            else cutsceneText = "Eh..._I guess it's not that bad._Some people don't like me that much." +
                    "_Some think I'm cringe._But I don't really want to change school again..." +
                    "_I decided._I'LL STAY.";
        }
        SceneManager.LoadScene("Cutscene");
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
            SceneManager.LoadScene("LevelMenu");
        }
        else
        {
            SceneManager.LoadScene(miniGames[currentGameIndex]);
        }
    }

    public void HandleGameLoss()
    {
        currentGameIndex++;
        gameData.errors += 1;
        gameData.consecutiveErrors += 1;
        if (gameData.consecutiveErrors == 2)
        {
            SaveSystem.DeleteSaveFile();
            isPlaying = false;
            LoadCutscene("AAAAAAAAAA_I hate it here!_I've made a fool out of myself_" +
                "I can't be here any longer!_I want to change school again!");
        }
        else
        {
            if (currentGameIndex >= miniGames.Count)
            {
                gameData.day += 1;
                isPlaying = true;
                currentGameIndex = 0;
                SceneManager.LoadScene("LevelMenu");
            }
            else
            {
                SceneManager.LoadScene(miniGames[currentGameIndex]);
            }
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
        SceneManager.LoadScene("LevelMenu"); 
    }


    private IEnumerator SpawnTimingGame(float time)
    {
        yield return new WaitForSeconds(time);
        Instantiate(timingObject, new Vector3(0, 0, 0), Quaternion.identity);
        yield return null;
    }

    public void AddError()
    {
        gameData.consecutiveErrors++;
        gameData.errors++;
        if (gameData.consecutiveErrors == 2)
        {
            SaveSystem.DeleteSaveFile();
            isPlaying = false;
            LoadCutscene("AAAAAAAAAA_I hate it here!_I've made a fool out of myself_" +
                "I can't be here any longer!_I want to change school again!");
        }

    }
}
