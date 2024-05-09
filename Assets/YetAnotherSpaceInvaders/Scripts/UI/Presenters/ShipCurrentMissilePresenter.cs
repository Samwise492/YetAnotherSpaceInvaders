using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ShipCurrentMissilePresenter : MonoBehaviour
{
    public ReactiveProperty<Sprite> currentMissileValue = new();

    [SerializeField]
    private Image currentMissileImage;

    private readonly CompositeDisposable disposables = new();

    private void Start()
    {
        Subscribe();
    }
    private void OnDisable()
    {
        disposables.Clear();
    }

    public void SetSprite(NewMissileTypeGotSignal signal)
    {
        currentMissileValue.Value = signal.sprite;
    }

    private void Subscribe()
    {
        currentMissileValue.Subscribe(value =>
        {
            currentMissileImage.sprite = value;
        }).AddTo(disposables);
    }
}
