using UnityEngine;

public class TestSize : MonoBehaviour
{
    // Reference to the SpriteRenderer component
    private SpriteRenderer spriteRenderer;

    // Reference to the BoxCollider2D component
    private BoxCollider2D boxCollider;

    // Min and max scale values for randomizing
    public float minScale = 0.5f;
    public float maxScale = 2.0f;

    private void Start()
    {
        // Get references to SpriteRenderer and BoxCollider2D components
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        // Randomize sprite size
        RandomizeSize();
    }

    // Method to randomize sprite size
    private void RandomizeSize()
    {
        float randomSize = Random.Range(minScale, maxScale);

        spriteRenderer.size = new Vector2(randomSize,15f);

        boxCollider.size = spriteRenderer.bounds.size;
    }
}
