using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public int day;
    public int errors;
    public int consecutiveErrors;
    public List<int> scenarioIDs;
    public int outfit; //not used currently

    public GameData()
    {
        day = 1;
        errors = 0;
        consecutiveErrors = 0;
        outfit = -1;
        scenarioIDs = new List<int>();
    }
}
