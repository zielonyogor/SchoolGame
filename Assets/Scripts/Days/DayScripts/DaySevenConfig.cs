using UnityEngine;


[CreateAssetMenu(fileName = "Day_7", menuName = "DayConfigs/Day_7")]
public class DaySevenConfig : Levelnfo
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