using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void LoadSinglePlayerScene()
    {
        StartCoroutine(LoadScene("Singleplayer"));
    }

    public void LoadMultiplayerPlayerScene()
    {
        // StartCoroutine(LoadScene("Multiplayer"));
        Debug.Log("Nada por aquí");
    }

    private IEnumerator LoadScene(string scene)
    {
        AsyncOperation async =
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene);
        while (!async.isDone)
        {
            yield return null;
        }
    }
}
