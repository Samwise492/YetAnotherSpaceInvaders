using System.Linq;
using UnityEngine;
using Zenject;

public class MissileFacade : MonoBehaviour
{
    private Missile missile;
    private Pool missilePool;
    private MissileTunables laserTunables;
    private ScreenBoundary screenBoundary;
    private MissileHitHandler laserHitHandler;

    [Inject]
    public void Construct(
        Missile missile,
        Pool missilePool,
        ScreenBoundary screenBoundary,
        MissileHitHandler laserHitHandler
    )
    {
        this.missile = missile;
        this.missilePool = missilePool;
        this.screenBoundary = screenBoundary;
        this.laserHitHandler = laserHitHandler;
    }

    public MissileType GetMissileType
    {
        get
        {
            return laserTunables.type;
        }
    }

    public int GetHitPoint
    {
        get
        {
            return laserTunables.hitPoint;
        }
    }

    public void ReInit(MissileTunables tunables)
    {
        laserTunables = tunables;
        missile.ChangeType(tunables.type);
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        laserHitHandler.MissileHit(other);
    }


    private void Update()
    {
        var newPosition = transform.position;
        newPosition.y += Time.deltaTime * laserTunables.velocity;
        transform.position = newPosition;

        if (transform.position.y < screenBoundary.Bottom || transform.position.y > screenBoundary.Top)
        {
            if (!missilePool.InactiveItems.Contains(this))
            {
                missilePool.Despawn(this);
            }
        }
    }

    public class Pool : MonoMemoryPool<MissileTunables, MissileFacade>
    {
        protected override void Reinitialize(MissileTunables missileTunables, MissileFacade laser)
        {
            base.Reinitialize(missileTunables, laser);
            laser.transform.position = missileTunables.spawnPoint;
            laser.ReInit(missileTunables);
        }
    }
}