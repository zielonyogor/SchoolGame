using UnityEngine;

public class ChangeSize : MonoBehaviour
{
    // Reference to the SpriteRenderer component
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    [SerializeField] int tableSize = 100;

    private void Start()
    {
        tableSize = MiniGameManager.instance.tableSize;
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        spriteRenderer.size = new Vector2(tableSize,24f);
        boxCollider.size = spriteRenderer.bounds.size;
    }
}
