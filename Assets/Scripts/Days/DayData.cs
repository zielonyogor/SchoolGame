using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DayData : ScriptableObject
{
    public List<string> miniGames;

    public bool isCutscene = false;
    public string cutscene = "";

    public string dialogue = "";

    public int numberOfMeds = -1;

    public float time;
    public float timeBalance = -1f;
}
