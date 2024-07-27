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
    [SerializeField] TextMeshProUGUI resultTextField_1, resultTextField_2;

    [Header("Transition")]
    [SerializeField] GameObject transition;

    [Header("List of scenarios")]
    [SerializeField] List<Scenario_SO> scenarios;

    private Scenario_SO currentScenario;
    private string scenarioText;
    private string resultText;
    private bool isMultiScenario = false;

    void Start()
    {
        currentScenario = scenarios[DrawRandomScenario()];
        scenarioText = currentScenario.scenarioText;
        if (currentScenario.secondScenario != null)
        {
            isMultiScenario = true;
        }

        StartCoroutine(DisplayText());
    }

    //remembering what scenarios were played
    private int DrawRandomScenario()
    {
        int randomIndex = Random.Range(0, scenarios.Count);
        while(MiniGameManager.instance.gameData.scenarioIDs.Contains(randomIndex))
        {
            randomIndex = Random.Range(0, scenarios.Count);
        }
        MiniGameManager.instance.gameData.scenarioIDs.Add(randomIndex);
        return randomIndex;
    }

    private IEnumerator DisplayText()
    {
        personTextField.text = currentScenario.personName;
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

        button1.transform.parent.gameObject.SetActive(true);

        if (isMultiScenario)
        {
            button1.GetComponentInChildren<TextMeshProUGUI>().text = currentScenario.optionCorrect;
            button1.onClick.AddListener(OnClickContinue);
        }
        else
        {
            button2.transform.parent.gameObject.SetActive(true);
            int randomOrder = Random.Range(0, 2);
            if (randomOrder == 0)
            {
                button1.GetComponentInChildren<TextMeshProUGUI>().text = currentScenario.optionCorrect;
                button1.onClick.AddListener(() => OnClickButton(true));
                button2.GetComponentInChildren<TextMeshProUGUI>().text = currentScenario.optionIncorrect;
                button2.onClick.AddListener(() => OnClickButton(false));
            }
            else
            {
                button1.GetComponentInChildren<TextMeshProUGUI>().text = currentScenario.optionIncorrect;
                button1.onClick.AddListener(() => OnClickButton(false));
                button2.GetComponentInChildren<TextMeshProUGUI>().text = currentScenario.optionCorrect;
                button2.onClick.AddListener(() => OnClickButton(true));
            }
        }
    }

    public void OnClickContinue()
    {
        currentScenario = currentScenario.secondScenario;
        isMultiScenario = false;
        button1.onClick.RemoveListener(OnClickContinue);
        scenarioText = currentScenario.scenarioText;
        button1.transform.parent.gameObject.SetActive(false);
        StartCoroutine(DisplayText());
    }

    public void OnClickButton(bool isCorrect)
    {
        button1.transform.parent.gameObject.SetActive(false);
        button2.transform.parent.gameObject.SetActive(false);

        if (isCorrect)
        {
            if (Random.Range(0f, 100f) <= 90)
            {
                StartCoroutine(DisplayResult(ResultState.Correct));
            }
            else
            {
                StartCoroutine(DisplayResult(ResultState.CorrectRare));
            }
        }
        else
        {
            if (Random.Range(0f, 100f) <= 90)
            {
                StartCoroutine(DisplayResult(ResultState.Incorrect));
            }
            else
            {
                StartCoroutine(DisplayResult(ResultState.IncorrectRare));
            }
        }
    }

    private IEnumerator DisplayResult(ResultState result)
    {
        transition.SetActive(true);

        switch (result)
        {
            case ResultState.Correct:
                resultText = currentScenario.resultCorrect;
                MiniGameManager.instance.gameData.errors--;
                break;
            case ResultState.CorrectRare:
                resultText = currentScenario.resultCorrectRare;
                break;
            case ResultState.Incorrect:
                resultText = currentScenario.resultIncorrect;
                MiniGameManager.instance.gameData.errors++;
                break;
            case ResultState.IncorrectRare:
                resultText = currentScenario.resultIncorrectRare;
                break;
        }

        Animator animator = transition.GetComponent<Animator>();
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f)
        {
            yield return null;
        }

        messageTextField.transform.parent.gameObject.SetActive(false);
        personTextField.gameObject.SetActive(false);
        clockText.SetActive(false);
        resultTextField_1.transform.parent.gameObject.SetActive(true);

        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            yield return null;
        }
        //small delay after transition
        yield return new WaitForSeconds(Constants.letterDelay);

        resultTextField_1.text = "";
        resultTextField_2.text = "";
        for (int i = 0; i < resultText.Length; i++)
        {
            resultTextField_1.text += resultText[i];
            resultTextField_2.text += resultText[i];
            if (resultText[i] == '\n')
            {
                yield return new WaitForSeconds(1f);
            }
            yield return new WaitForSeconds(Constants.letterDelay);
        }
        yield return new WaitForSeconds(2.0f);

        MiniGameManager.instance.ExitPhone(); //probably enough for now
    }
}
