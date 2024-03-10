using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Levelnfo : ScriptableObject
{
    public List<string> miniGames;

    public float time;

    public virtual void LoadData() {
        MiniGameManager.instance.miniGames = miniGames;
        MiniGameManager.instance.time = time;
    }
}
