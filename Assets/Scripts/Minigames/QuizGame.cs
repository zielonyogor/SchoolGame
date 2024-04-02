using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizGame : MonoBehaviour
{
    List<int> correctAnswers;
    List<Transform> questionObjects = new List<Transform>();

    [SerializeField] Transform questionPrefab;

    private InputAction submitAction;
    private int currentQuestion=0;

    [SerializeField] int numberOfQuestions;
    private int correct = 0;

    [SerializeField] ParticleSystem confetti_1, confetti_2;
    [SerializeField] Timer timer;

    private void Awake()
    {
        submitAction = new ActionMap().Gameplay.SubmitQuestion;
    }

    private void OnEnable()
    {
        submitAction.Enable();
        submitAction.performed += SubmitQuestion;
        timer.OnTimeUp += GameEnd;
    }

    private void OnDisable()
    {
        submitAction.performed -= SubmitQuestion;
        submitAction.Disable();
    }

    void Start()
    {
        numberOfQuestions = MiniGameManager.instance.numberOfQuestions;
        correctAnswers = new List<int>(numberOfQuestions);
        for (int i = 0; i < numberOfQuestions; i++)
        {
            correctAnswers.Add(0);    
        }
        for (int i = 0; i < numberOfQuestions; i++){
            questionObjects.Add(transform.GetChild(i));
            ChangeEquation(i);
            questionObjects[i].gameObject.SetActive(true);
        }
        questionObjects[0].GetChild(3).GetComponent<TMP_InputField>().Select();


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

    public void HandleClick()
    {
        questionObjects[currentQuestion].GetChild(3).GetComponent<TMP_InputField>().Select();
        questionObjects[currentQuestion].GetChild(3).GetComponent<TMP_InputField>().ActivateInputField();
    }

    void SubmitQuestion(InputAction.CallbackContext context)
    {
        if (int.TryParse(questionObjects[currentQuestion].GetChild(3).GetComponent<TMP_InputField>().text, out int answer))
        {
            if (answer == correctAnswers[currentQuestion])
                correct++;

            currentQuestion++;
        }
        else
        {
            questionObjects[currentQuestion].GetChild(3).GetComponent<TMP_InputField>().text = null;
            questionObjects[currentQuestion].GetChild(3).GetComponent<TMP_InputField>().ActivateInputField();

        }
        if (currentQuestion == numberOfQuestions)
            currentQuestion--;
        questionObjects[currentQuestion].GetChild(3).GetComponent<TMP_InputField>().Select();

        if (correct == numberOfQuestions)
            GameFinished();
        else if (currentQuestion == numberOfQuestions)
            GameEnd();
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
