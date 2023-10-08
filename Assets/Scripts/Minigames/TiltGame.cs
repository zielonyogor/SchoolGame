using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
{

    private ActionMap actionMap;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float tiltForce = 5f;
    [SerializeField] Vector2 m_centerOfMass;

    private WaitForFixedUpdate waitForFixedUpdate;

    private void Awake()
    {
        actionMap = new ActionMap();
        waitForFixedUpdate = new WaitForFixedUpdate();
        //rb.centerOfMass = m_centerOfMass;
    }
    private void Start()
    {
        float startTilt = (int)Random.Range(0,2) == 0 ? -1 : 1;
        rb.AddTorque(startTilt * tiltForce);
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
        StopAllCoroutines();
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
            Debug.Log(tiltValue);
            rb.AddTorque(tiltValue * tiltForce);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector2 worldCenterOfMass = transform.TransformPoint(m_centerOfMass);
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(transform.TransformPoint(m_centerOfMass), 0.1f);
        Gizmos.DrawLine(worldCenterOfMass + Vector2.up, worldCenterOfMass - Vector2.up);
        Gizmos.DrawLine(worldCenterOfMass + Vector2.right, worldCenterOfMass - Vector2.right);
    }
}
