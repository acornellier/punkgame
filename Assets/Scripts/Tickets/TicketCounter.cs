using TMPro;
using UnityEngine;
using Zenject;

public class TicketCounter : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    [Inject] TicketManager _ticketManager;

    void Start()
    {
        UpdateText();
    }

    void OnEnable()
    {
        _ticketManager.OnTicketCaught += UpdateText;
    }

    void OnDisable()
    {
        _ticketManager.OnTicketCaught -= UpdateText;
    }

    void UpdateText()
    {
        text.text = $"{_ticketManager.TicketsCaught.ToString()} / {_ticketManager.TotalTickets}";
    }
}