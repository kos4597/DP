using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public enum SceneType
    {
        None,
        Logo,
        Intro,
        Lobby,
        Ingame,
        Loading,
    }

    public enum SceneState
    {
        Enter,
        Loading,
        Update,
        Exit,
    }


    public static SceneChanger Instance { get; private set; }
    [NonSerialized]
    public SceneBase PrevScene;
    [NonSerialized]
    public SceneBase CurrentScene;
    
    private void Awake()
    {
        DontDestroyOnLoad(this);
        Instance = this;
    }

    public async UniTask ChangeScene(SceneType type = SceneType.None, bool useLoading = true)
    {
        if(CurrentScene != null)
        {
            if(CurrentScene.sceneType == type)
            {
                return;
            }
        }

        PrevScene = CurrentScene;
        CurrentScene = type switch
        {
            SceneType.Logo => new LogoScene(),
            SceneType.Intro => new IntroScene(),
            SceneType.Lobby => new LobbyScene(),
            SceneType.Ingame => new IngameScene(),
            _ => null
        };

        CurrentScene.sceneType = type;

        CurrentScene.EnterScene();

        await CurrentScene.LoadingSceneAsync();
    }

    public void ExitScene()
    {
        if(CurrentScene != null)
        {
            CurrentScene.ExitScene();
        }
    }

    private void Update()
    {
        if (CurrentScene == null)
            return;

        if (CurrentScene.sceneState == SceneState.Update)
            CurrentScene.UpdateScene();
    }
}
