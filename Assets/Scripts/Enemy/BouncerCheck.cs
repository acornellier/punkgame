using UnityEngine;
using Zenject;

[RequireComponent(typeof(BoxCollider2D))]
public class BouncerCheck : MonoBehaviour
{
    [SerializeField] Bouncer bouncer;

    [Inject] TicketManager _ticketManager;

    bool _playerAllowedThrough;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (_playerAllowedThrough) return;

        var player = col.GetComponent<Player>();
        if (!player) return;

        if (_ticketManager.TicketsCaught < _ticketManager.TotalTickets)
        {
            bouncer.DenyPlayer();
            return;
        }

        bouncer.AllowPlayerThrough();
        _playerAllowedThrough = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        var player = col.GetComponent<Player>();
        if (!player) return;

        bouncer.OnPlayerGone();
    }
}