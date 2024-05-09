using UnityEngine;
using Zenject;

public class DefeatCollider : MonoBehaviour
{
    private SignalBus signalBus;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        this.signalBus = signalBus;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyFacade>())
        {
            signalBus.Fire(new GameStateChangedSignal() { state = GameState.Defeat });
        }
    }
}
