using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;

public class Puzzle : MonoBehaviour
{
    private Camera mainCamera;
    private InputAction mouseClick;

    private WaitForFixedUpdate waitForFixedUpdate;
    [SerializeField] float mouseDragTime = 1f;
    Vector2 velocity = Vector2.zero;

    public int numberOfPuzzles = 3;

    private void Awake()
    {
        mainCamera = Camera.main;
        mouseClick = new ActionMap().Gameplay.DragDrop;
        waitForFixedUpdate = new WaitForFixedUpdate();
    }

    private void Start()
    {
        StartCoroutine(Timer.instance.DecreaseTimer(10f));
    }

    private void OnEnable()
    {
        mouseClick.Enable();
        mouseClick.performed += OnDragDrop;
        Timer.instance.OnTimeUp += GameEnd;
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
            Debug.Log("yay");
            clickedObject.tag = "Untagged";
            numberOfPuzzles--;
        }
        if (numberOfPuzzles == 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("LevelMenu");
        }
    }

    private void GameEnd()
    {
        Timer.instance.DisableTimer();
        Timer.instance.OnTimeUp -= GameEnd;
    }
}