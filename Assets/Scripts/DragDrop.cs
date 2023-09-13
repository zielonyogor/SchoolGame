using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class DragDrop : MonoBehaviour
{
    private ActionMap actionMap;
    private Camera mainCamera;
    private InputAction mouseClick;

    private WaitForFixedUpdate waitForFixedUpdate;
    [SerializeField] float mouseDragTime = 1f;
    Vector2 velocity = Vector2.zero;
    private void Awake()
    {
        actionMap = new ActionMap();
        mainCamera = Camera.main;
        mouseClick = actionMap.Gameplay.DragDrop;
        waitForFixedUpdate = new WaitForFixedUpdate();
    }

    private void OnEnable()
    {
        actionMap.Enable();
        mouseClick.performed += OnDragDrop;
    }
    private void OnDisable()
    {
        mouseClick.performed -= OnDragDrop;
        actionMap.Disable();
    }

    private void OnDragDrop(InputAction.CallbackContext context)
    {
        Vector2 point = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero);

        if (hit.collider != null)
        {
            Debug.Log("ciagnij");
            StartCoroutine(DragUpdate(hit.collider.gameObject));
        }
    }

    private IEnumerator DragUpdate(GameObject clickedObject)
    {
        //float initialDistance = Vector2.Distance(clickedObject.transform.position, mainCamera.transform.position);
        //Debug.Log(initialDistance)
        while(mouseClick.ReadValue<float>() != 0)
        {
            Vector2 point = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            clickedObject.transform.position = Vector2.SmoothDamp(clickedObject.transform.position, point, ref velocity, mouseDragTime);
            yield return waitForFixedUpdate;
        }
        yield return null;
    }
    /*
      Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                Debug.Log("essa");
            }
        }*/
}
