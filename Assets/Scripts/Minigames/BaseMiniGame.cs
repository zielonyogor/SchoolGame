using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMiniGame : MonoBehaviour, IMiniGame
{
    public bool HasTimingGame { get; set; }

    protected float gameTime;

    [Header("Confetti")]
    [SerializeField] ParticleSystem confetti_1, confetti_2;

    [Header("Countdown")]
    [SerializeField] protected Canvas canvas;
    [SerializeField] protected GameObject countdown;

    [Header("Other")]
    [SerializeField] protected Timer timer;
    [SerializeField] protected GameObject timingGame;
    [SerializeField] protected GameObject gameOverScreen;

    public virtual void GameEnd()
    {
        timer.DisableTimer();
        StartCoroutine(PlayGameOverScreen());
    }
    public virtual void GameFinished()
    {
        timer.DisableTimer();
        StartCoroutine(PlayConfetti());
    }
    public virtual IEnumerator PlayCountdown()
    {
        GameObject spawnedObject = Instantiate(countdown, canvas.transform);
        Animator animator = spawnedObject.GetComponent<Animator>();
        while (animator && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            yield return null;
    }
    public virtual IEnumerator PlayConfetti()
    {
        yield return new WaitUntil(() => HasTimingGame == false);
        Destroy(timingGame);
        confetti_1.Play();
        confetti_2.Play();
        yield return new WaitForSeconds(2.5f);
        MiniGameManager.instance.NextLevel();
    }

    public IEnumerator PlayGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        Animator animator = gameOverScreen.GetComponent<Animator>();
        yield return new WaitForFixedUpdate();
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            yield return null;
        MiniGameManager.instance.HandleGameLoss();
    }
}
