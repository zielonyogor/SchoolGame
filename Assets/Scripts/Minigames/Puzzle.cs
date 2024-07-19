using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;

public class Puzzle : MonoBehaviour, IMiniGame
{
    public bool HasTimingGame{get; set;}

    private Camera mainCamera;
    private InputAction mouseClick;

    private WaitForFixedUpdate waitForFixedUpdate;
    [SerializeField] float mouseDragTime = 1f;
    Vector2 velocity = Vector2.zero;

    private int numberOfPuzzles;

    [Header("Extras")]
    [SerializeField] Timer timer;
    [SerializeField] ParticleSystem confetti_1, confetti_2;
    [SerializeField] GameObject timingGame;

    [Header("Countdown")]
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject countdown;

    private void Awake()
    {
        mainCamera = Camera.main;
        mouseClick = new ActionMap().Gameplay.DragDrop;
        waitForFixedUpdate = new WaitForFixedUpdate();
    }

    private void Start()
    {
        numberOfPuzzles = MiniGameManager.instance.numberOfPuzzles;
        StartCoroutine(PlayCountdown());
    }
    public IEnumerator PlayCountdown()
    {
        GameObject spawnedObject = Instantiate(countdown, canvas.transform);
        Animator animator = spawnedObject.GetComponent<Animator>();
        while (animator && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            yield return null;

        mouseClick.performed += OnDragDrop;
        timer.OnTimeUp += GameEnd;
        StartCoroutine(timer.DecreaseTimer(MiniGameManager.instance.time));
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
            clickedObject.transform.position = Vector2.SmoothDamp(clickedObject.transform.position, point, ref velocity, mouseDragTime);
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
        }
        if (numberOfPuzzles == 0)
        {
            GameFinished();
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
        yield return new WaitUntil(() => HasTimingGame == false);
        Destroy(timingGame);
        confetti_1.Play();
        confetti_2.Play();
        yield return new WaitForSeconds(2.5f);
        MiniGameManager.instance.NextLevel();
    }
}
