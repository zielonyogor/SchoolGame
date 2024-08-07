using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;

public class Puzzle : BaseMiniGame
{
    private Camera mainCamera;
    private InputAction mouseClick;

    private WaitForFixedUpdate waitForFixedUpdate;
    [SerializeField] float mouseDragTime = 1f;
    Vector2 velocity = Vector2.zero;

    private int numberOfPuzzles;

    private void Awake()
    {
        mainCamera = Camera.main;
        mouseClick = new ActionMap().Gameplay.DragDrop;
        waitForFixedUpdate = new WaitForFixedUpdate();
    }

    private void Start()
    {
        numberOfPuzzles = MiniGameManager.instance.numberOfPuzzles;
        gameTime = MiniGameManager.instance.time;
        StartCoroutine(PlayCountdown());
    }
    public override IEnumerator PlayCountdown()
    {
        yield return StartCoroutine(base.PlayCountdown());

        mouseClick.performed += OnDragDrop;
        timer.OnTimeUp += GameEnd;
        StartCoroutine(timer.DecreaseTimer(gameTime));
    }

    private void OnEnable()
    {
        mouseClick.Enable();
    }
    private void OnDisable()
    {
        mouseClick.performed -= OnDragDrop;
        mouseClick.Disable();
    }

    private void OnDragDrop(InputAction.CallbackContext context)
    {
        Vector2 point = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("PuzzlePiece"))
            {
                StartCoroutine(DragUpdate(hit.collider.gameObject));
            }
        }
    }

    private IEnumerator DragUpdate(GameObject clickedObject)
    {
        while (mouseClick.ReadValue<float>() != 0)
        {
            Vector2 point = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            Vector2 targetPosition = Vector2.SmoothDamp(clickedObject.transform.position, point, ref velocity, mouseDragTime);
            targetPosition.x = Mathf.Clamp(targetPosition.x, -140, 140);
            targetPosition.y = Mathf.Clamp(targetPosition.y, -80, 80);
            clickedObject.transform.position = targetPosition;
            yield return waitForFixedUpdate;
        }
        CheckCollision(clickedObject);
    }

    private void CheckCollision(GameObject clickedObject)
    {
        BoxCollider2D collider = clickedObject.GetComponent<BoxCollider2D>();
        if (Physics2D.IsTouching(collider, clickedObject.transform.parent.GetComponent<BoxCollider2D>()))
        {
            clickedObject.tag = "Untagged";
            numberOfPuzzles--;
            clickedObject.transform.position = clickedObject.transform.parent.position;
        }
        if (numberOfPuzzles == 0)
        {
            GameFinished();
        }
    }

    public override void GameEnd()
    {
        timer.OnTimeUp -= GameEnd;
        base.GameEnd();
    }

    public override void GameFinished()
    {
        timer.OnTimeUp -= GameEnd;
        base.GameFinished();
    }
}