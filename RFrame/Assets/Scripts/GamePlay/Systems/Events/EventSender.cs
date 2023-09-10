using System;
using System.Collections.Generic;
using UnityEngine;

public class EventSender<Tkey, TValue>
{
    /// <summary>
    /// 事件表
    /// </summary>
    private Dictionary<Tkey, Action<TValue>> eventDic = new Dictionary<Tkey, Action<TValue>>();

    /// <summary>
    /// 添加事件监听器
    /// </summary>
    public void AddListener(Tkey eventType, Action<TValue> eventHandler)
    {
        if (eventDic.TryGetValue(eventType, out var callbacks))
        {
            eventDic[eventType] = callbacks + eventHandler;
        }
        else
        {
            eventDic.Add(eventType, eventHandler);
        }
    }

    /// <summary>
    /// 移除事件
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="eventHandler"></param>
    public void RemoveListener(Tkey eventType, Action<TValue> eventHandler)
    {
        if (eventDic.TryGetValue(eventType, out var callbacks))
        {
            //Delegate.RemoveAll()方法会从对应的事件方法注册表中移除对应的事件方法
            callbacks = (Action<TValue>)Delegate.RemoveAll(callbacks, eventHandler);
            if (callbacks == null)
            {
                eventDic.Remove(eventType);
            }
            else
            {
                eventDic[eventType] = callbacks;
            }
        }
    }

    /// <summary>
    /// 发送事件
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="eventHandler"></param>
    public void SendMessage(Tkey eventType,TValue eventHandler)
    {
        if (eventDic.TryGetValue(eventType,out var callbacks))
        {
            callbacks.Invoke(eventHandler);
        }
    }

    /// <summary>
    /// 查询事件
    /// </summary>
    /// <param name="eventType"></param>
    /// <returns></returns>
    public bool HasEvent(Tkey eventType)
    {
        return eventDic.ContainsKey(eventType);
    }

    /// <summary>
    /// 清空事件表
    /// </summary>
    public void ClearAll()
    {
        eventDic.Clear();
        Debug.Log("<color=#DC143C>"+"事件表已清空"+"</color>");
    }
}