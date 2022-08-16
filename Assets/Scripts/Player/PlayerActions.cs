using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public PlayerInputActions actions;

    void Awake()
    {
        actions = new PlayerInputActions();
    }

    void OnEnable()
    {
        EnablePlayerControls();
    }

    void OnDisable()
    {
        DisablePlayerControls();
    }

    public void EnablePlayerControls()
    {
        actions.Player.Enable();
    }

    public void DisablePlayerControls()
    {
        actions.Player.Disable();
    }

    public float ReadMoveValue()
    {
        return actions.Player.Move.ReadValue<float>();
    }
}