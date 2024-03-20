using UnityEngine;

[CreateAssetMenu(fileName = "DayThree", menuName = "DayConfigs/Day Three")]
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