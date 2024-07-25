using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class TiltGame : BaseMiniGame
{
    private ActionMap actionMap;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float tiltForce = 5f;

    private WaitForFixedUpdate waitForFixedUpdate;

    private void Awake()
    {
        actionMap = new ActionMap();
        waitForFixedUpdate = new WaitForFixedUpdate();
        timer.isDecreasing = false;
    }
    private void Start()
    {
        gameTime = 60 / MiniGameManager.instance.time + 1;
        StartCoroutine(PlayCountdown());
    }
    public override IEnumerator PlayCountdown()
    {
        yield return StartCoroutine(base.PlayCountdown());

        float startTilt = (int)Random.Range(0, 2) == 0 ? -1 : 1;
        rb.AddTorque(startTilt * tiltForce);

        actionMap.Gameplay.Tilt.performed += OnTilt;
        timer.OnTimeUp += GameFinished;

        StartCoroutine(CheckRotation());
        StartCoroutine(timer.DecreaseTimer(gameTime));
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

    public override void GameEnd()
    {
        StopAllCoroutines();
        actionMap.Gameplay.Tilt.performed -= OnTilt;
        rb.bodyType = RigidbodyType2D.Static;
        base.GameEnd();
    }

    public override void GameFinished()
    {
        actionMap.Gameplay.Tilt.performed -= OnTilt;
        rb.bodyType = RigidbodyType2D.Static;
        base.GameFinished();
    }
}
