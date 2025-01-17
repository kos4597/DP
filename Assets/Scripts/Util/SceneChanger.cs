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
//        /// 매 프레임마다 상태에 따른 로직을 실행한다.
//        /// 이건 비동기함수로 매 프레임마다 호출 하는 방법도 있다.
//        /// 비동기함수는 코루틴을 쓰지 말고 UniTask를 활용할 것 (몇 년전부터 UniTask가 대세)
//        /// </summary>
//        private void Update()
//        {
//            if (currentGameScene == null)
//                return;

//            switch (currentGameScene.State)
//            {
//                // 첫 진입 시 None에서 Enter로 상태 전환
//                case SceneState.None:
//                    {
//                        // Enter를 호출해서 기본적인 정보를 셋팅
//                        currentGameScene.Enter();
//                        break;
//                    }

//                // Enter가 되었으면 Loading
//                case SceneState.Enter:
//                    {
//                        currentGameScene.Loading(prevGameScene);
//                        break;
//                    }

//                // 로딩 중에는 아무것도 하지 않는다.
//                case SceneState.Loading:
//                    {
//                        break;
//                    }

//                // 로딩이 끝나고 현재 씬이 완전히 동작 중일 때 매 프레임마다 호출
//                case SceneState.Update:
//                    {
//                        currentGameScene.Update();
//                        break;
//                    }

//                // 다른 씬으로 이동 시
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
//        // 씬에서 사용할 정보를 담는다.
//        // Intro -> Town으로 넘어갈 때 Town에 필요한 정보를 담은 후 SceneChanger를 통해 넘겨받는다.
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
//            // 기본적인 값 설정
//            SceneState = SceneState.Enter;
//        }

//        public void Loading(IGameScene prevGameScene)
//        {
//            SceneState = SceneState.Loading;

//            // 1. 로딩씬을 불러온다.  LoadingManager.In() ~

//            // 2. 이전씬을 언로드한다.
//            prevGameScene.Exit();

//            // 3. 현재 씬을 로드한다.
//            //SceneManager.Load(~)

//            // 4. 로딩씬을 언로드한다. LoadingManager.Out() ~

//            SceneState = SceneState.Update;
//        }

//        public void Update()
//        {
//        }

//        public void Exit()
//        {
//            // 씬을 빠져나올 때 해제해줘야 하는 것들 처리
//        }
//    }
//}