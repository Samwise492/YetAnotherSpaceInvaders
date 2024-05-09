using TMPro;
using UniRx;
using UnityEngine;

public class GameStatePresenter : MonoBehaviour
{
    public ReactiveProperty<string> gameStateTextValue = new();
    public ReactiveProperty<bool> gameStateWindowState = new();

    [SerializeField]
    private TextMeshProUGUI gameStateText;

    [SerializeField]
    private CanvasGroup gameStateWindow;

    private readonly CompositeDisposable disposables = new();

    private void Start()
    {
        Subscribe();
    }
    private void OnDisable()
    {
        disposables.Clear();
    }

    public void SetState(GameStateChangedSignal signal)
    {
        gameStateTextValue.Value = signal.GetGameStateText(signal.state);
        gameStateWindowState.Value = true;
    }

    private void Subscribe()
    {
        gameStateTextValue.Subscribe(value =>
        {
            gameStateText.text = value;
        }).AddTo(disposables);

        gameStateWindowState.Subscribe(value =>
        {
            gameStateWindow.alpha = value ? 1 : 0;
        }).AddTo(disposables);
    }
}
