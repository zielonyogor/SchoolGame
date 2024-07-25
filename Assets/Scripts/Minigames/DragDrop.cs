using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class DragDrop : BaseMiniGame
{
    private Camera mainCamera;
    private InputAction mouseClick;

    Vector2 velocity = Vector2.zero;
    private WaitForFixedUpdate waitForFixedUpdate;

    [SerializeField] float mouseDragTime = 1f;
    [SerializeField] private BoxCollider2D handCollider;

    private int goodItems = 3;

    private void Awake()
    {
        mainCamera = Camera.main;
        mouseClick = new ActionMap().Gameplay.DragDrop;
        waitForFixedUpdate = new WaitForFixedUpdate();
    }

    private void Start()
    {
        goodItems = MiniGameManager.instance.numberOfMeds;
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
            if( hit.collider.gameObject.CompareTag("DraggableIncorrect") || hit.collider.gameObject.CompareTag("DraggableCorrect"))
            {
                StartCoroutine(DragUpdate(hit.collider.gameObject));
            }
        }
    }

    private IEnumerator DragUpdate(GameObject clickedObject)
    {
        while(mouseClick.ReadValue<float>() != 0)
        {
            Vector2 point = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            clickedObject.transform.position = Vector2.SmoothDamp(clickedObject.transform.position, point, ref velocity, mouseDragTime);
            yield return waitForFixedUpdate;
        }
        CheckCollision(clickedObject);
    }

    private void CheckCollision(GameObject clickedObject)
    {
        BoxCollider2D collider = clickedObject.GetComponent<BoxCollider2D>();
        if (Physics2D.IsTouching(collider, handCollider))
        {
            if (clickedObject.CompareTag("DraggableCorrect"))
            {
                goodItems--;
                clickedObject.tag = "Untagged";
                if (goodItems == 0)
                {
                    GameFinished();
                }
            }
            else
            {
                GameEnd();
            }
        }
    }

    public override void GameEnd()
    {
        timer.OnTimeUp -= GameEnd;
        base.GameEnd();
    }
}
