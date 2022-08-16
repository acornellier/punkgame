using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider2D))]
public class Ticket : MonoBehaviour
{
    [Inject] TicketManager _ticketManager;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.GetComponent<Player>())
            return;

        _ticketManager.CatchTicket();
        Destroy(gameObject);
    }
}