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

    [SerializeField] int numberOfQuestions = 3;
    private int correct = 0;
    private void Awake()
    {
        submitAction = new ActionMap().Gameplay.SubmitQuestion;
    }

    private void OnEnable()
    {
        submitAction.Enable();
        submitAction.performed += SubmitQuestion;
        Timer.instance.OnTimeUp += GameFinished;
    }

    private void OnDisable()
    {
        submitAction.performed -= SubmitQuestion;
        submitAction.Disable();
    }

    void Start()
    {
        //change number of questions based on day
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

    void SubmitQuestion(InputAction.CallbackContext context)
    {
        if (currentQuestion >= numberOfQuestions - 1)
        {
            if (correct == numberOfQuestions) GameFinished();
            else GameEnd();
        }
        else
        {
            //maybe not tryparse but just check if null?
            if (int.TryParse(questionObjects[currentQuestion].GetChild(3).GetComponent<TMP_InputField>().text, out int answer))
            {
                if (answer == correctAnswers[currentQuestion])
                {
                    Debug.Log("dobrze nr: " + correctAnswers);
                    correct++;
                }
                currentQuestion++;
                questionObjects[currentQuestion].GetChild(3).GetComponent<TMP_InputField>().Select();
            }
            else
            {
                questionObjects[currentQuestion].GetChild(3).GetComponent<TMP_InputField>().text = null;
                Debug.Log("nie int");
                questionObjects[0].GetChild(3).GetComponent<TMP_InputField>().Select();

            }
        }
    }

    public void GameEnd()
    {
        Timer.instance.DisableTimer();
        Timer.instance.OnTimeUp -= GameEnd;
        MiniGameManager.instance.HandleGameLoss();
    }

    public void GameFinished()
    {
        MiniGameManager.instance.NextLevel();
    }

}
