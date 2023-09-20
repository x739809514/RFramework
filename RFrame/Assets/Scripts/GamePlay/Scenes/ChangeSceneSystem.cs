using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneSystem : SingletonMono<ChangeSceneSystem>, ISaveHandler
{
    public CanvasGroup fadeCanvas;
    private readonly float fadeDuration = 0.25f;
    private bool isFade;
    [SceneName] public String startScene;
    private string curScene = String.Empty;

    private void OnEnable()
    {
        //保存
        ISaveHandler handler = this;
        handler.DoRegister(this);
    }

    private void Start()
    {
        Debug.Log("ChangeScene");
        OnTeleport(String.Empty, curScene == String.Empty ? startScene : curScene);
    }


#region Teleport Scene

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
        if (from != String.Empty)
        {
            yield return SceneManager.UnloadSceneAsync(from);
        }

        yield return SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);
        curScene = to;
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
            yield return null;
        }

        fadeCanvas.blocksRaycasts = false;
        isFade = false;
    }

#endregion


#region Data Save

    public SaveData GenerateSaveData()
    {
        var data = new SaveData();
        data.levelName = curScene;
        return data;
    }

    public void LoadSaveData(SaveData saveData)
    {
        curScene = saveData.levelName;
        Debug.Log("Load Scuess");
    }

#endregion
}