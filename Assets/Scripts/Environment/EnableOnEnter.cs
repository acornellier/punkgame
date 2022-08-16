using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnableOnEnter : MonoBehaviour
{
    [SerializeField] GameObject[] objects;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.GetComponent<Player>())
            return;

        foreach (var obj in objects)
        {
            obj.SetActive(true);
        }
    }
}