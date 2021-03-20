using System.Collections;
using UnityEngine;

namespace MissionAcomplished
{
    public class NavigationController : MonoBehaviour
    {
        public GameObject confirmExitDialog;

        public void LoadMainMenuScene()
        {
            StartCoroutine(LoadScene("MainMenu"));
        }

        public void LoadSinglePlayerScene()
        {
            StartCoroutine(LoadScene("Singleplayer"));
        }

        public void LoadMultiplayerPlayerScene()
        {
            StartCoroutine(LoadScene("MultiplayerLobby"));
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


        public void HideConfirmExitDialog()
        {
            if (this.confirmExitDialog != null)
            {
                this.confirmExitDialog.SetActive(false);
            }
        }

#if UNITY_ANDROID
    private void Update()
    {
        // Make sure user is on Android platform
        if (Application.platform == RuntimePlatform.Android)
        {
            // Check if Back was pressed this frame
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 0)
                {
                    Application.Quit();
                }
                else if (this.confirmExitDialog != null)
                {
                    this.confirmExitDialog.SetActive(true);
                }
                else
                {
                    this.LoadMainMenuScene();
                }
            }
        }
    }
#endif
    }
}
