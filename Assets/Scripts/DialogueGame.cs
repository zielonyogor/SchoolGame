using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueGame : MonoBehaviour
{
    private List<Transform> DialogueButtons = new List<Transform>();
    private int expectedID;

    void Start()
    {
        expectedID = 0;
        Timer.instance.OnTimeUp += GameEnd;
        foreach (Transform child in transform)
        {
            DialogueButtons.Add(child);
        }
        //Debug.Log(GlobalVariables.Get<int>("currentDay"));
        StartCoroutine(Timer.instance.DecreaseTimer(5f));
    }


    public void OnButtonClicked(int id)
    {
        if (expectedID == id)
            expectedID += 1;
        else
        {
            Debug.Log("Wrong!");
            GameEnd();
        }
        if (DialogueButtons.Count == expectedID)
        {
            Debug.Log("Yay!");
            UnityEngine.SceneManagement.SceneManager.LoadScene("LevelMenu");
        }
    }

    private void GameEnd()
    {
        Debug.Log("Game ended");
        foreach (Transform child in DialogueButtons)
        {
            child.gameObject.SetActive(false);
        }
        Timer.instance.OnTimeUp -= GameEnd;
    }
}
