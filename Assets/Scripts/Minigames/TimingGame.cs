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
        RectTransform rectBar = bar.GetComponent<RectTransform>();
        RectTransform rectGoal = goal.GetComponent<RectTransform>();
        if (rectOverlaps(rectBar,rectGoal))
        {
            Debug.Log("yay");
            StartCoroutine(DelayWin());
        }
        else
        {
            Debug.Log("does not");
            StartCoroutine(DelayLoss());
        }
    }

    private IEnumerator DelayWin()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    private IEnumerator DelayLoss()
    {
        yield return new WaitForSeconds(0.1f);
        MiniGameManager.instance.AddError(); //add here some animation??
        StartCoroutine(MoveBar());
    }
    bool rectOverlaps(RectTransform rectTrans1, RectTransform rectTrans2)
    {
        Vector3 position1 = rectTrans1.position;
        Vector3 position2 = rectTrans2.position;

        Rect rect1 = new Rect(position1.x - rectTrans1.rect.width / 2, position1.y - rectTrans1.rect.height / 2, rectTrans1.rect.width, rectTrans1.rect.height);
        Rect rect2 = new Rect(position2.x - rectTrans2.rect.width / 2, position2.y - rectTrans2.rect.height / 2, rectTrans2.rect.width, rectTrans2.rect.height);

        return rect1.Overlaps(rect2);
    }
}
