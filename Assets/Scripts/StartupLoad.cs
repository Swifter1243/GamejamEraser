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

    const string STARTUP_SCENE_NAME = "Startup";

    private void Start()
    {
        //Wait for audio to load and then play intro.
        AsyncOperation asyncScene = SceneManager.LoadSceneAsync(AudioSceneName, LoadSceneMode.Additive);
        asyncScene.completed += (AsyncOperation operation) =>
        {
            //Don't destroy audio scene on load!
            GameObject[] toDestroy = SceneManager.GetSceneByName(AudioSceneName).GetRootGameObjects();
            foreach (GameObject obj in toDestroy) GameObject.DontDestroyOnLoad(obj);
        };
        asyncScene.completed += (AsyncOperation operation) => { startupText.Startup(LoadMain); };
    }

    private void LoadMain()
    {
        AsyncOperation asyncScene = SceneManager.LoadSceneAsync(GameSceneName);
        asyncScene.completed += (AsyncOperation asyncScene) => { AudioStatics.instance.AddCallbacks(asyncScene, GameSceneName); };
    }
}
