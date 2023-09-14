using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
{
    [SerializeField]private Timer timeScript;

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
        timeScript.OnTimeUp += GameEnd;
        var r = Random.Range(-15f, 15f);
        rb.rotation = r;
        Debug.Log(r);
        StartCoroutine(CheckRotation());
    }

    private void OnEnable()
    {
        actionMap.Enable();
        actionMap.Gameplay.Tilt.performed += OnTilt;
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
        timeScript.OnTimeUp -= GameEnd;
    }

    private void OnTilt(InputAction.CallbackContext context)
    {
        float tiltValue = context.ReadValue<float>();
        if (tiltValue != 0)
        {
            Debug.Log("tilt action");
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

    //private void Update()
    //{
    //    if (Input.GetKeyDown("1"))
    //    {
    //        StartCoroutine(timeScript.DecreaseTimer(10f));
    //    }
    //}

}
