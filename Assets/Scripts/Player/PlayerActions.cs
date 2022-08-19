using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class PlayerActions : MonoBehaviour
{
    public PlayerInputActions actions;

    [Inject] LevelLoader _levelLoader;

    void Awake()
    {
        actions = new PlayerInputActions();
    }

    void OnEnable()
    {
        EnablePlayerControls();

        actions.Debug.Enable();
        actions.Debug.Restart.performed +=
            (_) => _levelLoader.LoadScene(SceneManager.GetActiveScene().buildIndex);
        actions.Debug.LoadPrev.performed +=
            (_) => _levelLoader.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        actions.Debug.LoadNext.performed +=
            (_) => _levelLoader.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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