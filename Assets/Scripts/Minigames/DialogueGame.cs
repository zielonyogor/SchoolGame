using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueGame : BaseMiniGame
{
    private List<Transform> dialogueButtons = new();
    private string[] dialogueParts;
    private int expectedID;
    private int numberOfButtons;

    void Start()
    {
        dialogueParts = MiniGameManager.instance.dialogueText.Split('_');
        gameTime = MiniGameManager.instance.time;

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

    public override IEnumerator PlayCountdown()
    {
        yield return StartCoroutine(base.PlayCountdown());

        StartCoroutine(timer.DecreaseTimer(gameTime));
    }

    void ChangePosition(int index)
    {
        int randomX = Random.Range(-260, 180);
        int randomY = Random.Range(-220, 280);
        int i = 0;
        while (true)
        {
            dialogueButtons[index].gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(randomX, randomY);
            if (!IsColliding(index)) break;
            randomX = Random.Range(-260, 180);
            randomY = Random.Range(-220, 280);
            i++;
            //so that it wont fall into inf loop
            if (i >= 100)
            {
                break;
            }
        }
    }

    private bool IsColliding(int index)
    {
        Vector3[] corners1 = new Vector3[4];
        dialogueButtons[index].GetComponent<RectTransform>().GetWorldCorners(corners1);
        Rect rect1 = new Rect(corners1[0], corners1[2] - corners1[0]);

        for (int i = 0; i < index; i++)
        {

            Vector3[] corners2 = new Vector3[4];
            dialogueButtons[i].GetComponent<RectTransform>().GetWorldCorners(corners2);
            Rect rect2 = new Rect(corners2[0], corners2[2] - corners2[0]);

            if (rect1.Overlaps(rect2))
            {
                return true;
            }
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

    public override void GameEnd()
    {
        foreach (Transform child in dialogueButtons)
        {
            child.gameObject.SetActive(false);
        }
        timer.OnTimeUp -= GameEnd;
        base.GameEnd();
    }
}
