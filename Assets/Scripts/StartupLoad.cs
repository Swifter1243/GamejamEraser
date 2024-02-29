using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartupLoad : MonoBehaviour
{
    [SerializeField]
    private string GameSceneName;
    [SerializeField]
    private string AudioSceneName;
    [SerializeField]
    private StartupFormat startupText;

    const string StartupSceneName = "Startup";

    private void Start()
    {
        //Wait for audio to load and then play intro.
        AsyncOperation asyncScene = SceneManager.LoadSceneAsync(AudioSceneName, LoadSceneMode.Additive);
        asyncScene.completed += (AsyncOperation operation) => { startupText.Startup(LoadMain); };
    }

    private void LoadMain()
    {
        //Don't wait for callbacks or anything.
        SceneManager.UnloadSceneAsync(StartupSceneName);
        AsyncOperation asyncScene = SceneManager.LoadSceneAsync(GameSceneName, LoadSceneMode.Additive);

        asyncScene.completed += (AsyncOperation asyncScene) => { AudioStatics.instance.AddCallbacks(asyncScene, GameSceneName); };
    }
}
