using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
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
        float rotate = Random.Range(5f, 15f) * ((int)Random.Range(0,2) == 0 ? -1 : 1);
        rb.rotation = rotate;
        StartCoroutine(CheckRotation());
        StartCoroutine(Timer.instance.DecreaseTimer(10f));
    }

    private void OnEnable()
    {
        actionMap.Enable();
        actionMap.Gameplay.Tilt.performed += OnTilt;
        Timer.instance.OnTimeUp += GameCompleted;
    }
    private void OnDisable()
    {
        actionMap.Gameplay.Tilt.performed -= OnTilt;
        actionMap.Disable();
    }

    private IEnumerator CheckRotation()
    {
        while (Mathf.Abs(rb.rotation) < 30f)
        {
            yield return waitForFixedUpdate;
        }
        GameEnd();
    }

    private void GameEnd()
    {
        Debug.Log("Game ended");
        Timer.instance.DisableTimer();
        Timer.instance.OnTimeUp -= GameCompleted;
        rb.bodyType = RigidbodyType2D.Static;
    }

    private void GameCompleted()
    {
        Timer.instance.OnTimeUp -= GameCompleted;
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelMenu");
    }

    private void OnTilt(InputAction.CallbackContext context)
    {
        float tiltValue = context.ReadValue<float>();
        if (tiltValue != 0)
        {
            rb.AddTorque(tiltValue * tiltForce);
        }
    }
}
