using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour, IMiniGame
{

    private ActionMap actionMap;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float tiltForce = 5f;

    private WaitForFixedUpdate waitForFixedUpdate;

    [SerializeField] ParticleSystem confetti_1, confetti_2;
    [SerializeField] Timer timer; 

    private void Awake()
    {
        actionMap = new ActionMap();
        waitForFixedUpdate = new WaitForFixedUpdate();
        timer.isDecreasing = false;
    }
    private void Start()
    {
        StartCoroutine(StartGame());
    }

    private void OnEnable()
    {
        actionMap.Enable();
        actionMap.Gameplay.Tilt.performed += OnTilt;
        timer.OnTimeUp += GameFinished;
    }
    private void OnDisable()
    {
        actionMap.Gameplay.Tilt.performed -= OnTilt;
        actionMap.Disable();
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSecondsRealtime(0.1f);

        float startTilt = (int)Random.Range(0,2) == 0 ? -1 : 1;
        rb.AddTorque(startTilt * tiltForce);

        StartCoroutine(CheckRotation());
        //here is a little goofy algorithm for time in increasing type
        //(maybe ill just add another day variable for that)
        StartCoroutine(timer.DecreaseTimer(60/MiniGameManager.instance.time + 1));
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
        confetti_1.Play();
        confetti_2.Play();
        yield return new WaitForSeconds(2.5f);
        MiniGameManager.instance.NextLevel();
    }
}
