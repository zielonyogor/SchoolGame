using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueGame : MonoBehaviour, IMiniGame
{
    public bool HasTimingGame {get; set;}

    private List<Transform> dialogueButtons = new();
    private string[] dialogueParts;
    private int expectedID;
    private int numberOfButtons;

    [Header("Extras")]
    [SerializeField] Timer timer;
    [SerializeField] ParticleSystem confetti_1, confetti_2;

    [Header("Countdown")]
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject countdown;

    void Start()
    {
        dialogueParts = MiniGameManager.instance.dialogueText.Split('_');

        expectedID = 0;
        numberOfButtons = dialogueParts.Length;

        timer.OnTimeUp += GameEnd;

        for (int i = 0; i < numberOfButtons; i++)
        {
            dialogueButtons.Add(transform.GetChild(i));
            dialogueButtons[i].gameObject.SetActive(true);
            dialogueButtons[i].GetChild(0).GetComponent<TextMeshProUGUI>().text = dialogueParts[i];
            ChangePosition(i);
        }
        StartCoroutine(PlayCountdown());
    }

    public IEnumerator PlayCountdown()
    {
        GameObject spawnedObject = Instantiate(countdown, canvas.transform);
        Animator animator = spawnedObject.GetComponent<Animator>();
        while (animator && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            yield return null;

        StartCoroutine(timer.DecreaseTimer(MiniGameManager.instance.time));
    }

    void ChangePosition(int index)
    {
        int randomX = Random.Range(-360, 260);
        int randomY = Random.Range(-260, 360);
        int i = 0;
        while (true)
        {
            dialogueButtons[index].gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(randomX, randomY);
            if (!isColliding(index)) break;
            randomX = Random.Range(-360, 260);
            randomY = Random.Range(-260, 360);
            i++;
            if (i >= 100) break;
        }
    }

    private bool isColliding(int index)
    {
        for (int i = 0; i < index; i++)
        {
            float distance = Vector2.Distance(dialogueButtons[index].gameObject.GetComponent<RectTransform>().anchoredPosition, dialogueButtons[i].gameObject.GetComponent<RectTransform>().anchoredPosition);
            if (distance < 300f) return true;
        }
        return false;
    }


    public void OnButtonClicked(int id)
    {
        dialogueButtons[id].GetComponent<Button>().interactable = false;
        if (expectedID == id) expectedID += 1;
        else
        {
            foreach (Transform t in dialogueButtons)
            {
                t.GetComponent<Button>().interactable = false;
            }
            GameEnd();
        }
        if (dialogueButtons.Count == expectedID)
        {
            GameFinished();
        }
    }

    public void GameEnd()
    {
        foreach (Transform child in dialogueButtons)
        {
            child.gameObject.SetActive(false);
        }
        timer.DisableTimer();
        timer.OnTimeUp -= GameEnd;
        MiniGameManager.instance.HandleGameLoss();
    }

    public void GameFinished()
    {
        StartCoroutine(PlayConfetti());
    }

    private IEnumerator PlayConfetti()
    {
        yield return new WaitForEndOfFrame();
        timer.DisableTimer();
        confetti_1.Play();
        confetti_2.Play();
        yield return new WaitForSeconds(2.5f);
        MiniGameManager.instance.NextLevel();
    }
}
