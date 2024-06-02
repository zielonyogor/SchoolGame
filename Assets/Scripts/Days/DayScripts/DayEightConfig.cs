using UnityEngine;

[CreateAssetMenu(fileName = "Day_8", menuName = "DayConfigs/Day_8")]
public class DayEightConfig : Levelnfo
{
    public int numberOfBooks;
    public int bookSpeed;
    public int numberOfPuzzles;

    public override void LoadData()
    {
        base.LoadData();
        MiniGameManager.instance.numberOfBooks = numberOfBooks;
        MiniGameManager.instance.bookSpeed = bookSpeed;
        MiniGameManager.instance.numberOfPuzzles = numberOfPuzzles;
    }
}