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
        None,
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
            SceneType.Loading => new LoadingScene(),
            _ => null
        };

        CurrentScene.sceneType = type;
        CurrentScene.sceneState = SceneState.None;
    }

    private void Update()
    {
        if (CurrentScene == null)
            return;

        switch(CurrentScene.sceneState)
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