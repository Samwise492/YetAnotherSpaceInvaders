using UnityEngine;
using Zenject;

public class EnemySpawner : ITickable
{
    private readonly Settings settings;
    private readonly ScreenBoundary screenBoundary;
    private readonly EnemyFacade.Pool enemyFactory;
    private readonly EnemyCommonSettings enemyCommonSettings;
    private readonly SignalBus signalBus;

    private int spawnedEnemyCount;
    private float currentRowY;

    public EnemySpawner
    (
        Settings settings,
        EnemyCommonSettings enemyCommonSettings,
        ScreenBoundary screenBoundary,
        EnemyFacade.Pool enemyFactory,
        SignalBus signalBus
    )
    {
        this.settings = settings;
        this.enemyCommonSettings = enemyCommonSettings;
        this.screenBoundary = screenBoundary;
        this.enemyFactory = enemyFactory;
        this.signalBus = signalBus;

        currentRowY = settings.RowYPosition;
    }

    public void Tick()
    {
        if (spawnedEnemyCount < settings.EnemyCount)
        {
            for (int i = 0; i < settings.EnemiesPerRow; i++)
            {
                SpawnEnemy(i, currentRowY);
                spawnedEnemyCount++;
            }

            currentRowY += settings.YSpaceBetweenRows;
        }
        if (spawnedEnemyCount == settings.EnemyCount)
        {
            enemyFactory.isAllSpawned = true;
        }

        if (enemyFactory.currentPool.Count == 0)
        {
            spawnedEnemyCount = 0;
            currentRowY = settings.RowYPosition;
            enemyFactory.allSpawnedEnemies.Clear();

            signalBus.Fire(new EnemyLeapSignal() { direction = LeapDirection.None });
        }
    }

    private void SpawnEnemy(float vectorXMultiplier, float vectorYMultiplier)
    {
        EnemyTunables tunables = SetEnemySettings();
        EnemyFacade enemy = enemyFactory.Spawn(tunables);

        enemyFactory.currentPool.Add(enemy);
        enemyFactory.allSpawnedEnemies.Add(spawnedEnemyCount, enemy);

        enemy.GetPosition = SetSpawnPosition(enemy.GetSize, vectorXMultiplier, vectorYMultiplier);
    }

    private EnemyTunables SetEnemySettings()
    {
        return new EnemyTunables
        {
            Speed = settings.Velocity,
            RowLeap = settings.RowLeap,
            Health = enemyCommonSettings.Health,
            HitPoint = enemyCommonSettings.HitPoint,
            Score = enemyCommonSettings.Score
        };
    }

    private Vector3 SetSpawnPosition(Vector3 enemySize, float xSpawnPosition, float ySpawnPosition)
    {
        float sidePadding = screenBoundary.Left + enemySize.x;
        float topPadding = screenBoundary.Top;

        return new Vector3(
            sidePadding + xSpawnPosition * settings.XSpaceInRow, 
            topPadding + ySpawnPosition * settings.YSpaceBetweenRows, 
            0);
    }

    [System.Serializable]
    public class Settings
    {
        public float Velocity => velocity;
        public float RowLeap => rowLeap;
        public int EnemyCount => enemyCount;
        public int EnemiesPerRow => enemiesPerRow;
        public float XSpaceInRow => xSpaceInRow;
        public float YSpaceBetweenRows => ySpaceBetweenRows;
        public float RowYPosition => rowYPosition;

        [SerializeField]
        private float velocity;
        [SerializeField]
        private float rowLeap;

        [SerializeField]
        private int enemyCount;
        [SerializeField]
        private int enemiesPerRow;

        [SerializeField]
        private float xSpaceInRow;
        [SerializeField]
        private float ySpaceBetweenRows;

        [SerializeField]
        private float rowYPosition;
    }
}
