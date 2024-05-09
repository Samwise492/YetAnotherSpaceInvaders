public class EnemyMoveComposer
{
    private readonly EnemyFacade.Pool enemyFactory;
    private LeapDirection lastDirection;

    public EnemyMoveComposer(EnemyFacade.Pool enemyFactory)
    {
        this.enemyFactory = enemyFactory;
    }

    public void RegulateMoveBehaviour(EnemyLeapSignal signal)
    {
        if (signal.direction == LeapDirection.None)
        {
            lastDirection = signal.direction;
            return;
        }

        if (signal.direction == lastDirection)
        {
            return;
        }

        foreach (EnemyFacade _enemy in enemyFactory.currentPool)
        {
            _enemy.ConcreteEnemy.isReadyForLeap = true;
            _enemy.ConcreteEnemy.GetTunables.Speed *= -1;

            lastDirection = signal.direction;
        }
    }
}
