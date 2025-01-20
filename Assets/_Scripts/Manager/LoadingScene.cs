using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public static string nextScene;

    private void Start()
    {
        StartCoroutine(LoadScene());
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
            Debug.Log(nextScene);
            Debug.Log("진입");
            yield return null;
            if (op.progress < 0.9f)
            {
                Debug.Log("대기중");
            }
            else
            {
                Debug.Log("대기 끝");
                Debug.Log(op.progress);
                if (op.progress >= 0.9f)
                {
                    Debug.Log("허가");
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
