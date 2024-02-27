using System.Collections;
using UnityEngine;

public class StackingGame : MonoBehaviour
{
    [SerializeField] private Transform blockPrefab;
    [SerializeField] private Transform blockHolder;

    private Transform currentBlock = null;
    private Rigidbody2D currentRigidbody;

    private Vector2 blockStartPosition = new Vector2(0f, 4f);

    private float blockSpeed = 8f;
    private float blockSpeedIncrement = 0.5f;
    private int blockDirection = 1;
    private float xLimit = 5;

    private float timeBetweenRounds = 1f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnNewBlock();
    }

    private void SpawnNewBlock()
    {
        // Create a block with the desired properties.
        currentBlock = Instantiate(blockPrefab, blockHolder);
        currentBlock.position = blockStartPosition;
        currentBlock.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
        currentRigidbody = currentBlock.GetComponent<Rigidbody2D>();
        // Increase the block speed each time to make it harder.
        blockSpeed += blockSpeedIncrement;
    }

    private IEnumerator DelayedSpawn()
    {
        yield return new WaitForSeconds(timeBetweenRounds);
        SpawnNewBlock();

    }

    // Update is called once per frame
    void Update()
    {
        // If we have a waiting block, move it about.
        if (currentBlock)
        {
            float moveAmount = Time.deltaTime * blockSpeed * blockDirection;
            currentBlock.position += new Vector3(moveAmount, 0, 0);
            // If we've gone as far as we want, reverse direction.
            if (Mathf.Abs(currentBlock.position.x) > xLimit)
            {
                // Set it to the limit so it doesn't go further.
                currentBlock.position = new Vector3(blockDirection * xLimit, currentBlock.position.y, 0);
                blockDirection = -blockDirection;
            }

            // If we press space drop the block.
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Stop it moving.
                currentBlock = null;
                // Activate the RigidBody to enable gravity to drop it.
                currentRigidbody.simulated = true;
                // Spawn the next block.
                StartCoroutine(DelayedSpawn());
            }
        }
    }
}