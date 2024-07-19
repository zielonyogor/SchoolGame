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
    private List<bool> playerAnswers = new List<bool>();

    public int currentSelectedField = 0;

    [Header("MiniGame variable")]
    [SerializeField] int numberOfQuestions;

    [Header("Extras")]
    [SerializeField] Timer timer;
    [SerializeField] ParticleSystem confetti_1, confetti_2;
    [SerializeField] GameObject timingGame;

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

    public IEnumerator PlayCountdown()
    {
        GameObject spawnedObject = Instantiate(countdown, canvas.transform);
        Animator animator = spawnedObject.GetComponent<Animator>();
        while (animator && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            yield return null;

        for (int i = 0; i < numberOfQuestions; i++)
        {
            questionObjects[i].GetComponentInChildren<TMP_InputField>().interactable = true;
        }
        questionObjects[currentSelectedField].GetComponentInChildren<TMP_InputField>().Select();
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
        Debug.Log("handle select");
        currentSelectedField = index;
    }

    public void GameEnd()
    {
        timer.DisableTimer();
        timer.OnTimeUp -= GameEnd;

        for (int i = 0; i < numberOfQuestions; i++)
        {
            questionObjects[i].GetComponentInChildren<TMP_InputField>().interactable = false;
        }

        MiniGameManager.instance.HandleGameLoss();
    }

    public void GameFinished()
    {
        timer.OnTimeUp -= GameEnd;

        for (int i = 0; i < numberOfQuestions; i++)
        {
            questionObjects[i].GetComponentInChildren<TMP_InputField>().interactable = false;
        }

        StartCoroutine(PlayConfetti());
    }

    private IEnumerator PlayConfetti()
    {
        yield return new WaitForEndOfFrame();
        timer.DisableTimer();
        yield return new WaitUntil(() => HasTimingGame == false);
        Destroy(timingGame);
        confetti_1.Play();
        confetti_2.Play();
        yield return new WaitForSeconds(2.5f);
        MiniGameManager.instance.NextLevel();
    }

}