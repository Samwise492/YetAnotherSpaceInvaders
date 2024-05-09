using UnityEngine;
using Zenject;

public class EnemyInstaller : MonoInstaller<EnemyInstaller>
{
    [SerializeField]
    private Settings settings;

    public override void InstallBindings()
    {
        Container.Bind<EnemyTunables>().AsSingle();
        Container.Bind<Enemy>().AsSingle()
            .WithArguments(
                settings.RootObject,
                settings.Collider2D,
                settings.SpriteRenderer,
                settings.MissileSpawnPoint
            );

        Container.BindInterfacesAndSelfTo<EnemyMoveHandler>().AsSingle();
        Container.BindInterfacesTo<EnemyMissileHandler>().AsSingle();
    }

    [System.Serializable]
    public class Settings
    {
        public GameObject RootObject => rootObject;
        public Collider2D Collider2D => collider2D;
        public SpriteRenderer SpriteRenderer => spriteRenderer;
        public Transform MissileSpawnPoint => missileSpawnPoint;

        [SerializeField]
        private GameObject rootObject;
        [SerializeField]
        private Collider2D collider2D;
        [SerializeField]
        private SpriteRenderer spriteRenderer;
        [SerializeField]
        private Transform missileSpawnPoint;
    }
}