using UnityEngine;
using Zenject;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Ticket : MonoBehaviour
{
    [SerializeField] AudioClip collectClip;

    [Inject] TicketManager _ticketManager;

    AudioSource _audioSource;
    SpriteRenderer _renderer;

    bool _collected;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (_collected || !col.GetComponent<Player>())
            return;

        _collected = true;
        _ticketManager.CatchTicket();
        _audioSource.PlayOneShot(collectClip);
        _renderer.enabled = false;
    }
}