using TMPro;
using UniRx;
using UnityEngine;

public class ShipPointsPresenter : MonoBehaviour
{
	public ReactiveProperty<int> pointValue = new();

    [SerializeField]
    private TextMeshProUGUI pointText;

    private readonly CompositeDisposable disposables = new();

    private void Start()
    {
        Subscribe();
    }
    private void OnDisable()
    {
        disposables.Clear();
    }

    public void ChangePoints(EnemyDestroyedSignal signal)
	{
        pointValue.Value += signal.points;
    }

    private void Subscribe()
    {
        pointValue.Subscribe(value =>
        {
            pointText.text = value.ToString();
        }).AddTo(disposables);
    }
}
