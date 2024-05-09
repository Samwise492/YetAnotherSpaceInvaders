using UnityEngine;

public class Ship : MovableObject, IInteractiveObject, IShip
{
    private readonly Transform laserSpawnPoint;

    public Ship(GameObject rootObject, Collider2D collider2d, SpriteRenderer spriteRenderer, Transform laserSpawnPoint) :
        base(rootObject, collider2d, spriteRenderer)
    {
        this.laserSpawnPoint = laserSpawnPoint;
    }

    public Vector3 GetPosition
    {
        get
        {
            return rootObject.transform.position;
        }
        set
        {
            rootObject.transform.position = value;
        }
    }

    public Vector3 GetSize
    {
        get
        {
            return spriteRenderer.bounds.size;
        }
    }

    public Transform GetMissileSpawnPoint
    {
        get
        {
            return laserSpawnPoint;
        }
    }
}