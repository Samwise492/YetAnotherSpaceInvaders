using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class ShipMissileHandler : IFixedTickable
{
    private readonly Ship ship;
    private readonly Settings settings;
    private readonly MissileFacade.Pool laserPool;
    private readonly ShipCommonSettings shipCommonSettings;
    private readonly ShipMissileTypes shipMissileTypes;
    private readonly SignalBus signalBus;

    private float lastShootTime;

    private float currentTimeFrame;
    private bool isMissileTypeChangingLocked;
    private bool isTimeFrameAssigned;

    private MissileType currentMissileType;
    private float currentDelay;

    public ShipMissileHandler(
        Ship ship,
        Settings settings,
        MissileFacade.Pool laserPool,
        ShipCommonSettings shipCommonSettings,
        ShipMissileTypes missileDropChance,
        SignalBus signalBus
    )
    {
        this.ship = ship;
        this.settings = settings;
        this.laserPool = laserPool;
        this.shipCommonSettings = shipCommonSettings;
        this.shipMissileTypes = missileDropChance;
        this.signalBus = signalBus;

        currentMissileType = shipMissileTypes.MissileData.Where(x => x.IsDefault).First().MissileType;
        currentDelay = shipMissileTypes.MissileData.Where(x => x.IsDefault).First().Delay;

        lastShootTime = -2;
    }

    public void ChangeMissileType()
    {
        if (!IsMissilePerChanceValid() || isMissileTypeChangingLocked)
        {
            return;
        }

        List<MissileType> availableTypes = new();

        foreach (MissileData missile in shipMissileTypes.MissileData)
        {
            availableTypes.Add(missile.MissileType);
        }

        MissileType pickedType = availableTypes[Random.Range(0, availableTypes.Count)];
        MissileData pickedMissile = shipMissileTypes.MissileData.Where(x => x.MissileType == pickedType).First();

        int chance = Random.Range(0, 100);

        if (pickedMissile.Chance <= chance)
        {
            SetMissile(pickedType, pickedMissile);

            isMissileTypeChangingLocked = true;
        }
    }

    public void FixedTick()
    {
        if (Time.time - lastShootTime > currentDelay)
        {
            lastShootTime = Time.time;

            Shoot();
        }

        if (isMissileTypeChangingLocked)
        {
            if (!isTimeFrameAssigned)
            {
                currentTimeFrame = Time.time;
                isTimeFrameAssigned = true;
            }

            if (Time.time - currentTimeFrame > settings.newMissileTypeLockTime)
            {
                SetMissile(shipMissileTypes.MissileData.Where(x => x.IsDefault).First().MissileType,
                    shipMissileTypes.MissileData.Where(x => x.IsDefault).First());

                isMissileTypeChangingLocked = false;
                isTimeFrameAssigned = false;
            }
        }
    }

    private bool IsMissilePerChanceValid()
    {
        int wholeDropChance = 0;

        foreach (MissileData missile in shipMissileTypes.MissileData)
        {
            wholeDropChance += missile.Chance;
        }

        if (wholeDropChance > 100)
        {
            Debug.LogError("Drop chances are more than 100%. Check Scriptable Object.");
            return false;
        }

        return true;
    }

    private void Shoot()
    {
        var tunables = new MissileTunables
        {
            type = currentMissileType,
            spawnPoint = ship.GetMissileSpawnPoint.position,
            velocity = settings.velocity,
            hitPoint = shipCommonSettings.HitPoint
        };

        laserPool.Spawn(tunables);
    }

    private void SetMissile(MissileType type, MissileData data)
    {
        currentMissileType = type;
        currentDelay = data.Delay;

        signalBus.Fire(new NewMissileTypeGotSignal()
        {
            newType = currentMissileType,
            sprite = shipMissileTypes.MissileData.Where(x => x.MissileType == currentMissileType).First().Sprite
        });
    }

    [System.Serializable]
    public class Settings
    {
        public float newMissileTypeLockTime;
        public int velocity;
    }
}
