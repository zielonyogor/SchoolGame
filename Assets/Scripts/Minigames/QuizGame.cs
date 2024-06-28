using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizGame : MonoBehaviour, IMiniGame
{
    public bool HasTimingGame {get; set;}

    private List<int> correctAnswers;
    private List<Transform> questionObjects = new List<Transform>();
    private List<bool> answers = new List<bool>();

    [Header("MiniGame variable")]
    [SerializeField] int numberOfQuestions;

    [Header("Extras")]
    [SerializeField] Timer timer;
    [SerializeField] ParticleSystem confetti_1, confetti_2;

    [Header("Countdown")]
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject countdown;

    private void OnEnable()
    {
        timer.OnTimeUp += GameEnd;
    }

    void Start()
    {
        numberOfQuestions = MiniGameManager.instance.numberOfQuestions;
        correctAnswers = new List<int>(numberOfQuestions);
        for (int i = 0; i < numberOfQuestions; i++)
        {
            correctAnswers.Add(0);
            answers.Add(false);
        }
        for (int i = 0; i < numberOfQuestions; i++){
            questionObjects.Add(transform.GetChild(i));
            ChangeEquation(i);
            questionObjects[i].gameObject.SetActive(true);
        }
        questionObjects[0].GetChild(4).GetComponent<TMP_InputField>().Select();

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

    public void HandleClick(int index)
    {
        Debug.Log("Index: " + index);
        if (int.TryParse(questionObjects[index].GetChild(4).GetComponent<TMP_InputField>().text, out int answer))
        {
            if (answer == correctAnswers[index])
            {
                answers[index] = true;
            }
            else
            {
                answers[index] = false;
            }
        }
    }

    public void SubmitQuestions()
    {
        for (int i = 0; i < numberOfQuestions; i++)
        {
            questionObjects[i].GetChild(4).GetComponent<TMP_InputField>().DeactivateInputField();
            questionObjects[i].GetChild(4).GetComponent<TMP_InputField>().interactable = false;
        }
        int correct = 0;
        for (int i = 0; i < numberOfQuestions; i++)
        {
            if (answers[i])
            {
                correct++;
            }
        }
        if (correct == numberOfQuestions)
        {
            GameFinished();
        }
        else
        {
            GameEnd();
        }
    }

    public void GameEnd()
    {
        timer.DisableTimer();
        timer.OnTimeUp -= GameEnd;
        MiniGameManager.instance.HandleGameLoss();
    }

    public void GameFinished()
    {
        timer.OnTimeUp -= GameEnd;
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
