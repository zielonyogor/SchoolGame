using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour, IMiniGame
{

    private ActionMap actionMap;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float tiltForce = 5f;

    private WaitForFixedUpdate waitForFixedUpdate;

    private void Awake()
    {
        actionMap = new ActionMap();
        waitForFixedUpdate = new WaitForFixedUpdate();
    }
    private void Start()
    {
        float startTilt = (int)Random.Range(0,2) == 0 ? -1 : 1;
        rb.AddTorque(startTilt * tiltForce);
        Timer.instance.isDecreasing = false;
        StartCoroutine(CheckRotation());
        //in tilt game it should be a little different, the harder it gets the more time you have to spend balansing
        StartCoroutine(Timer.instance.DecreaseTimer(MiniGameManager.instance.dayInfo.time));
    }

    private void OnEnable()
    {
        actionMap.Enable();
        actionMap.Gameplay.Tilt.performed += OnTilt;
        Timer.instance.OnTimeUp += GameFinished;
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
        Timer.instance.DisableTimer();
        rb.bodyType = RigidbodyType2D.Static;
        MiniGameManager.instance.NextLevel();
    }

    public void GameFinished()
    {
        Timer.instance.OnTimeUp -= GameFinished;
        MiniGameManager.instance.NextLevel();
    }
}
