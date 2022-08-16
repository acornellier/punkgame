using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider2D))]
public class Checkpoint : MonoBehaviour
{
    [Inject] CheckpointManager _checkpointManager;

    Collider2D _collider;

    void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.GetComponent<Player>())
            return;

        _checkpointManager.CurrentCheckpoint = transform;
        _collider.enabled = false;
    }
}