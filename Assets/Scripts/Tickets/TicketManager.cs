using System;
using Zenject;
using Object = UnityEngine.Object;

public class TicketManager : IInitializable
{
    public int TicketsCaught { get; private set; }
    public event Action OnTicketCaught;

    public int TotalTickets { get; private set; }

    public void Initialize()
    {
        TotalTickets = Object.FindObjectsOfType<Ticket>().Length;
        OnTicketCaught?.Invoke();
    }

    public void CatchTicket()
    {
        TicketsCaught += 1;
        OnTicketCaught?.Invoke();
    }
}