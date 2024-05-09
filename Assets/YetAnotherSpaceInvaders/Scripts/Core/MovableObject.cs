using UnityEngine;

public abstract class MovableObject : IMovableObject
{
    protected readonly GameObject rootObject;
    protected readonly Collider2D collider2d;
    protected readonly SpriteRenderer spriteRenderer;

    public MovableObject(
        GameObject rootObject,
        Collider2D collider2d,
        SpriteRenderer spriteRenderer
    )
    {
        this.rootObject = rootObject;
        this.collider2d = collider2d;
        this.spriteRenderer = spriteRenderer;
    }

    public GameObject GetRootObject
    {
        get
        {
            return rootObject;
        }
    }

    public Collider2D GetCollider2D
    {
        get
        {
            return collider2d;
        }
    }

    public SpriteRenderer GetSpriteRenderer
    {
        get
        {
            return spriteRenderer;
        }
    }
}
