using UnityEngine;
using Zenject;

public class EnemyMoveHandler : IFixedTickable
{
    private readonly Enemy enemy;
    private readonly EnemyFacade enemyFacade;
    private readonly EnemyFacade.Pool enemyFactory;
    private readonly ScreenBoundary screenBoundary;
    private readonly SignalBus signalBus;

    public EnemyMoveHandler
    (
        Enemy enemy,
        EnemyFacade enemyFacade,
        EnemyFacade.Pool enemyFactory,
        ScreenBoundary screenBoundary,
        SignalBus signalBus
    )
    {
        this.enemy = enemy;
        this.enemyFacade = enemyFacade;
        this.enemyFactory = enemyFactory;
        this.screenBoundary = screenBoundary;
        this.signalBus = signalBus;
    }

    public void FixedTick()
    {
        Vector3 newPosition = enemy.GetPosition;

        newPosition.x += enemy.GetTunables.Speed * Time.fixedDeltaTime;
        CheckLeap();

        enemy.GetPosition = newPosition;

        if (enemy.isReadyForLeap)
        {
            enemy.isReadyForLeap = false;
            newPosition = enemy.GetPosition;
            newPosition.y += enemy.GetTunables.RowLeap;
            enemy.GetPosition = newPosition;
        }

        if (!screenBoundary.IsBelowBottom(enemy))
        {
            enemyFactory.Despawn(enemyFacade);
        }
    }

    private void CheckLeap()
    {
        if (enemy.GetPosition.x >= screenBoundary.Right)
        {
            signalBus.Fire(new EnemyLeapSignal() { direction = LeapDirection.Left});
        }
        else if (enemy.GetPosition.x <= screenBoundary.Left)
        {
            signalBus.Fire(new EnemyLeapSignal() { direction = LeapDirection.Right});
        }
    }
}