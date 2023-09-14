using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneSystem : SingletonMono<ChangeSceneSystem>
{
    public CanvasGroup fadeCanvas;
    private float fadeDuration = 0.5f;
    private bool isFade;

    private void Start()
    {
        //DontDestroyOnLoad(gameObject);
    }

    public void OnTeleport(string from, string to)
    {
        StartCoroutine(TransitionScene(from, to));
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

        var speed = (fadeCanvas.alpha - targetAlpha) / fadeDuration;
        if (Mathf.Approximately(fadeCanvas.alpha, targetAlpha) == false)
        {
            fadeCanvas.alpha = Mathf.MoveTowards(fadeCanvas.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;
        }

        fadeCanvas.blocksRaycasts = false;
        isFade = false;
    }
}