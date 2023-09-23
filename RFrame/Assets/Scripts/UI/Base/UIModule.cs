using System.Collections.Generic;
using UnityEngine;

public class UIModule : SingletonMono<UIModule>
{
    public Dictionary<UIType, PanelBase> panelExistDic;

    private Stack<PanelBase> panelStack;
    private PanelBase curPanel;
    private readonly string folderPath = "UI/";

    private Transform CanvasTransform
    {
        get
        {
            var trans = GameObject.Find("Canvas").transform;
            return trans != null ? trans : null;
        }
    }

    /// <summary>
    /// 打开面板
    /// </summary>
    /// <param name="panelName"></param>
    public void OpenPanel(BaseContent content)
    {
        panelStack ??= new Stack<PanelBase>();

        if (panelStack.Count > 0)
        {
            curPanel = panelStack.Peek();
            curPanel.OnClose();
        }

        var panel = GetPanel(content);
        panelStack.Push(panel);
        panel.OnEnter();
    }

    /// <summary>
    /// 打开面板
    /// </summary>
    public void ClosePanel()
    {
        panelStack ??= new Stack<PanelBase>();
        if (panelStack.Count < 0) return;

        var top = panelStack.Pop();
        top.OnExit();

        if (panelStack.Count > 0)
        {
            var panel = panelStack.Peek();
            panel.OnEnter();
        }
    }

    /// <summary>
    /// 根据面板类型获得面板
    /// </summary>
    /// <param name="uiType"></param>
    /// <returns></returns>
    private PanelBase GetPanel(BaseContent content)
    {
        panelExistDic ??= new Dictionary<UIType, PanelBase>();

        if (panelExistDic.TryGetValue(content.uiType, out var panel)) return panel;
        var panelRes = Resources.Load<GameObject>(content.uiType.path);
        var go = Instantiate(panelRes, CanvasTransform);
        var b = go.GetComponent<PanelBase>();
        if (b != null)
        {
            b.OnOpen();
            panelExistDic.Add(content.uiType, b);
            return b;
        }

        Debug.LogError("找不到对应路径的面板");
        return null;
    }
}