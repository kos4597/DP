using UnityEngine;

public class Weapon : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger : {other.tag}");
        if (other.tag.Equals("Monster"))
        {
            other.gameObject.GetComponent<Monster>().Hit(10);
        }
    }
}
