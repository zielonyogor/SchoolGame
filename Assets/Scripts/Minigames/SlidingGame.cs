using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SlidingGame : BaseMiniGame
{
    private bool isMoving = false;
    private WaitForFixedUpdate waitForFixedUpdate;

    [SerializeField] Rigidbody2D player;

    private InputAction turnHorizontal, turnVertical;
    [SerializeField] LayerMask blockMask;

    [SerializeField] float moveSpeed = 20f;

    SpriteRenderer sRenderer;
    float spriteWidth;

    private void Start()
    {
        waitForFixedUpdate = new WaitForFixedUpdate();
        sRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = sRenderer.sprite.bounds.size.x * transform.lossyScale.x / 2;

        gameTime = MiniGameManager.instance.time;
        StartCoroutine(PlayCountdown());
    }
    public override IEnumerator PlayCountdown()
    {
        yield return StartCoroutine(base.PlayCountdown());

        turnHorizontal.performed += SlideHorizontal;
        turnVertical.performed += SlideVertical;

        timer.OnTimeUp += GameEnd;
        StartCoroutine(timer.DecreaseTimer(gameTime));
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
            if (Mathf.Abs(newPosition.x) > 140 || Mathf.Abs(newPosition.y) > 90)
            {
                GameEnd();
            }
            yield return waitForFixedUpdate;

        } while (isMoving);
        isMoving = false;
    }

    public override void GameEnd()
    {
        StopAllCoroutines();
        isMoving = false;
        timer.OnTimeUp -= GameEnd;
        base.GameEnd();
    }

    public override void GameFinished()
    {
        StopAllCoroutines();
        isMoving = false;
        turnHorizontal.performed -= SlideHorizontal;
        turnVertical.performed -= SlideVertical;
        timer.OnTimeUp -= GameEnd;
        base.GameFinished();
    }
}
