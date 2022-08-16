using UnityEngine;
using UnityEngine.SceneManagement;

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

        actions.Debug.Enable();
        actions.Debug.Restart.performed +=
            (_) => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        actions.Debug.LoadPrev.performed +=
            (_) => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        actions.Debug.LoadNext.performed +=
            (_) => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void OnDisable()
    {
        DisablePlayerControls();

        actions.Debug.Disable();
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