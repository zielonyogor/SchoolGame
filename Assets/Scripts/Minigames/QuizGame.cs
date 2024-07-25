using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizGame : BaseMiniGame
{
    private List<int> correctAnswers;
    private List<Transform> questionObjects = new List<Transform>();
    private List<bool> playerAnswers = new List<bool>();

    private int currentSelectedField = 0;

    [Header("MiniGame variable")]
    [SerializeField] int numberOfQuestions;

    private void OnEnable()
    {
        timer.OnTimeUp += GameEnd;
    }

    void Start()
    {
        numberOfQuestions = MiniGameManager.instance.numberOfQuestions;
        gameTime = MiniGameManager.instance.time;
        correctAnswers = new List<int>(numberOfQuestions);
        for (int i = 0; i < numberOfQuestions; i++)
        {
            correctAnswers.Add(0);
            playerAnswers.Add(false);
        }
        for (int i = 0; i < numberOfQuestions; i++){
            questionObjects.Add(transform.GetChild(i));
            ChangeEquation(i);
            questionObjects[i].gameObject.SetActive(true);
            questionObjects[i].GetComponentInChildren<TMP_InputField>().interactable = false;
        }
        StartCoroutine(PlayCountdown());
    }

    public override IEnumerator PlayCountdown()
    {
        yield return StartCoroutine(base.PlayCountdown());

        for (int i = 0; i < numberOfQuestions; i++)
        {
            questionObjects[i].GetComponentInChildren<TMP_InputField>().interactable = true;
        }
        questionObjects[currentSelectedField].GetComponentInChildren<TMP_InputField>().Select();
        StartCoroutine(timer.DecreaseTimer(gameTime));
    }

    void ChangeEquation(int i)
    {
        int symbol = Random.Range(0, 3);
        int number_1 = Random.Range(0, 12);
        int number_2 = Random.Range(0, 12);
        switch (symbol)
        {
            case 0:
                correctAnswers[i] = number_1 + number_2;
                questionObjects[i].GetChild(1).GetComponent<TextMeshProUGUI>().text = "+";
                break;
            case 1:
                correctAnswers[i] = number_1 - number_2;
                questionObjects[i].GetChild(1).GetComponent<TextMeshProUGUI>().text = "-";
                break;
            case 2:
            default:
                correctAnswers[i] = number_1 * number_2;
                questionObjects[i].GetChild(1).GetComponent<TextMeshProUGUI>().text = "*";
                break;
        }
        questionObjects[i].GetChild(0).GetComponent<TextMeshProUGUI>().text = number_1.ToString();
        questionObjects[i].GetChild(2).GetComponent<TextMeshProUGUI>().text = number_2.ToString();

    }

    public void HandleSubmit()
    {
        if (int.TryParse(questionObjects[currentSelectedField].GetChild(4).GetComponent<TMP_InputField>().text, out int answer))
        {
            if (answer == correctAnswers[currentSelectedField])
            {
                playerAnswers[currentSelectedField] = true;
                bool hasPassed = true;
                for (int i = 0; i < numberOfQuestions; i++)
                {
                    if (!playerAnswers[i])
                    {
                        hasPassed = false;
                    }
                }
                if (hasPassed)
                {
                    GameFinished();
                    return;
                }
            }
            else
            {
                playerAnswers[currentSelectedField] = false;
            }
        }
        currentSelectedField = (currentSelectedField + 1) % numberOfQuestions;
        questionObjects[currentSelectedField].GetChild(4).GetComponent<TMP_InputField>().Select();
    }

    public void HandleSelect(int index)
    {
        currentSelectedField = index;
    }

    public override void GameEnd()
    {
        timer.OnTimeUp -= GameEnd;

        for (int i = 0; i < numberOfQuestions; i++)
        {
            questionObjects[i].GetComponentInChildren<TMP_InputField>().interactable = false;
        }

        if (int.TryParse(questionObjects[currentSelectedField].GetChild(4).GetComponent<TMP_InputField>().text, out int answer))
        {
            if (answer == correctAnswers[currentSelectedField])
            {
                playerAnswers[currentSelectedField] = true;
                bool hasPassed = true;
                for (int i = 0; i < numberOfQuestions; i++)
                {
                    if (!playerAnswers[i])
                    {
                        hasPassed = false;
                    }
                }
                if (hasPassed)
                {
                    GameFinished();
                    return;
                }
            }
        }
        base.GameEnd();
    }

    public override void GameFinished()
    {
        timer.OnTimeUp -= GameEnd;

        for (int i = 0; i < numberOfQuestions; i++)
        {
            questionObjects[i].GetComponentInChildren<TMP_InputField>().interactable = false;
        }

        base.GameFinished();
    }
}