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












//namespace IronJade
//{
//    public enum SceneType
//    {
//        Logo,
//        Intro,
//        Town,
//        Battle
//    }

//    public enum SceneState
//    {
//        None,
//        Enter,
//        Loading,
//        Update,
//        Exit,
//    }

//    public class SceneChanger : MonoBehaviour
//    {
//        private static IGameScene prevGameScene = null;
//        private static IGameScene currentGameScene = null;

//        private void Awake()
//        {
//            DontDestroyOnLoad(gameObject);
//        }

//        public static void Change(SceneType sceneType, IGameSceneModel model)
//        {
//            prevGameScene = currentGameScene;
//            currentGameScene = type switch
//            {
//                //SceneType.Logo => new LogoScene(model),
//                SceneType.Intro => new IntroScene(model),
//                //SceneType.Town => new TownScene(model),
//                //SceneType.Battle => new BattleScene(model),
//                _ => null
//            };
//        }

//        /// <summary>
//        /// �� �����Ӹ��� ���¿� ���� ������ �����Ѵ�.
//        /// �̰� �񵿱��Լ��� �� �����Ӹ��� ȣ�� �ϴ� ����� �ִ�.
//        /// �񵿱��Լ��� �ڷ�ƾ�� ���� ���� UniTask�� Ȱ���� �� (�� �������� UniTask�� �뼼)
//        /// </summary>
//        private void Update()
//        {
//            if (currentGameScene == null)
//                return;

//            switch (currentGameScene.State)
//            {
//                // ù ���� �� None���� Enter�� ���� ��ȯ
//                case SceneState.None:
//                    {
//                        // Enter�� ȣ���ؼ� �⺻���� ������ ����
//                        currentGameScene.Enter();
//                        break;
//                    }

//                // Enter�� �Ǿ����� Loading
//                case SceneState.Enter:
//                    {
//                        currentGameScene.Loading(prevGameScene);
//                        break;
//                    }

//                // �ε� �߿��� �ƹ��͵� ���� �ʴ´�.
//                case SceneState.Loading:
//                    {
//                        break;
//                    }

//                // �ε��� ������ ���� ���� ������ ���� ���� �� �� �����Ӹ��� ȣ��
//                case SceneState.Update:
//                    {
//                        currentGameScene.Update();
//                        break;
//                    }

//                // �ٸ� ������ �̵� ��
//                case SceneState.Exit:
//                    {
//                        break;
//                    }
//            }
//        }
//    }


//    public interface IGameScene
//    {
//        public SceneType SceneType { get; }
//        public SceneState SceneState { get; private set; }
//        public IGameSceneModel Model { get; private set; }
//        public void Enter();
//        public void Loading(IGameScene prevGameScene);
//        public void Update();
//        public void Exit();
//    }

//    public interface IGameSceneModel
//    {
//    }

//    public class IntroSceneModel : IGameSceneModel
//    {
//        // ������ ����� ������ ��´�.
//        // Intro -> Town���� �Ѿ �� Town�� �ʿ��� ������ ���� �� SceneChanger�� ���� �Ѱܹ޴´�.
//    }

//    public class IntroScene : IGameScene
//    {
//        public SceneType SceneType { get; }
//        public SceneState SceneState { get; private set; }
//        public IGameSceneModel Model { get; private set; }

//        public IntroScene(IGameSceneModel model)
//        {
//            Model = model;
//        }

//        public void Enter()
//        {
//            // �⺻���� �� ����
//            SceneState = SceneState.Enter;
//        }

//        public void Loading(IGameScene prevGameScene)
//        {
//            SceneState = SceneState.Loading;

//            // 1. �ε����� �ҷ��´�.  LoadingManager.In() ~

//            // 2. �������� ��ε��Ѵ�.
//            prevGameScene.Exit();

//            // 3. ���� ���� �ε��Ѵ�.
//            //SceneManager.Load(~)

//            // 4. �ε����� ��ε��Ѵ�. LoadingManager.Out() ~

//            SceneState = SceneState.Update;
//        }

//        public void Update()
//        {
//        }

//        public void Exit()
//        {
//            // ���� �������� �� ��������� �ϴ� �͵� ó��
//        }
//    }
//}