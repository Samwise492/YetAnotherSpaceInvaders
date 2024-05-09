using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
	public int freezeTime = 2;

	private bool isRealodingStarted;

	public void ReloadScene()
	{
		StartCoroutine(ProcessSceneReloading());
	}

	private IEnumerator ProcessSceneReloading()
	{
		if (isRealodingStarted)
		{
			yield break;
		}

		isRealodingStarted = true;

        Time.timeScale = 0;

		yield return new WaitForSecondsRealtime(freezeTime);

        Time.timeScale = 1;

        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name, LoadSceneMode.Single);

		yield break;
    }
}
