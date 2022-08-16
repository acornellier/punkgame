using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider2D))]
public class Checkpoint : MonoBehaviour
{
    [Inject] CheckpointManager _checkpointManager;

    void OnTriggerEnter2D(Collider2D col)
    {
        _checkpointManager.CurrentCheckpoint = transform;
    }
}