using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlidingGameManager : MonoBehaviour, IMiniGame
{
    [SerializeField] List<Transform> blocks;
    [SerializeField] Transform player;

    //later delete [SerializeField]
    [SerializeField] SlidingMapLayout slidingMapLayout;

    private void Start()
    {
        player.position = slidingMapLayout.playerPosition;
        transform.position = slidingMapLayout.finishPosition;
        for (int i = 0; i < slidingMapLayout.blockPosition.Count; i++)
        {
            blocks[i].gameObject.SetActive(true);
            blocks[i].position = slidingMapLayout.blockPosition[i];
        }

        Timer.instance.OnTimeUp += GameEnd;
        StartCoroutine(Timer.instance.DecreaseTimer(MiniGameManager.instance.time));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("yippee");
            MiniGameManager.instance.NextLevel();
        }
    }

    public void GameEnd()
    {
        Timer.instance.DisableTimer();
        Timer.instance.OnTimeUp -= GameEnd;
        MiniGameManager.instance.HandleGameLoss();
    }

    public void GameFinished()
    {
        MiniGameManager.instance.NextLevel();
    }
}
