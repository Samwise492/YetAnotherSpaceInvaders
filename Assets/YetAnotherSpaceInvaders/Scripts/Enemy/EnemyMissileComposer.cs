using UnityEngine;
using Zenject;

public class EnemyMissileComposer : IFixedTickable
{
    private readonly EnemyFacade.Pool enemyFactory;
    private readonly EnemyMissileHandler.Settings enemyMissileHandlerSettings;
    private readonly EnemySpawner.Settings enemySpawnerSettings;

    private float lastShootTime;
    private bool isEnemyChosen;
    private Enemy chosenEnemy;

    public EnemyMissileComposer(
        EnemyFacade.Pool enemyFactory, 
        EnemyMissileHandler.Settings enemyMissileHandlerSettings,
        EnemySpawner.Settings enemySpawnerSettings
        )
    {
        this.enemyFactory = enemyFactory;
        this.enemyMissileHandlerSettings = enemyMissileHandlerSettings;
        this.enemySpawnerSettings = enemySpawnerSettings;
    }

    public void FixedTick()
    {
        if (Time.realtimeSinceStartup > lastShootTime && !isEnemyChosen)
        {
            PickEnemy();
        }

        if (Time.realtimeSinceStartup - lastShootTime > enemyMissileHandlerSettings.delayBetweenShoots && chosenEnemy != null)
        {
            lastShootTime = Time.realtimeSinceStartup;

            isEnemyChosen = false;
            chosenEnemy.isAbleToShoot = true;
        }
    }

    private void PickEnemy()
    {
        if (!enemyFactory.isAllSpawned)
        {
            return;
        }

        int randomIndex = Random.Range(0, enemyFactory.currentPool.Count);

        EnemyFacade _enemy = enemyFactory.currentPool[randomIndex];

        int enemyRow = (randomIndex / enemySpawnerSettings.EnemiesPerRow) + 1;
        int howManyRowsInFront = enemyRow - 1;

        chosenEnemy = _enemy.ConcreteEnemy;

        if (howManyRowsInFront > 0)
        {
            for (int i = 0; i < howManyRowsInFront; i++)
            {
                EnemyFacade enemyInFront = enemyFactory.allSpawnedEnemies[i * randomIndex];

                if (!enemyFactory.currentPool.Contains(enemyInFront))
                {
                    isEnemyChosen = true;
                }
            }
        }
        else if (howManyRowsInFront == 0)
        {
            isEnemyChosen = true;
        }
    }
}