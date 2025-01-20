using Cysharp.Threading.Tasks;
using UnityEngine;

public class Root : MonoBehaviour
{
    private void Start()
    {
        SceneChanger.Instance.ChangeScene(SceneChanger.SceneType.Logo, false).Forget();
    }
}
