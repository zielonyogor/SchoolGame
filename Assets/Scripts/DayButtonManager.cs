using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayButtonManager : MonoBehaviour
{
    private void Start()
    {
        EnableDay(MiniGameManager.instance.day - 1);
    }

    private void EnableDay(int day)
    {
        Button button = transform.GetChild(day).GetComponent<Button>();
        button.interactable = true;
        // Iterate through all children of the parent
        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    Button button = transform.GetChild(i).GetComponent<Button>();
        //    button.interactable = (i == day);
        //}
    }
}
