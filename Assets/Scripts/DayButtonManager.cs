using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject dayPointer;
    private void Start()
    {
        EnableDay(MiniGameManager.instance.gameData.day - 1);
    }

    private void EnableDay(int day)
    {
        Button button = transform.GetChild(day).GetComponent<Button>();
        button.interactable = true;
        Instantiate(dayPointer, button.transform);
    }
}
