using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class PerformBootstrap
{
    const string SceneName = "LoadingScreen";

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Execute()
    {
        for (int sceneIndex = 0; sceneIndex < SceneManager.sceneCount; ++sceneIndex) 
        {
            var candidate = SceneManager.GetSceneAt(sceneIndex);
            if(candidate.name == SceneName)
            {
                return;
            }
        }
    }
}


public class BootstrappedData : MonoBehaviour
{
    private static BootstrappedData _instance;
    public static BootstrappedData Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
