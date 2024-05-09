using UnityEngine;

public class SceneReloaderToPauseMenuPresenterAdapter : MonoBehaviour
{
	[SerializeField]
	private SceneReloader sceneReloader;

	[SerializeField]
	private PauseMenuPresenter pauseMenuPresenter;

    private void Start()
    {
        pauseMenuPresenter.OnSetReload += CheckReload;
    }
    private void OnDestroy()
    {
        pauseMenuPresenter.OnSetReload -= CheckReload;
    }

    private void CheckReload(bool state)
    {
        if (state)
        {
            sceneReloader.ReloadScene();
        }
    }
}
