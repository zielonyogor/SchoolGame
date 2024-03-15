using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlidingGameManager : MonoBehaviour
{
    [SerializeField] List<Transform> blocks;
    [SerializeField] Transform player;

    //later delete [SerializeField]
    [SerializeField] SlidingMapLayout slidingMapLayout;


    [SerializeField] SlidingGame slidingGame;

    private void Start()
    {
        player.position = slidingMapLayout.playerPosition;
        transform.position = slidingMapLayout.finishPosition;
        for (int i = 0; i < slidingMapLayout.blockPosition.Count; i++)
        {
            blocks[i].gameObject.SetActive(true);
            blocks[i].position = slidingMapLayout.blockPosition[i];
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            slidingGame.GameFinished();
        }
    }

    
}
