using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    private int numberOfPuzzles;

    private Camera mainCamera;

    private List<Transform> puzzlePieces = new List<Transform>();
    private List<Transform> puzzleHoles = new List<Transform>();

    public float bufferZone = 4f; 

    void Start()
    {
        mainCamera = Camera.main;
        numberOfPuzzles = MiniGameManager.instance.numberOfPuzzles;
        for(int i = 0; i < numberOfPuzzles; i++)
        {
            puzzleHoles.Add(transform.GetChild(i));
            puzzlePieces.Add(transform.GetChild(i).GetChild(0));
            transform.GetChild(i).gameObject.SetActive(true);
        }

        ChangePositions();
    }


    //definitely improve this one, kinda wacky rn
    void ChangePositions()
    {

        foreach (Transform child in puzzleHoles)
        {
            float viewportWidth = mainCamera.orthographicSize * 2f * mainCamera.aspect;
            float viewportHeight = mainCamera.orthographicSize * 2f;

            // Generate random coordinates scaled by the viewport size
            float randomX = Random.Range(-viewportWidth / 2f + bufferZone, viewportWidth / 2f - bufferZone);
            float randomY = Random.Range(-viewportHeight / 2f + bufferZone, viewportHeight / 2f - bufferZone);

            Vector2 randomWorldPosition = new Vector2(randomX, randomY);

            child.position = randomWorldPosition;
        }

        foreach (Transform puzzle in puzzlePieces)
        {
            float viewportWidth = mainCamera.orthographicSize * 2f * mainCamera.aspect;
            float viewportHeight = mainCamera.orthographicSize * 2f;

            // Generate random coordinates scaled by the viewport size
            float randomX = Random.Range(-viewportWidth / 2f + bufferZone, viewportWidth / 2f - bufferZone);
            float randomY = Random.Range(-viewportHeight / 2f + bufferZone, viewportHeight / 2f - bufferZone);

            Vector2 randomWorldPosition = new Vector2(randomX, randomY);

            puzzle.position = randomWorldPosition;
        }
    }
}
