using UnityEngine;

[CreateAssetMenu(fileName = "Day_3", menuName = "DayConfigs/Day_3")]
public class DayThreeConfig : Levelnfo
{
    public int numberOfPuzzles;
    public int numberOfBooks;
    public int bookSpeed;
    public int tableSize;
    public SlidingMapLayout slidingMapLayout;

    public override void LoadData()
    {
        base.LoadData();
        MiniGameManager.instance.numberOfPuzzles = numberOfPuzzles;
        MiniGameManager.instance.bookSpeed = bookSpeed;
        MiniGameManager.instance.numberOfBooks = numberOfBooks;
        MiniGameManager.instance.slidingMapLayout = slidingMapLayout;
        MiniGameManager.instance.tableSize = tableSize;
    }
}