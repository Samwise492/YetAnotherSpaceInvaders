using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MissileHitHandler
{
    private readonly MissileFacade missile;
    private readonly MissileFacade.Pool missilePool;
    private readonly ShipMissileTypes shipMissileTypes;

    private readonly List<MissileType> availableShipMissileTypes = new List<MissileType>();

    private readonly List<MissileType> enemyMissileTypes = new List<MissileType>()
    { 
        MissileType.EnemyMissile 
    };

    public MissileHitHandler(
        MissileFacade missile,
        MissileFacade.Pool missilePool,
        ShipMissileTypes shipMissileTypes
    )
    {
        this.missilePool = missilePool;
        this.missile = missile;
        this.shipMissileTypes = shipMissileTypes;

        AssignAvailableShipTypes();
    }

    public void MissileHit(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageable hit))
        {
            bool isHitEnemy = availableShipMissileTypes.Contains(missile.GetMissileType) && other.gameObject.GetComponent<EnemyFacade>();
            bool isHitShip = enemyMissileTypes.Contains(missile.GetMissileType) && other.gameObject.GetComponent<ShipFacade>();

            if (isHitEnemy || isHitShip)
            {
                hit.TakeDamage(missile.GetHitPoint);
            }

            if (!missilePool.InactiveItems.Contains(missile))
            {
                missilePool.Despawn(missile);
            }
        }
    }

    private void AssignAvailableShipTypes() 
    {
        foreach (MissileData data in shipMissileTypes.MissileData)
        {
            availableShipMissileTypes.Add(data.MissileType);
        }
    }
}