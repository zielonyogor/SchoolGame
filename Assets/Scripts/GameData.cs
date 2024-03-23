using System;

[Serializable]
public class GameData
{
    public int day;
    public int errors;
    public int consecutiveErrors;
    public int outfit;

    public GameData()
    {
        day = 1;
        errors = 0;
        consecutiveErrors = 0;
        outfit = -1;
    }
}
