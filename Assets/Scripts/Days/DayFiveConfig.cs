using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Day_5", menuName = "DayConfigs/Day Five")]
public class DayFiveConfig : Levelnfo
{
    public int numberOfBooks;
    public int bookSpeed;
    public int tableSize;

    public override void LoadData()
    {
        base.LoadData();
        MiniGameManager.instance.bookSpeed = bookSpeed;
        MiniGameManager.instance.numberOfBooks = numberOfBooks;
        MiniGameManager.instance.tableSize = tableSize;
    }
}