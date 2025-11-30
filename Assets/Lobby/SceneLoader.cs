using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private string loadedEnvironment;

    private void OnEnable()
    {
        EventBus.Instance.OnMapSelected += LoadEnvironment;
    }

    private void OnDisable()
    {
        if (EventBus.Instance == null) return;
        EventBus.Instance.OnMapSelected -= LoadEnvironment;
    }

    private void LoadEnvironment(string mapSceneName)
    {
        StartCoroutine(SwapEnvironment(mapSceneName));
    }

    private IEnumerator SwapEnvironment(string mapScene)
    {
        if (!string.IsNullOrEmpty(loadedEnvironment))
        {
            yield return SceneManager.UnloadSceneAsync(loadedEnvironment);
        }

        yield return new WaitForSeconds(2f);
        // load chosen map: change LoadSceneMode to additive once scenes are separated into ManagerScene and LevelScenes
        var async = SceneManager.LoadSceneAsync(mapScene, LoadSceneMode.Single);
        while (!async.isDone) yield return null;
        loadedEnvironment = mapScene;
        yield return null;

        var players = FindObjectsByType<Player>(FindObjectsSortMode.None);
        EventBus.Instance?.OnStartGame?.Invoke(players);
    }
}