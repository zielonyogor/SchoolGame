using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StackingGame : MonoBehaviour, IMiniGame
{
    [SerializeField] private Transform blockPrefab;
    [SerializeField] private Transform blockHolder;

    private Transform currentBlock = null;
    private Rigidbody2D currentRigidbody;

    private Vector2 blockStartPosition = new Vector2(0f, 0f);

    private int bookDirection = 1;
    private float xLimit = 70;

    private float timeBetweenRounds = 1f;

    private InputAction dropAction;

    [SerializeField] private List<Sprite> spriteList = new List<Sprite>();

    //MiniGame Manager variables
    private float bookSpeed = 30f;
    private int numberOfBooks = 4;

    [SerializeField] ParticleSystem confetti_1, confetti_2;

    private void Awake()
    {
        dropAction = new ActionMap().Gameplay.DropBook;
    }
    private void OnEnable()
    {
        dropAction.Enable();
        dropAction.performed += DropBook;
        Timer.instance.OnTimeUp += GameFinished;
    }
    private void OnDisable()
    {
        dropAction.performed -= DropBook;
        dropAction.Disable();
    }

    void Start()
    {
        Timer.instance.isDecreasing = false;

        numberOfBooks = MiniGameManager.instance.numberOfBooks;
        bookSpeed = MiniGameManager.instance.bookSpeed;

        SpawnNewBlock();
    }

    private void SpawnNewBlock()
    {
        currentBlock = Instantiate(blockPrefab, blockHolder);
        currentBlock.GetComponent<SpriteRenderer>().sprite = spriteList[(int)Random.Range(0, spriteList.Count)];
        currentBlock.position = blockStartPosition;
        currentRigidbody = currentBlock.GetComponent<Rigidbody2D>();
    }

    private IEnumerator DelayedSpawn()
    {
        yield return new WaitForSeconds(timeBetweenRounds);
        blockStartPosition = new Vector2(Random.Range(-xLimit, xLimit), 0);
        SpawnNewBlock();
        dropAction.performed += DropBook;
    }

    void FixedUpdate()
    {
        // If we have a waiting block, move it about.
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
            StartCoroutine(Timer.instance.DecreaseTimer(MiniGameManager.instance.time));
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

    public void GameEnd()
    {
        Timer.instance.DisableTimer();
        Timer.instance.OnTimeUp -= GameFinished;
        MiniGameManager.instance.HandleGameLoss();
    }

    public void GameFinished()
    {
        StartCoroutine(PlayConfetti());
    }

    private IEnumerator PlayConfetti()
    {
        yield return new WaitForEndOfFrame();
        Timer.instance.DisableTimer();
        confetti_1.Play();
        confetti_2.Play();
        yield return new WaitForSeconds(2.5f);
        MiniGameManager.instance.NextLevel();
    }
}