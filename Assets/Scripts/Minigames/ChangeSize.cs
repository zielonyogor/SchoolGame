using UnityEngine;

public class ChangeSize : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    private int tableSize = 100;

    private void Start()
    {
        tableSize = MiniGameManager.instance.tableSize;
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        spriteRenderer.size = new Vector2(tableSize,64f);
        boxCollider.size = spriteRenderer.bounds.size;
    }
}
