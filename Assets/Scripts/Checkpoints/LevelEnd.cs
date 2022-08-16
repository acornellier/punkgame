using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class LevelEnd : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.GetComponent<Player>())
            return;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}