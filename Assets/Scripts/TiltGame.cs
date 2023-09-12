using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]Timer TimeScript;
    private void Start()
    {
        TimeScript.OnTimeUp += GameEnd;
    }

    private void GameEnd()
    {
        Debug.Log("Game ended");
        TimeScript.OnTimeUp -= GameEnd;
    }

    private void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            StartCoroutine(TimeScript.DecreaseTimer(10f));
        }
    }

}
