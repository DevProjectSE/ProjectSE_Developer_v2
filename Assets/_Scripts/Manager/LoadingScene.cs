using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public static string nextScene;
    public TextMeshProUGUI loadingText;
    private void Start()
    {
        StartCoroutine(LoadScene());
        StartCoroutine(LoadingTextCoroutine());
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    private IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        while (!op.isDone)
        {
            if (op.progress >= 0.9f)
            {
                op.allowSceneActivation = true;
                yield break;
            }

        }
    }

    private IEnumerator LoadingTextCoroutine()
    {
        while (true)
        {
            loadingText.text = "Loading.";
            yield return new WaitForSeconds(1f);
            loadingText.text = "Loading..";
            yield return new WaitForSeconds(1f);
            loadingText.text = "Loading...";
            yield return new WaitForSeconds(1f);
        }
    }
}
