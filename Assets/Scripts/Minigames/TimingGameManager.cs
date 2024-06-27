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
        if (day > 7 || true)
        {
            int probability = day > 21 ? 90 : day > 14 ? 80 : 60;
            if (Random.Range(0, 100) < probability || true)
            {
                game = miniGame as IMiniGame;
                //need to change this a little beacause of: 
                //Tilting Game (less time than time in instance)
                //Stacking Game (infinite time for stacking books, not related to instance.time)
                timeStamp = Random.Range(1f, MiniGameManager.instance.time - 1f); //some offset
                Debug.Log("offset : " +  timeStamp);
                StartCoroutine(SpawnTimingGame(timeStamp));
            }
        }
    }
    private IEnumerator SpawnTimingGame(float time)
    {
        yield return new WaitForSeconds(time);
        game.HasTimingGame = true;
        GameObject timingGameObject = Instantiate(timingGame, transform.position, Quaternion.identity, transform);
        yield return new WaitUntil(() => timingGameObject == null);
        Debug.Log("Object destroyed");
        game.HasTimingGame = false;
        yield return null;
    }
}
