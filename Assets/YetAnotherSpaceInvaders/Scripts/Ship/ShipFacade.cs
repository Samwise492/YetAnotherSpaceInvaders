using UnityEngine;
using Zenject;

public class ShipFacade : MonoBehaviour, IDamageable
{
    private Ship ship;
    private ShipCommonSettings shipCommonSettings;
    private SignalBus signalBus;

    private int currentHealth;

    [Inject]
    public void Construct(Ship ship, ShipCommonSettings shipCommonSettings, SignalBus signalBus)
    {
        this.ship = ship;
        this.shipCommonSettings = shipCommonSettings;
        this.signalBus = signalBus;

        currentHealth = this.shipCommonSettings.Health;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            signalBus.Fire(new GameStateChangedSignal() { state = GameState.Defeat });
        }
    }
}
