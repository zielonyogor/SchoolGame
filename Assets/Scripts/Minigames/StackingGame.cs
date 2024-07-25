using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StackingGame : BaseMiniGame
{
    private InputAction dropAction;

    [SerializeField] private Transform blockPrefab;
    [SerializeField] private Transform blockHolder;

    private Transform currentBlock = null;
    private Rigidbody2D currentRigidbody;

    private Vector2 blockStartPosition = new Vector2(0f, 20f);

    private int bookDirection = 1;
    private float xLimit = 70;
    private float timeBetweenRounds = 1f;

    [Header("Book Sprites")]
    [SerializeField] private List<Sprite> spriteList = new List<Sprite>();

    //MiniGame Manager variables
    private float bookSpeed = 30f;
    private int numberOfBooks = 4;

    private void Awake()
    {
        dropAction = new ActionMap().Gameplay.DropBook;
    }
    private void OnEnable()
    {
        dropAction.Enable();
    }
    private void OnDisable()
    {
        dropAction.performed -= DropBook;
        dropAction.Disable();
    }

    void Start()
    {
        timer.isDecreasing = false;
        numberOfBooks = MiniGameManager.instance.numberOfBooks;
        bookSpeed = MiniGameManager.instance.bookSpeed;


        gameTime = 60 / MiniGameManager.instance.time + 1;

        StartCoroutine(PlayCountdown());
    }

    public override IEnumerator PlayCountdown()
    {
        yield return StartCoroutine(base.PlayCountdown());

        dropAction.performed += DropBook;
        timer.OnTimeUp += GameFinished;
        SpawnNewBlock();
    }

    private void SpawnNewBlock()
    {
        currentBlock = Instantiate(blockPrefab, blockHolder);
        currentBlock.GetComponent<SpriteRenderer>().sprite = spriteList[(int)Random.Range(0, spriteList.Count)];
        currentBlock.position = blockStartPosition;
        SpriteRenderer currentBlockSprite = currentBlock.GetComponent<SpriteRenderer>();
        currentBlockSprite.size = new Vector2(Random.Range(25, 50), Random.Range(5, 10));
        currentBlock.GetComponent<BoxCollider2D>().size = currentBlockSprite.bounds.size;
        currentRigidbody = currentBlock.GetComponent<Rigidbody2D>();
    }

    private IEnumerator DelayedSpawn()
    {
        yield return new WaitForSeconds(timeBetweenRounds);
        blockStartPosition = new Vector2(Random.Range(-xLimit, xLimit), 20f);
        SpawnNewBlock();
        dropAction.performed += DropBook;
    }

    void FixedUpdate()
    {
        if (currentBlock)
        {
            float moveAmount = Time.deltaTime * bookSpeed * bookDirection;
            currentBlock.position += new Vector3(moveAmount, 0, 0);
            if (Mathf.Abs(currentBlock.position.x) > xLimit)
            {
                currentBlock.position = new Vector3(bookDirection * xLimit, currentBlock.position.y, 0);
                bookDirection = -bookDirection;
            }
        }
    }

    private void DropBook(InputAction.CallbackContext context)
    {
        numberOfBooks--;
        currentBlock = null;
        currentRigidbody.simulated = true;
        dropAction.performed -= DropBook;
        if (numberOfBooks == 0)
        {
            StartCoroutine(timer.DecreaseTimer(gameTime));
        }
        else
        {
            StartCoroutine(DelayedSpawn());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Book"))
        {
            GameEnd();
        }
    }

    public override void GameEnd()
    {
        timer.OnTimeUp -= GameFinished;
        base.GameEnd();
    }

    public override void GameFinished()
    {
        timer.OnTimeUp -= GameFinished;
        base.GameFinished();
    }
}