using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


//This is an old script, propably useless
public class InputManager : MonoBehaviour
{
    //public static InputManager instance;
    //private PlayerInput playerInput;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float tiltForce = 5f;

    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Already existing singleton of InputManager");
    //    }
    //    playerInput = GetComponent<PlayerInput>();

    //}

    //public static InputManager GetInstance()
    //{
    //    return instance;
    //}

    public void OnTilt(InputValue value)
    {
        float tiltValue = value.Get<float>();
        if (tiltValue != 0)
        {
           Debug.Log("tilt action");
            //Vector2 force = new Vector2(tiltValue * tiltForce, 0.0f);
            //Debug.Log(force);
            rb.AddTorque(tiltValue * tiltForce);
            //Vector2 topCenter = new Vector2(transform.position.x, transform.position.y + (transform.localScale.y / 2));
            //Vector2 forceVector = new Vector2(tiltValue * tiltForce, 0.0f);
            //rb.AddForceAtPosition(forceVector, topCenter);
        }
    }

}
