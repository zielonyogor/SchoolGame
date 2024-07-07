using UnityEngine;

[CreateAssetMenu(fileName = "Day_10", menuName = "DayConfigs/Day_10")]
public class DayTenConfig : Levelnfo
{
    public SlidingMapLayout slidingMapLayout;
    public int numberOfBooks;
    public int bookSpeed;
    public int tableSize;
    public int numberOfQuestions;

    public override void LoadData()
    {
        base.LoadData();
        MiniGameManager.instance.slidingMapLayout = slidingMapLayout;
        MiniGameManager.instance.numberOfBooks = numberOfBooks;
        MiniGameManager.instance.bookSpeed = bookSpeed;
        MiniGameManager.instance.tableSize = tableSize;
        MiniGameManager.instance.numberOfQuestions = numberOfQuestions;
    }
}