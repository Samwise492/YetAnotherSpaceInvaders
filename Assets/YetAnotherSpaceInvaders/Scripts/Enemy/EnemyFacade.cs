using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyFacade : MonoBehaviour, IDamageable
{
    public Enemy ConcreteEnemy { get; private set; }

    private Pool enemyPool;
    private SignalBus signalBus;

    private int currentHealth;

    [Inject]
    public void Construct(
        Enemy enemy,
        Pool enemyPool,
        SignalBus signalBus
    )
    {
        ConcreteEnemy = enemy;
        this.enemyPool = enemyPool;
        this.signalBus = signalBus;

        currentHealth = enemy.GetTunables.Health;

    }

    public Vector3 GetPosition
    {
        get
        {
            return ConcreteEnemy.GetPosition;
        }
        set
        {
            ConcreteEnemy.GetPosition = value;
        }
    }

    public Vector3 GetSize
    {
        get
        {
            return ConcreteEnemy.GetSize;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            enemyPool.currentPool.Remove(this);
            enemyPool.Despawn(this);

            signalBus.Fire(new EnemyDestroyedSignal() { points = ConcreteEnemy.GetTunables.Score });
        }
    }

    public void ResetTunables(EnemyTunables newTunables)
    {
        ConcreteEnemy.GetTunables = newTunables;
    }

    public class Pool : MonoMemoryPool<EnemyTunables, EnemyFacade>
    {
        public List<EnemyFacade> currentPool = new();
        public Dictionary<int, EnemyFacade> allSpawnedEnemies = new();

        public bool isAllSpawned;

        protected override void Reinitialize(
            EnemyTunables tunables,
            EnemyFacade enemyFacade
        )
        {
            enemyFacade.ResetTunables(tunables);
        }
    }
}
