using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    private int numberOfPuzzles;
    public List<Transform> objects;

    void Start()
    {
        numberOfPuzzles = MiniGameManager.instance.numberOfPuzzles;
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
        int randomX = Random.Range(-80, 80);
        int randomY = Random.Range(-32, 48);
        int i = 0;
        while (true)
        {
            objects[index].position = new Vector2(randomX, randomY);
            if (!(Mathf.Abs(randomX) <= 46 && randomY <= 5) && !isColliding(index))
            {
                break;
            }
            randomX = Random.Range(-80, 80);
            randomY = Random.Range(-32, 48);
            i++;
            if (i >= 100)
                break;
        }
    }

    private bool isColliding(int index)
    {
        for (int i = 0; i < index; i++)
        {
            float distance = Vector2.Distance(objects[index].position, objects[i].position);
            if (distance < 14f) //11 or sth was enough but with 14 it's better (holes aren't on each other)
            {
                return true;
            }
        }
        return false;
    }
}
