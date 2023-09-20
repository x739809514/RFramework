using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using Object = UnityEngine.Object;

public class SaveManager : SingletonMono<SaveManager>
{
    private Dictionary<string, SaveData> saveDic = new Dictionary<string, SaveData>();
    private List<ISaveHandler> saveList = new List<ISaveHandler>();
    private string jsonFolder;

    private void Start()
    {
        jsonFolder = Application.persistentDataPath + "/SAVE/";
        AddListener();
    }

    private void OnDestroy()
    {
        RemoveListener();
    }

    public void Register(ISaveHandler handler)
    {
        if (saveList.Contains(handler) == false)
        {
            saveList.Add(handler);
        }
    }

    /// <summary>
    /// 保存数据
    /// </summary>
    private void Save()
    {
        saveDic.Clear();
        foreach (var handler in saveList)
        {
            saveDic.Add(handler.GetType().Name, handler.GenerateSaveData());
        }

        var jsonData = JsonConvert.SerializeObject(saveDic);

        var resultPath = jsonFolder + "save.txt";
        if (File.Exists(resultPath) == false)
        {
            Directory.CreateDirectory(jsonFolder);
        }

        File.WriteAllText(resultPath, jsonData);
        Debug.Log("Save Success");
    }

    /// <summary>
    /// 重新加载数据
    /// </summary>
    private void LoadData()
    {
        var resultPath = jsonFolder + "save.txt";
        if (File.Exists(resultPath))
        {
            var stringData = File.ReadAllText(resultPath);
            var data = JsonConvert.DeserializeObject<Dictionary<string, SaveData>>(stringData);
            foreach (var handler in saveList)
            {
                handler.LoadSaveData(data[handler.GetType().Name]);
            }
        }
    }


#region Addlistener

    private void AddListener()
    {
        EventSystem.AddListener(EventEnumType.GameStartEvent, OnLoadEvent);
        EventSystem.AddListener(EventEnumType.GameEndEvent, OnSaveEvent);
    }

    private void RemoveListener()
    {
        EventSystem.RemoveListener(EventEnumType.GameStartEvent, OnLoadEvent);
        EventSystem.RemoveListener(EventEnumType.GameEndEvent, OnSaveEvent);
    }


    private void OnLoadEvent(object msg)
    {
        LoadData();
    }

    private void OnSaveEvent(object msg)
    {
        Save();
    }

    private void OnStartNewGameEvent(Object msg)
    {
        var resultPath = jsonFolder + "save.txt";
        if (File.Exists(resultPath) == false)
        {
            File.Delete(resultPath);
        }
    }

#endregion
}