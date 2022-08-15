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
        EnableControls();
    }

    void OnDisable()
    {
        DisableControls();
    }

    public void EnableControls()
    {
        actions.Player.Enable();
    }

    public void DisableControls()
    {
        actions.Player.Disable();
    }

    public float ReadMoveValue()
    {
        return actions.Player.Move.ReadValue<float>();
    }
}