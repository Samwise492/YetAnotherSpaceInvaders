using UnityEngine;

public interface IMovableObject
{
    public GameObject GetRootObject { get; }
    public Collider2D GetCollider2D { get; }
    public SpriteRenderer GetSpriteRenderer { get; }
}