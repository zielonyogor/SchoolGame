using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMiniGame
{
    public bool HasTimingGame { get; set; }
    void GameEnd();
    void GameFinished();
    IEnumerator PlayCountdown();
    //IEnumerator PlayConfetti();
}
