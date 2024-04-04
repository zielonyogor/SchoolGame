using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject dayPointer;
    [SerializeField] private GameObject dayDone;
    private void Start()
    {
        EnableDay(MiniGameManager.instance.gameData.day - 1);
    }

    private void EnableDay(int day)
    {
        //for (int i = 0; i < day; i++)
        //{
        //    Instantiate(dayDone, transform.GetChild(day).GetComponent<RectTransform>().position, 
        //        Quaternion.identity, this.transform);
        //}
        Button button = transform.GetChild(day).GetComponent<Button>();
        button.interactable = true;
        Instantiate(dayPointer, button.transform);
    }
}
