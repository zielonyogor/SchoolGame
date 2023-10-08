using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimingGame : MonoBehaviour
{
    private InputAction spaceAction;

    private WaitForFixedUpdate waitForFixedUpdate;

    private float phase = 0;
    private float phaseDirection = 1;

    [Header("UI Elements")]
    [SerializeField] Transform leftPivot;
    [SerializeField] Transform rightPivot;
    [SerializeField] Transform goal;
    [SerializeField] Transform bar;

    [Header("Other")]
    [SerializeField] float barSpeed = 5f;
    private void Awake()
    {
        spaceAction = new ActionMap().Gameplay.StopBar;
        waitForFixedUpdate = new WaitForFixedUpdate();
    }
    private void OnEnable()
    {
        spaceAction.Enable();
        spaceAction.performed += Stop;
    }

    private void OnDisable()
    {
        spaceAction.performed -= Stop;
        spaceAction.Disable();
    }
    void Start()
    {
        goal.position = new Vector2(Random.Range(leftPivot.position.x, rightPivot.position.x), goal.position.y);
        StartCoroutine(MoveBar());
    }

    private IEnumerator MoveBar()
    {
        while (true)
        {
            bar.transform.position = Vector2.Lerp(leftPivot.position, rightPivot.position, phase);
            phase += Time.deltaTime * barSpeed * phaseDirection;
            if (phase >= 1 || phase <= 0)
                phaseDirection *= -1;
            yield return waitForFixedUpdate;
        }
    }

    private void Stop(InputAction.CallbackContext context)
    {
        StopAllCoroutines();
        BoxCollider2D barBox = bar.gameObject.GetComponent<BoxCollider2D>();
        BoxCollider2D goalBox = goal.gameObject.GetComponent<BoxCollider2D>();
        if (Physics2D.IsTouching(barBox, goalBox))
        {
            Debug.Log("yay"); 
        }
    }
}
