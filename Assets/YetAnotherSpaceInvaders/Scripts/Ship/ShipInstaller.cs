using UnityEngine;
using Zenject;

public class ShipInstaller : MonoInstaller<ShipInstaller>
{
    [SerializeField]
    private Settings settings;

    [SerializeField]
    private ShipMissileTypes missileDropChance;
    
    public override void InstallBindings()
    {
        Container.Bind<Ship>().AsSingle()
            .WithArguments(
            settings.RootObject,
            settings.Collider2D,
            settings.SpriteRenderer,
            settings.MissileSpawnPoint
        );

        Container.BindInstance(missileDropChance);

        Container.BindInterfacesTo<ShipMoveHandler>().AsSingle();
        Container.BindInterfacesAndSelfTo<ShipMissileHandler>().AsSingle();

        Container.BindSignal<EnemyDestroyedSignal>().ToMethod<ShipMissileHandler>(x => x.ChangeMissileType).FromResolve();
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