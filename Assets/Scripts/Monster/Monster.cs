using UnityEngine;

public class Monster : MonoBehaviour
{
    public void Hit(int damage)
    {
        Debug.Log("Hit" + damage);
    }
}
