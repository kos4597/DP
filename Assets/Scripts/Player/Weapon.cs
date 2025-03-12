using UnityEngine;

public class Weapon : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Monster"))
        {
            other.gameObject.GetComponent<Monster>().HitMonster(100);
        }
    }
}
