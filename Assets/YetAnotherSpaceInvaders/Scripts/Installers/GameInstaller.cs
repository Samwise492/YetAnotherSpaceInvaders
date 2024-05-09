using System;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller<GameInstaller>
{
    [Inject]
    private readonly Settings settings = null;

    public override void InstallBindings()
    {
        BindDependencies();

        BindPools();

        InstallSignals();
    }

    private void BindDependencies()
    {
        Container.Bind<ScreenBoundary>().AsSingle();
        Container.BindInterfacesTo<EnemyMissileComposer>().AsSingle();
        Container.BindInterfacesTo<EnemySpawner>().AsSingle();
        Container.BindInterfacesAndSelfTo<EnemyMoveComposer>().AsSingle();
    }

    private void BindPools()
    {
        Container.BindMemoryPool<EnemyFacade, EnemyFacade.Pool>()
            .WithInitialSize(10).ExpandByDoubling()
            .FromSubContainerResolve()
            .ByNewContextPrefab(settings.EnemyPrefab)
            .UnderTransformGroup("Enemies");

        Container.BindMemoryPool<MissileFacade, MissileFacade.Pool>()
            .WithInitialSize(10).ExpandByDoubling()
            .FromSubContainerResolve()
            .ByNewContextPrefab(settings.MissilePrefab)
            .UnderTransformGroup("Missiles");
    }

    private void InstallSignals()
    {
        Container.DeclareSignal<EnemyDestroyedSignal>();
        Container.DeclareSignal<EnemyLeapSignal>();
        Container.DeclareSignal<GameStateChangedSignal>();
        Container.DeclareSignal<NewMissileTypeGotSignal>();

        Container.BindSignal<EnemyLeapSignal>().ToMethod<EnemyMoveComposer>(x => x.RegulateMoveBehaviour).FromResolve();
        Container.BindSignal<EnemyDestroyedSignal>().ToMethod<ShipPointsPresenter>(x => x.ChangePoints).FromResolveAll();
        Container.BindSignal<GameStateChangedSignal>().ToMethod<GameStatePresenter>(x => x.SetState).FromResolveAll();
        Container.BindSignal<GameStateChangedSignal>().ToMethod<SceneReloader>(x => x.ReloadScene).FromResolveAll();
    }

    [Serializable]
    public class Settings
    {
        public GameObject EnemyPrefab => enemyPrefab;
        public GameObject MissilePrefab => missilePrefab;

        [SerializeField]
        private GameObject enemyPrefab;
        [SerializeField]
        private GameObject missilePrefab;
    }
}