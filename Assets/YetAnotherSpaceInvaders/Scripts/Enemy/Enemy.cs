using UnityEngine;

public class Enemy : MovableObject, IEnemy
{
    private readonly Transform laserSpawnPoint;

    private EnemyTunables enemyTunables;

    public bool isReadyForLeap;
    public bool isAbleToShoot;

    public Enemy(
        GameObject rootObject,
        Collider2D collider2d,
        SpriteRenderer spriteRenderer,
        Transform laserSpawnPoint,
        EnemyTunables enemyTunables
    ) : base(
        rootObject,
        collider2d,
        spriteRenderer
    )
    {
        this.laserSpawnPoint = laserSpawnPoint;
        this.enemyTunables = enemyTunables;
    }

    public EnemyTunables GetTunables
    {
        get
        {
            return enemyTunables;
        }
        set
        {
            enemyTunables = value;
        }
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
