using System.Collections;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger Instance { get; private set; }

    public enum SceneType
    {
        None,
        Logo,
        Intro,
        Lobby,
        Ingame,
        Loading,
    }

    public SceneType NextSceneType { get; private set; }

    public SceneBase NowScene { get; set; }

    private Coroutine sceneChangeCor = null;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        Instance = this;
    }

    public void ChangeScene(SceneType type = SceneType.None, bool useLoading = true)
    {
        if (NextSceneType == type)
            return;

        NextSceneType = type;

        if (useLoading)
        {
            SceneManager.LoadScene($"{SceneType.Loading}");
        }
        else
        {
            if (sceneChangeCor != null)
            {
                StopCoroutine(sceneChangeCor);
                sceneChangeCor = null;
            }

            sceneChangeCor = StartCoroutine(ChangeCor());
        }
    }

    private IEnumerator ChangeCor()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync($"{NextSceneType}");

        while (asyncOperation.isDone == false)
        {
            Debug.Log($"Scene Load : + {asyncOperation.progress * 100}%");

            yield return null;
        }

        NowScene = NextSceneType switch
        {
            SceneType.Logo => new LogoScene(),
            SceneType.Intro => new IntroScene(),
            SceneType.Lobby => new LobbyScene(),
            SceneType.Ingame => new IntroScene(),
            _ => null
        };
    }
}
