using System;

public static class EventSystem
{
    private static EventSender<Enum, Object> sender = new EventSender<Enum, object>();

    /// <summary>
    /// 添加事件监听器
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="eventHandler"></param>
    public static void AddListener(Enum eventType, Action<Object> eventHandler)
    {
        sender.AddListener(eventType, eventHandler);
    }

    /// <summary>
    /// 移除事件监听器
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="eventHandler"></param>
    public static void RemoveListener(Enum eventType, Action<Object> eventHandler)
    {
        sender.RemoveListener(eventType, eventHandler);
    }

    /// <summary>
    /// 发送事件
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="eventArg"></param>
    public static void Dispatch(Enum eventType, Object eventArg)
    {
        sender.SendMessage(eventType, eventArg);
    }

    /// <summary>
    /// 发送事件 0个参数
    /// </summary>
    /// <param name="eventType"></param>
    public static void Dispatch(Enum eventType)
    {
        sender.SendMessage(eventType, null);
    }

    /// <summary>
    /// 查询事件
    /// </summary>
    /// <param name="eventType"></param>
    public static void HasEvent(Enum eventType)
    {
        sender.HasEvent(eventType);
    }

    /// <summary>
    /// 清除事件表
    /// </summary>
    public static void Clear()
    {
        sender.ClearAll();
    }
}