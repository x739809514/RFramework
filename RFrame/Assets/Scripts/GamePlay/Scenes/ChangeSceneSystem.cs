using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneSystem : SingletonMono<ChangeSceneSystem>
{
    public CanvasGroup fadeCanvas;
    private readonly float fadeDuration = 0.25f;
    private bool isFade;
    

    public void OnTeleport(string from, string to)
    {
        if (!isFade)
        {
            StartCoroutine(TransitionScene(from, to));
        }
    }

    IEnumerator TransitionScene(string from, string to)
    {
        yield return OnFade(1f);
        yield return SceneManager.UnloadSceneAsync(from);
        yield return SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);

        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newScene);
        yield return OnFade(0f);
    }

    IEnumerator OnFade(float targetAlpha)
    {
        isFade = true;
        fadeCanvas.blocksRaycasts = true;

        var speed = Mathf.Abs(fadeCanvas.alpha - targetAlpha) / fadeDuration;

        while (Mathf.Approximately(fadeCanvas.alpha, targetAlpha) == false)
        {
            fadeCanvas.alpha = Mathf.MoveTowards(fadeCanvas.alpha, targetAlpha, speed * Time.deltaTime);
            Debug.Log(fadeCanvas.alpha);
            yield return null;
        }

        fadeCanvas.blocksRaycasts = false;
        isFade = false;
    }
}