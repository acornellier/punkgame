using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    AsyncOperation _asyncLoad;

    void Start()
    {
        _asyncLoad = SceneManager.LoadSceneAsync("Level1");
        _asyncLoad.allowSceneActivation = false;
    }

    public void LoadLevel1()
    {
        _asyncLoad.allowSceneActivation = true;
    }
}