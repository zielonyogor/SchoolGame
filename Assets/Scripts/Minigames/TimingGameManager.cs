using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingGameManager : MonoBehaviour
{
    private float timeStamp = 0;

    [SerializeField] private MonoBehaviour miniGame;
    [SerializeField] GameObject timingGame;

    private IMiniGame game;
    void Start()
    {
        if (MiniGameManager.instance.gameData.day > 0) //later add some day later (maybe 2nd week)
        {
            game = miniGame as IMiniGame;
            timeStamp = Random.Range(1f, MiniGameManager.instance.time - 1f); //some offset
            Debug.Log("offset : " +  timeStamp);
            StartCoroutine(SpawnTimingGame(timeStamp));
        }
    }
    private IEnumerator SpawnTimingGame(float time)
    {
        yield return new WaitForSeconds(time);
        game.HasTimingGame = true;
        GameObject timingGameObject = Instantiate(timingGame, new Vector3(0, 0, 0), Quaternion.identity);
        yield return new WaitUntil(() => timingGameObject == null);
        Debug.Log("Object destroyed");
        game.HasTimingGame = false;
        yield return null;
    }
}
