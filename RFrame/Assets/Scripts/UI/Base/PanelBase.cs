using System;
using UnityEngine;

public abstract class PanelBase : MonoBehaviour
{
#region Virtual,Abstract

    /// <summary>
    /// 面板激活时调用，相当于OnEnable
    /// </summary>
    public virtual void OnEnter()
    {
    }

    /// <summary>
    /// 面板打开时调用,相当于Awake
    /// </summary>
    public virtual void OnOpen()
    {
    }

    /// <summary>
    /// 面板被隐藏时调用
    /// </summary>
    public virtual void OnClose()
    {
    }

    /// <summary>
    /// 面板关闭时调用
    /// </summary>
    public virtual void OnExit()
    {
    }

#endregion
}