using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class DialogueGame : MonoBehaviour, IMiniGame
{
    private List<Transform> DialogueButtons = new();
    private string[] dialogueParts;
    private int expectedID;
    private int numberOfButtons;

    [SerializeField] ParticleSystem confetti_1, confetti_2;

    void Start()
    {
        dialogueParts = MiniGameManager.instance.dialogueText.Split('_');

        expectedID = 0;
        numberOfButtons = dialogueParts.Length;

        Timer.instance.OnTimeUp += GameEnd;

        for (int i = 0; i < numberOfButtons; i++)
        {
            DialogueButtons.Add(transform.GetChild(i));
            DialogueButtons[i].gameObject.SetActive(true);
            DialogueButtons[i].GetChild(0).GetComponent<TextMeshProUGUI>().text = dialogueParts[i];
        }
        StartCoroutine(Timer.instance.DecreaseTimer(MiniGameManager.instance.time));
    }


    public void OnButtonClicked(int id)
    {
        if (expectedID == id) expectedID += 1;
        else
        {
            GameEnd();
        }
        if (DialogueButtons.Count == expectedID)
        {
            GameFinished();
        }
    }

    public void GameEnd()
    {
        foreach (Transform child in DialogueButtons)
        {
            child.gameObject.SetActive(false);
        }
        Timer.instance.DisableTimer();
        Timer.instance.OnTimeUp -= GameEnd;
        MiniGameManager.instance.HandleGameLoss();
    }

    public void GameFinished()
    {
        StartCoroutine(PlayConfetti());
    }

    private IEnumerator PlayConfetti()
    {
        yield return new WaitForEndOfFrame();
        Timer.instance.DisableTimer();
        confetti_1.Play();
        confetti_2.Play();
        yield return new WaitForSeconds(2.5f);
        MiniGameManager.instance.NextLevel();
    }
}
