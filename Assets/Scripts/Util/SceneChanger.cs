using System;
using System.Collections;
using Cysharp.Threading.Tasks;
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
        None,
        Enter,
        Loading,
        Update,
        Exit,
    }

    // 논리가 다른 로직들끼리는 한 칸씩 간격을 줄 것
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

    public async UniTask ChangeScene(SceneType type = SceneType.None)
    {
        if (CurrentScene != null)
        {
            if (CurrentScene.SceneType == type)
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
            SceneType.Loading => new LoadingScene(),
            _ => null
        };

        CurrentScene.ChangeSceneType(type);
        CurrentScene.ChangeSceneState(SceneChanger.SceneState.None);
    }

    private void Update()
    {
        if (CurrentScene == null)
            return;

        switch (CurrentScene.SceneState)
        {
            case SceneState.None:
                {
                    CurrentScene.EnterScene();
                }
                break;
            case SceneState.Enter:
                {
                    CurrentScene.LoadingSceneAsync().Forget();
                }
                break;
            case SceneState.Loading:
                {
                    //로딩중일땐 아무것도 안함.
                }
                break;
            case SceneState.Update:
                {
                    CurrentScene.UpdateScene();
                }
                break;
            case SceneState.Exit:
                {
                    // 다음씬 이동시 Exit.
                    CurrentScene.ExitScene();
                }
                break;
        }
    }
}