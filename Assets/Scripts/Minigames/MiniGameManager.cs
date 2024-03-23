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
        SceneManager.LoadScene("Cutscene");
    }

    public void NextLevel()
    {
        currentGameIndex++;
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

    public void ExitCutscene()
    {
        gameData.day += 1;
        SceneManager.LoadScene("LevelMenu"); 
    }

    //probably move to minigameManager
    private IEnumerator SpawnTimingGame(float time)
    {
        yield return new WaitForSeconds(time);
        Instantiate(timingObject, new Vector3(0, 0, 0), Quaternion.identity);
        yield return null;
    }
}
