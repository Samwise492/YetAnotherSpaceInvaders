using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuPresenter : MonoBehaviour
{
    public ReactiveProperty<bool> pauseMenuWindowState = new();

    public event Action<bool> OnSetReload;

    [SerializeField]
    private CanvasGroup pauseMenuWindow;

    [SerializeField]
    private Button pauseMenuButton;

    [SerializeField]
    private Button confirmButton;
    [SerializeField]
    private Button rejectButton;

    private readonly CompositeDisposable disposables = new();

    private void Start()
    {
        Subscribe();
    }
    private void OnDisable()
    {
        disposables.Clear();
    }

    private void Subscribe()
    {
        pauseMenuButton.onClick.AsObservable().Subscribe(x => { SetPauseWindowState(); }).AddTo(disposables);
        confirmButton.onClick.AsObservable().Subscribe(x => { SetReload(true); }).AddTo(disposables);
        rejectButton.onClick.AsObservable().Subscribe(x => { SetReload(false); }).AddTo(disposables);

        pauseMenuWindowState.Subscribe(value =>
        {
            pauseMenuWindow.alpha = value ? 1 : 0;
        }).AddTo(disposables);
    }

    private void SetPauseWindowState()
    {
        pauseMenuWindowState.Value = !pauseMenuWindowState.Value;
        Time.timeScale = pauseMenuWindowState.Value ? 0 : 1;
    }

    private void SetReload(bool state)
    {
        OnSetReload?.Invoke(state);

        if (!state)
        {
            SetPauseWindowState();
        }
    }
}
