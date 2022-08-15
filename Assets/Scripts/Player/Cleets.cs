using UnityEngine;

public class Cleets : MonoBehaviour
{
    [SerializeField] Player player;
    
    void OnTriggerEnter2D(Collider2D col)
    {
        var enemy = col.gameObject.GetComponent<Enemy>();
        if (!enemy)
            return;

        enemy.Die();
        player.Jump();
    }
}
