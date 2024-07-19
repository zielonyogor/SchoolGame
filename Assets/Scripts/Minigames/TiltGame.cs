using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class TiltGame : MonoBehaviour, IMiniGame
{
    public bool HasTimingGame{ get; set; }

    private ActionMap actionMap;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float tiltForce = 5f;

    private WaitForFixedUpdate waitForFixedUpdate;

    [Header("Extras")]
    [SerializeField] Timer timer;
    [SerializeField] ParticleSystem confetti_1, confetti_2;
    [SerializeField] GameObject timingGame;

    [Header("Countdown")]
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject countdown;

    private void Awake()
    {
        actionMap = new ActionMap();
        waitForFixedUpdate = new WaitForFixedUpdate();
        timer.isDecreasing = false;
    }
    private void Start()
    {
        
        StartCoroutine(PlayCountdown());
    }
    public IEnumerator PlayCountdown()
    {
        GameObject spawnedObject = Instantiate(countdown, canvas.transform);
        Animator animator = spawnedObject.GetComponent<Animator>();
        while (animator && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            yield return null;

        float startTilt = (int)Random.Range(0, 2) == 0 ? -1 : 1;
        rb.AddTorque(startTilt * tiltForce);

        actionMap.Gameplay.Tilt.performed += OnTilt;
        timer.OnTimeUp += GameFinished;

        StartCoroutine(CheckRotation());
        //here is a little goofy algorithm for time in increasing type
        //(maybe ill just add another day variable for that)
        StartCoroutine(timer.DecreaseTimer(60 / MiniGameManager.instance.time + 1));
        Debug.Log(60 / MiniGameManager.instance.time + 1);
    }

    private void OnEnable()
    {
        actionMap.Enable();
    }
    private void OnDisable()
    {
        actionMap.Gameplay.Tilt.performed -= OnTilt;
        actionMap.Disable();
    }

    private IEnumerator CheckRotation()
    {
        while (Mathf.Abs(rb.rotation) < 20f)
        {
            yield return waitForFixedUpdate;
        }
        GameEnd();
    }

    private void OnTilt(InputAction.CallbackContext context)
    {
        rb.velocity = Vector3.zero;
        float tiltValue = context.ReadValue<float>();
        if (tiltValue != 0)
        {
            rb.AddTorque(tiltValue * tiltForce);
        }
    }

    public void GameEnd()
    {
        StopAllCoroutines();
        actionMap.Gameplay.Tilt.performed -= OnTilt;
        timer.DisableTimer();
        rb.bodyType = RigidbodyType2D.Static;
        MiniGameManager.instance.HandleGameLoss();
    }

    public void GameFinished()
    {
        actionMap.Gameplay.Tilt.performed -= OnTilt;
        timer.DisableTimer();
        rb.bodyType = RigidbodyType2D.Static;
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
