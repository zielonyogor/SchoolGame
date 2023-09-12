using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueGame : MonoBehaviour
{
    private List<Transform> DialogueButtons = new List<Transform>();
    private int expectedID;
    void Start()
    {
        expectedID = 0;
        foreach (Transform child in transform)
        {
            DialogueButtons.Add(child);
        }
        Debug.Log(GlobalVariables.Get<int>("currentDay"));
    }
    public void OnButtonClicked(int id)
    {
        if (expectedID == id)
            expectedID += 1;
        else
        {
            Debug.Log("Wrong!");
        }
        if (DialogueButtons.Count == expectedID)
        {
            Debug.Log("Yay!");
        }
    }
}
