using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SlidingMapLayout : ScriptableObject
{
    public Vector2 playerPosition;
    public Vector2 finishPosition;

    public List<Vector2> blockPosition;
}
