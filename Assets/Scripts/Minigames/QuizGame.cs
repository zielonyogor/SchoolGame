using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizGame : MonoBehaviour
{
    //[SerializeField] TextMeshProUGUI firstNumber;
    //[SerializeField] TextMeshProUGUI secondNumber;
    //[SerializeField] TextMeshProUGUI symbolText;

    //[SerializeField] TMP_InputField inputField;
    //int correctAnswer;

    [SerializeField] int numberOfQuestions = 3;

    List<int> correctAnswers;
    List<Transform> questionObjects = new List<Transform>();

    [SerializeField] Transform questionPrefab;

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
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            CheckAnswer();
        }
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

    void CheckAnswer()
    {
        for (int i = 0; i < numberOfQuestions; i++)
        {
            Debug.Log(questionObjects[i].GetChild(3).GetComponent<InputField>().text);
            if (int.Parse("0") == correctAnswers[i]){
        }
        }
        //if (int.Parse(inputField.text) == correctAnswer)
        //{
        //    inputField.text = "";
        //    ChangeEquation();
        //}
        //else
        //{
        //    Debug.Log("upsi pupsi");
        //}
    }
}
