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

    private Animator gameAnimator;

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
        gameAnimator = GetComponent<Animator>();
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
        RectTransform rectBar = bar.GetComponent<RectTransform>();
        RectTransform rectGoal = goal.GetComponent<RectTransform>();
        if (RectOverlaps(rectBar,rectGoal))
        {
            StartCoroutine(DelayWin());
        }
        else
        {
            StartCoroutine(DelayLoss());
        }
    }

    private IEnumerator DelayWin()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private IEnumerator DelayLoss()
    {
        MiniGameManager.instance.AddError();
        gameAnimator.SetTrigger("error");
        yield return new WaitForSeconds(0.01f);

        while (gameAnimator.GetCurrentAnimatorStateInfo(0).IsName("Anxiety_Error"))
            yield return null;
        StartCoroutine(MoveBar());
    }
    bool RectOverlaps(RectTransform rectTrans1, RectTransform rectTrans2)
    {
        Vector3[] corners1 = new Vector3[4];
        rectTrans1.GetWorldCorners(corners1);
        Rect rect1 = new Rect(corners1[0], corners1[2] - corners1[0]);
        Vector3[] corners2 = new Vector3[4];
        rectTrans2.GetWorldCorners(corners2);
        Rect rect2 = new Rect(corners2[0], corners2[2] - corners2[0]);

        if (rect1.Overlaps(rect2))
        {
            return true;
        }
        return false;
    }
}
