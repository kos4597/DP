using UnityEngine;
using UnityEngine.UI;

public static class ResourceManager
{
    public static GameObject CreateGameObject(string path, Transform parentTr = null)
    {
        GameObject go = Resources.Load<GameObject>(path);
        if(go == null)
        {
            Debug.LogError($"{path} 프리팹을 로드할 수 없습니다.");
            return null;
        }

        return GameObject.Instantiate(go, parentTr);
    }

    public static GameObject CreateGameObject(string path, Vector3 position, Quaternion rotation)
    {
        GameObject go = Resources.Load<GameObject>(path);
        if (go == null)
        {
            Debug.LogError($"{path} 프리팹을 로드할 수 없습니다.");
            return null;
        }

        return GameObject.Instantiate(go, position, rotation);
    }

    public static Sprite LoadSprite(string path)
    {
        return Resources.Load<Sprite>(path);
    }
}
