using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DraggableManager : MonoBehaviour
{
    private List<Transform> pickedChildren = new List<Transform>();
    [SerializeField] private int numberOfGoodItems = 3;

    [SerializeField] private List<Image> images = new List<Image>();
    void Start()
    {
        for (int i = numberOfGoodItems; i < 4; i++)
        {
            images[i].enabled = false;
        }
        List<Transform> items = new List<Transform>();
        foreach (Transform child in transform)
        {
            items.Add(child);
            child.gameObject.tag = "DraggableIncorrect";
        }
        for (int i = 0; i < numberOfGoodItems; i++)
        {
            int randomIndex = Random.Range(0, items.Count - i);
            pickedChildren.Add(items[randomIndex]);
            items[randomIndex].gameObject.tag = "DraggableCorrect";
            items[randomIndex].transform.rotation = Quaternion.Euler(0, 0, Random.Range(-80,80));
            images[i].sprite = items[randomIndex].GetComponent<SpriteRenderer>().sprite;
            items.RemoveAt(randomIndex);
        }
        //foreach (Transform pickedChild in pickedChildren)
        //{
        //    Debug.Log("Picked child: " + pickedChild.name);
        //}
    }

}
