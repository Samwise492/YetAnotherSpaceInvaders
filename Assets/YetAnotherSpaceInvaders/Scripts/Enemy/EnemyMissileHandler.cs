using System;
using System.Diagnostics;
using Zenject;

public class EnemyMissileHandler : IFixedTickable
{
    private readonly EnemyCommonSettings enemyCommonSettings;
    private readonly MissileFacade.Pool laserPool;
    private readonly Settings settings;
    private readonly Enemy enemy;

    public EnemyMissileHandler(
        Enemy enemy,
        Settings settings,
        MissileFacade.Pool laserPool,
        EnemyCommonSettings enemyCommonSettings
    )
    {
        this.enemy = enemy;
        this.settings = settings;
        this.laserPool = laserPool;
        this.enemyCommonSettings = enemyCommonSettings;
    }

    public void FixedTick()
    {
        if (enemy.isAbleToShoot)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        var tunables = new MissileTunables
        {
            type = MissileType.EnemyMissile,
            spawnPoint = enemy.GetMissileSpawnPoint.position,
            velocity = -settings.Velocity,
            hitPoint = enemyCommonSettings.HitPoint
        };

        laserPool.Spawn(tunables);

        enemy.isAbleToShoot = false;
    }

    [Serializable]
    public class Settings
    {
        public float delayBetweenShoots;
        public int Velocity;
    }
}