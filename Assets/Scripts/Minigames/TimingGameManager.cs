using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TimingGameManager : MonoBehaviour
{
    private float timeStamp = 0;

    [SerializeField] private MonoBehaviour miniGame;
    [SerializeField] GameObject timingGame;

    private IMiniGame game;
    void Start()
    {
        int day = MiniGameManager.instance.gameData.day;
        if (day > 7)
        {
            int probability = day > 21 ? 90 : day > 14 ? 80 : 60;
            if (Random.Range(0, 100) < probability)
            {
                game = miniGame as IMiniGame;
                if (MiniGameManager.instance.miniGames[MiniGameManager.instance.currentGameIndex] == "TiltGame")
                {
                    timeStamp = Random.Range(2f, 60 / MiniGameManager.instance.time - 1f);
                }
                else
                {
                    timeStamp = Random.Range(2f, MiniGameManager.instance.time - 2f); //some offset
                }
                StartCoroutine(SpawnTimingGame(timeStamp));
            }
        }
        else
        {
            Destroy(this);
        }
    }
    private IEnumerator SpawnTimingGame(float time)
    {
        yield return new WaitForSeconds(time);
        game.HasTimingGame = true;
        GameObject timingGameObject = Instantiate(timingGame, transform.position, Quaternion.identity, transform.parent);
        yield return new WaitUntil(() => timingGameObject == null);
        game.HasTimingGame = false;
        yield return null;
    }
}
