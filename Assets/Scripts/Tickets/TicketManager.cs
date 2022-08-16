using System;

public class TicketManager
{
    public int TicketsCaught { get; private set; }
    public event Action OnTicketCaught;

    public void CatchTicket()
    {
        TicketsCaught += 1;
        OnTicketCaught?.Invoke();
    }
}