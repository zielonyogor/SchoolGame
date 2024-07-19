using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SlidingGame : MonoBehaviour, IMiniGame
{
    public bool HasTimingGame{ get; set;}

    private bool isMoving = false;
    private WaitForFixedUpdate waitForFixedUpdate;

    [SerializeField] Rigidbody2D player;

    private InputAction turnHorizontal, turnVertical;
    [SerializeField] LayerMask blockMask;

    [SerializeField] float moveSpeed = 20f;

    SpriteRenderer sRenderer;
    float spriteWidth;

    [Header("Extras")]
    [SerializeField] Timer timer;
    [SerializeField] ParticleSystem confetti_1, confetti_2;
    [SerializeField] GameObject timingGame;

    [Header("Countdown")]
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject countdown;

    private void Start()
    {
        waitForFixedUpdate = new WaitForFixedUpdate();
        sRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = sRenderer.sprite.bounds.size.x * transform.lossyScale.x / 2;

        StartCoroutine(PlayCountdown());
    }
    public IEnumerator PlayCountdown()
    {
        GameObject spawnedObject = Instantiate(countdown, canvas.transform);
        Animator animator = spawnedObject.GetComponent<Animator>();
        while (animator && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f) 
            yield return null;

        turnHorizontal.performed += SlideHorizontal;
        turnVertical.performed += SlideVertical;

        timer.OnTimeUp += GameEnd;
        StartCoroutine(timer.DecreaseTimer(MiniGameManager.instance.time));
    }

    private void Awake()
    {
        turnHorizontal = new ActionMap().Gameplay.SlideHorizontal;
        turnVertical = new ActionMap().Gameplay.SlideVertical;
    }

    private void OnEnable()
    {
        turnHorizontal.Enable();
        turnVertical.Enable();
    }

    private void OnDisable()
    {
        turnHorizontal.performed -= SlideHorizontal;
        turnVertical.performed -= SlideVertical;
        turnHorizontal.Disable();
        turnVertical.Disable();
    }

    void SlideHorizontal(InputAction.CallbackContext context)
    {
        
        if (!isMoving)
        {
            isMoving = true;
            StartCoroutine(Move(new Vector3(context.ReadValue<float>(), 0)));
        }
    }

    void SlideVertical(InputAction.CallbackContext context)
    {
        if (!isMoving)
        {
            isMoving = true;
            StartCoroutine(Move(new Vector3(0, context.ReadValue<float>())));
        }
    }

    private IEnumerator Move(Vector2 vec)
    {
        Vector2 currentPosition;
        RaycastHit2D hit;
        do
        {
            currentPosition = player.transform.position;
            Vector2 newPosition = currentPosition + moveSpeed * vec;
            hit = Physics2D.Raycast(currentPosition, vec, spriteWidth + 0.03f, blockMask);

            if (hit.collider != null)
            {
                break;
            }
            player.MovePosition(newPosition);
            if (Mathf.Abs(newPosition.x) > 110 || Mathf.Abs(newPosition.y) > 70)
            {
                GameEnd();
            }
            yield return waitForFixedUpdate;

        } while (isMoving);
        isMoving = false;
    }

    public void GameEnd()
    {
        StopAllCoroutines();
        timer.DisableTimer();
        timer.OnTimeUp -= GameEnd;
        MiniGameManager.instance.HandleGameLoss();
    }

    public void GameFinished()
    {
        StopAllCoroutines();
        StartCoroutine(PlayConfetti());
    }

    private IEnumerator PlayConfetti()
    {
        turnHorizontal.performed -= SlideHorizontal;
        turnVertical.performed -= SlideVertical;
        isMoving = false;

        timer.DisableTimer();
        yield return new WaitUntil(() => HasTimingGame == false);
        Destroy(timingGame);
        confetti_1.Play();
        confetti_2.Play();
        yield return new WaitForSeconds(2.5f);
        MiniGameManager.instance.NextLevel();
    }
}
