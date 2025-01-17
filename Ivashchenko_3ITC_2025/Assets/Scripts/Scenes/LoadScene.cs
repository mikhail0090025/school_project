using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    static string LoadSceneName;
    [SerializeField] Image Loadingbar;

    void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(LoadSceneName);
        asyncOperation.allowSceneActivation = false; // Wait until the scene is fully loaded

        while (!asyncOperation.isDone)
        {
            // Update the progress of the loading bar (progress goes from 0 to 0.9)
            Loadingbar.fillAmount = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            if (asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    public static void LoadSceneGlobally(string sceneName)
    {
        LoadSceneName = sceneName;
        SceneManager.LoadScene("SceneLoader");
    }
}
