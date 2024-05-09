using System;
using System.Linq;
using UnityEngine;
using Zenject;

public class MissileInstaller : MonoInstaller<MissileInstaller>
{
    [SerializeField]
    private Settings settings;

    [SerializeField]
    private ShipMissileTypes shipMissileTypes;

    public override void InstallBindings()
    {
        Container.BindInstance(shipMissileTypes);
        Container.Bind<MissileTunables>().AsSingle();
        Container.Bind<MissileHitHandler>().AsSingle();

        Container.Bind<Missile>().AsSingle()
            .WithArguments(
                settings.SpriteRenderer,
                shipMissileTypes.MissileData.Where(x => x.MissileType == MissileType.ShipMissile).First().Sprite,
                shipMissileTypes.MissileData.Where(x => x.MissileType == MissileType.DoubleShipMissile).First().Sprite,
                shipMissileTypes.MissileData.Where(x => x.MissileType == MissileType.TripleShipMissle).First().Sprite,
                settings.EnemyMissileSprite 
        );

        Container.BindSignal<NewMissileTypeGotSignal>().ToMethod<ShipCurrentMissilePresenter>(x => x.SetSprite).FromResolve();
    }

    [Serializable]
    public class Settings
    {
        public GameObject RootObject => rootObject;
        public SpriteRenderer SpriteRenderer => spriteRenderer;
        public Sprite EnemyMissileSprite => enemyMissileSprite;

        [SerializeField]
        private GameObject rootObject;
        [SerializeField]
        private SpriteRenderer spriteRenderer;
        [SerializeField]
        private Sprite enemyMissileSprite;
    }
}


