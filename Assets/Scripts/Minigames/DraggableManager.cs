using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DraggableManager : MonoBehaviour
{
    private List<Transform> pickedChildren = new List<Transform>();
    [SerializeField] private int numberOfMeds = 3;

    [SerializeField] private List<Image> images = new List<Image>();
    void Start()
    {
        numberOfMeds = MiniGameManager.instance.numberOfMeds;

        for (int i = numberOfMeds; i < 4; i++)
        {
            images[i].enabled = false;
        }

        List<Transform> items = new List<Transform>();
        foreach (Transform child in transform)
        {
            items.Add(child);
            child.gameObject.tag = "DraggableIncorrect";
        }

        //Try this later
        //List<Transform> tempList = new List<Transform>(transforms);

        //// Fisher-Yates shuffle
        //for (int i = tempList.Count - 1; i > 0; i--)
        //{
        //    int randomIndex = Random.Range(0, i + 1);
        //    Transform temp = tempList[i];
        //    tempList[i] = tempList[randomIndex];
        //    tempList[randomIndex] = temp;
        //}

        //// Select the first 'count' elements
        //Transform[] randomTransforms = new Transform[count];
        //for (int i = 0; i < count; i++)
        //{
        //    randomTransforms[i] = tempList[i];
        //}

        for (int i = 0; i < numberOfMeds; i++)
        {
            int randomIndex = i; //Random.Range(0, items.Count - i); //this weird
            pickedChildren.Add(items[randomIndex]);
            items[randomIndex].gameObject.tag = "DraggableCorrect";
            //Do I need to rotate this?? Maybe a little bit, Should Move around also
            items[randomIndex].transform.rotation = Quaternion.Euler(0, 0, Random.Range(-80,80));

            images[i].sprite = items[randomIndex].GetComponent<SpriteRenderer>().sprite;
            items.RemoveAt(randomIndex);
        }
    }

}
