using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SlidingGame : MonoBehaviour /*, IMiniGame*/
{
    private bool isMoving = false;
    private WaitForFixedUpdate waitForFixedUpdate;

    [SerializeField] Rigidbody2D player;

    private InputAction turnHorizontal, turnVertical;
    [SerializeField] LayerMask blockMask;

    [SerializeField] float moveSpeed = 10f;

    SpriteRenderer sRenderer;
    float spriteWidth;


    private void Start()
    {
        waitForFixedUpdate = new WaitForFixedUpdate();
        sRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = sRenderer.sprite.bounds.size.x * transform.lossyScale.x / 2;
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
        turnHorizontal.performed += SlideHorizontal; 
        turnVertical.performed += SlideVertical;
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

            //Debugging Raycast
            Debug.DrawLine(currentPosition, currentPosition + ( spriteWidth + 0.03f) * vec, Color.green);

            if (hit.collider != null)
            {
                break;
            }
            player.MovePosition(newPosition);
            Debug.Log(Mathf.Abs(newPosition.y));
            if (Mathf.Abs(newPosition.x) > 110 || Mathf.Abs(newPosition.y) > 70)
            {
                Debug.Log("przegrama");
                SceneManager.LoadScene("LevelMenu");
            }
            yield return waitForFixedUpdate;

        } while (true);
        isMoving = false;
    }
}
