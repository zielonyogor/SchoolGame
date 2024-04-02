using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class DragDrop : MonoBehaviour, IMiniGame
{
    private Camera mainCamera;
    private InputAction mouseClick;

    private WaitForFixedUpdate waitForFixedUpdate;
    Vector2 velocity = Vector2.zero;
    [SerializeField] float mouseDragTime = 1f;

    [SerializeField] private BoxCollider2D handCollider;
    [SerializeField] private int goodItems = 3;

    [SerializeField] ParticleSystem confetti_1, confetti_2;
    [SerializeField] Timer timer;
    private void Awake()
    {
        mainCamera = Camera.main;
        mouseClick = new ActionMap().Gameplay.DragDrop;
        waitForFixedUpdate = new WaitForFixedUpdate();
    }

    private void Start()
    {
        StartCoroutine(timer.DecreaseTimer(MiniGameManager.instance.time));
    }

    private void OnEnable()
    {
        mouseClick.Enable();
        mouseClick.performed += OnDragDrop;
        timer.OnTimeUp += GameEnd;
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
            if (clickedObject.gameObject.CompareTag("DraggableCorrect"))
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

    public void GameEnd()
    {
        timer.DisableTimer();
        timer.OnTimeUp -= GameEnd;
        MiniGameManager.instance.HandleGameLoss();
    }

    public void GameFinished()
    {
        StartCoroutine(PlayConfetti());
    }

    private IEnumerator PlayConfetti()
    {
        yield return new WaitForEndOfFrame();
        timer.DisableTimer();
        confetti_1.Play();
        confetti_2.Play();
        yield return new WaitForSeconds(2.5f);
        MiniGameManager.instance.NextLevel();
    }
}
