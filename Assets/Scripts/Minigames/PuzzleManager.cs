using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    private int numberOfPuzzles;
    private List<Transform> objects;

    void Start()
    {
        numberOfPuzzles = 3;//MiniGameManager.instance.numberOfPuzzles;
        objects = new List<Transform>();
        for(int i = 0; i < numberOfPuzzles; i++)
        {
            objects.Add(transform.GetChild(i));
            ChangePosition(i*2);
            objects.Add(transform.GetChild(i).GetChild(0));
            ChangePosition(i*2+1);

            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    void ChangePosition(int index)
    {
        int randomX = Random.Range(-128, 128);
        int randomY = Random.Range(-54, 74);
        int i = 0;
        while (true)
        {
            objects[index].position = new Vector2(randomX, randomY);
            if (!(Mathf.Abs(randomX) <= 46 && randomY <= 26) && !IsColliding(index)) break;
            randomX = Random.Range(-128, 128);
            randomY = Random.Range(-54, 74);
            i++;
            if (i >= 100) break;
        }
    }

    private bool IsColliding(int index)
    {
        for (int i = 0; i < index; i++)
        {
            float distance = Vector2.Distance(objects[index].position, objects[i].position);
            if (distance < 24f) //11 or sth was enough but with 14 it's better (holes aren't on each other)
            {
                return true;
            }
        }
        return false;
    }
}
