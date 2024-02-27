using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SlidingGame : MonoBehaviour
{
    private bool isMoving = false;
    private WaitForFixedUpdate waitForFixedUpdate;

    [SerializeField] Rigidbody2D player;

    private void Start()
    {
        waitForFixedUpdate = new WaitForFixedUpdate();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && !isMoving)
        {
            //isMoving = true;
            Debug.Log("prawo");
            player.AddForce( new Vector2(100f, 0f));

        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && !isMoving)
        {
            //isMoving = true;
            Debug.Log("lewo");

            player.AddForce(new Vector2(-100f, 0f));
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && !isMoving)
        {
            //isMoving = true;
            Debug.Log("up");

            player.AddForce(new Vector2(0f, 100f));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && !isMoving)
        {
            //isMoving = true;
            Debug.Log("dul");

            player.AddForce(new Vector2(0f, -100f));
        }
    }

    //private IEnumerator MovePlayer(int dir)
    //{
    //    while ()
    //    yield return new WaitForFixedUpdate();
    //}

}
