using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnxietyGame : MonoBehaviour
{
    [SerializeField] Transform leftPivot;
    [SerializeField] Transform rightPivot;

    [SerializeField] Transform bar;
    [SerializeField] Transform goal;

    private void Start()
    {
        goal.position = new Vector2(Random.Range(leftPivot.position.x + 100, rightPivot.position.x), goal.position.y);
    }

    private void Update()
    {
        
    }
}
