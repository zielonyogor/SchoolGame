using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

enum ResultState
{
    Correct,
    CorrectRare,
    Incorrect,
    IncorrectRare
}

public class PhoneManager : MonoBehaviour
{
    [Header("Scene objects")]
    [Header("Message UI")]
    [SerializeField] TextMeshProUGUI messageTextField;
    [SerializeField] TextMeshProUGUI personTextField;
    [SerializeField] Button button1, button2;
    [SerializeField] GameObject clockText;
    [Header("Result UI")]
    [SerializeField] TextMeshProUGUI resultTextField;
    [Header("Background")]
    [SerializeField] SpriteRenderer backgroundImage;
    [SerializeField] Sprite diarySprite;
    [Header("List of scenarios")]
    [SerializeField] List<Scenario_SO> scenarios; //random scenarios or based on day????

    private int scenarioIndex;
    private string scenarioText;
    private string resultText;

    void Start()
    {
        button1.interactable = false;
        button2.interactable = false;
        scenarioIndex = Random.Range(0, scenarios.Count);
        scenarioText = scenarios[scenarioIndex].scenarioText;

        StartCoroutine(displayText());
    }

    private IEnumerator displayText()
    {
        personTextField.text = scenarios[scenarioIndex].personName;
        messageTextField.text = "";
        for (int i = 0; i < scenarioText.Length; i++)
        {
            messageTextField.text += scenarioText[i];
            if (scenarioText[i] == '\n')
            {
                yield return new WaitForSeconds(1f);
            }
            yield return new WaitForSeconds(Constants.letterDelay);
        }
        yield return new WaitForSeconds(.5f);

        int randomOrder = Random.Range(0, 2);
        if (randomOrder == 0)
        {
            button1.GetComponentInChildren<TextMeshProUGUI>().text = scenarios[scenarioIndex].optionCorrect;
            button1.onClick.AddListener(() => onClickButton(true));
            button2.GetComponentInChildren<TextMeshProUGUI>().text = scenarios[scenarioIndex].optionIncorrect;
            button2.onClick.AddListener(() => onClickButton(false));
        }
        else
        {
            button1.GetComponentInChildren<TextMeshProUGUI>().text = scenarios[scenarioIndex].optionIncorrect;
            button1.onClick.AddListener(() => onClickButton(false));
            button2.GetComponentInChildren<TextMeshProUGUI>().text = scenarios[scenarioIndex].optionCorrect;
            button2.onClick.AddListener(() => onClickButton(true));
        }

        button1.interactable = true;
        button2.interactable = true;
    }
    public void onClickButton(bool isCorrect)
    {
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);

        if (isCorrect)
        {
            Debug.Log("Correct answer!");
            if (Random.Range(0f, 100f) <= 99)
            {
                StartCoroutine(displayResult(ResultState.Correct));
            }
            else
            {
                StartCoroutine(displayResult(ResultState.CorrectRare));
            }
        }
        else
        {
            Debug.Log("Incorrect answer.");
            if (Random.Range(0f, 100f) <= 99)
            {
                StartCoroutine(displayResult(ResultState.Incorrect));
            }
            else
            {
                StartCoroutine(displayResult(ResultState.IncorrectRare));
            }
        }
    }

    private IEnumerator displayResult(ResultState result)
    {

        messageTextField.gameObject.SetActive(false);
        personTextField.gameObject.SetActive(false);
        clockText.SetActive(false);
        resultTextField.gameObject.SetActive(true);

        switch (result)
        {
            case ResultState.Correct:
                resultText = scenarios[scenarioIndex].resultCorrect;
                MiniGameManager.instance.gameData.errors--;
                break;
            case ResultState.CorrectRare:
                resultText = scenarios[scenarioIndex].resultCorrectRare;
                break;
            case ResultState.Incorrect:
                resultText = scenarios[scenarioIndex].resultIncorrect;
                MiniGameManager.instance.gameData.errors++;
                break;
            case ResultState.IncorrectRare:
                resultText = scenarios[scenarioIndex].resultIncorrectRare;
                break;
        }
        resultTextField.text = "";
        for (int i = 0; i < resultText.Length; i++)
        {
            resultTextField.text += resultText[i];
            if (resultText[i] == '\n')
            {
                yield return new WaitForSeconds(1f);
            }
            yield return new WaitForSeconds(Constants.letterDelay);
        }
        yield return new WaitForSeconds(2.0f);

        MiniGameManager.instance.ExitCutscene();
    }
}
