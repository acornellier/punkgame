using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider2D))]
public class Ticket : MonoBehaviour
{
    [SerializeField] float rotationSpeed;

    [Inject] TicketManager _ticketManager;

    void FixedUpdate()
    {
        transform.Rotate(0, Time.fixedDeltaTime * rotationSpeed, 0);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        var player = col.GetComponent<Player>();
        if (!player)
            return;

        _ticketManager.CatchTicket();
        Destroy(gameObject);
    }
}