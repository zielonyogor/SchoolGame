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

        for (int i = items.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            (items[randomIndex], items[i]) = (items[i], items[randomIndex]);
        }

        for (int i = 0; i < numberOfMeds; i++)
        {
            pickedChildren.Add(items[i]);
            items[i].gameObject.tag = "DraggableCorrect";
            items[i].transform.position = new Vector2(Random.Range(-40, 30), Random.Range(-48, 16));

            images[i].sprite = items[i].GetComponent<SpriteRenderer>().sprite;
        }

    }

}
