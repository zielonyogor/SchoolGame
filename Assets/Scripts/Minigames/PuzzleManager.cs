using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    private int numberOfPuzzles;

    void Start()
    {
        numberOfPuzzles = MiniGameManager.instance.numberOfPuzzles;
        for(int i = 0; i < numberOfPuzzles; i++)
        {
            ChangePosition(transform.GetChild(i));
            ChangePosition(transform.GetChild(i).GetChild(0));
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    void ChangePosition(Transform t)
    {
        int randomX = Random.Range(-80, 80);
        int randomY = Random.Range(-32, 48);
        while (Mathf.Abs(randomX) <= 46 && randomY <= 4)
        {
            randomX = Random.Range(-80, 80);
            randomY = Random.Range(-32, 48);
        }
        t.position = new Vector2(randomX, randomY);
    }
}
