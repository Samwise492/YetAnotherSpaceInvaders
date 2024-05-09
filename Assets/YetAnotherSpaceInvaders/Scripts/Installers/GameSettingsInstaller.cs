using System;
using UnityEngine;
using Zenject;

//[CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    [SerializeField]
    private GameInstaller.Settings gameInstallerSettings;
    [SerializeField]
    private EnemySpawner.Settings enemySpawnerSettings;

    [SerializeField]
    private ShipSettings ship;
    [SerializeField]
    private EnemySettings enemy;

    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);


        Container.BindInstance(gameInstallerSettings);

        Container.BindInstance(ship.ShipCommonSettings);
        Container.BindInstance(ship.ShipMissileSettings);

        Container.BindInstance(enemySpawnerSettings);
        Container.BindInstance(enemy.EnemyCommonSettings);
        Container.BindInstance(enemy.EnemyMissileSettings);
    }
}

[Serializable]
public class ShipSettings
{
    public ShipCommonSettings ShipCommonSettings => shipCommonSettings;
    public ShipMissileHandler.Settings ShipMissileSettings => shipMissileSettings;
    
    [SerializeField] 
    private ShipCommonSettings shipCommonSettings;
    [SerializeField]
    private ShipMissileHandler.Settings shipMissileSettings;
}

[Serializable]
public class EnemySettings
{
    public EnemyCommonSettings EnemyCommonSettings => enemyCommonSettings;
    public EnemyMissileHandler.Settings EnemyMissileSettings => enemyMissileSettings;

    [SerializeField]
    private EnemyCommonSettings enemyCommonSettings;
    [SerializeField]
    private EnemyMissileHandler.Settings enemyMissileSettings;
}