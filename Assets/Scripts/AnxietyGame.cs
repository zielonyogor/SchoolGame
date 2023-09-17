using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;

public class AnxietyGame : MonoBehaviour
{
    private InputAction spaceAction;

    [SerializeField] Transform leftPivot;
    [SerializeField] Transform rightPivot;


    [SerializeField] Transform goal;

    [SerializeField] Transform bar;
    float barPosition;
    [SerializeField] float barSize = 0.1f;
    private float barProgress;
    private float barPullVelocity;
    [SerializeField] float barPullPower = 0.01f;
    [SerializeField] float barGravityPower = 0.005f;
    private void Awake()
    {
        spaceAction = new ActionMap().Gameplay.MoveBar;
    }
    private void Start()
    {
        goal.position = new Vector2(Random.Range(leftPivot.position.x + 100, rightPivot.position.x), goal.position.y);
    }

    private void OnEnable()
    {
        spaceAction.Enable();
        spaceAction.performed += Move;
    }

    private void OnDisable()
    {
        spaceAction.performed -= Move;
        spaceAction.Disable();
    }

    private void Update()
    {
        barPullVelocity -= barGravityPower * Time.deltaTime;
        barPosition += barPullVelocity;
        barPosition = Mathf.Clamp(barPosition, barSize/2, 1 - barSize/2);
        bar.position = Vector3.Lerp(leftPivot.position, rightPivot.position, barPosition);
        if(bar.position.x == leftPivot.position.x || bar.position.x == rightPivot.position.x)
        {
            barPullVelocity = 0;
        }
    }

    //private void Update()
    //{
    //    barPullVelocity -= barGravityPower * Time.deltaTime;

    //    barPosition += barPullVelocity;
    //    barPosition = Mathf.Clamp(barPosition, 0, 1);
    //    bar.position = Vector3.Lerp(leftPivot.position, rightPivot.position, barPosition);
    //}
    private void Move(InputAction.CallbackContext context)
    {
        barPullVelocity = barPullPower;
    }
}
