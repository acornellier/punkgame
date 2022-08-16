using TMPro;
using UnityEngine;
using Zenject;

public class TicketCounter : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    [Inject] TicketManager _ticketManager;

    void OnEnable()
    {
        _ticketManager.OnTicketCaught += OnTicketCaught;
    }

    void OnDisable()
    {
        _ticketManager.OnTicketCaught -= OnTicketCaught;
    }

    void OnTicketCaught()
    {
        text.text = _ticketManager.TicketsCaught.ToString();
    }
}