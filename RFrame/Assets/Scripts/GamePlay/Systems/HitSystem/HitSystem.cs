using System;
using Unity.VisualScripting;
using UnityEngine;
using Object = System.Object;

public class HitSystem : MonoBehaviour
{
#region Overrride

    private void Start()
    {
        AddListener();
    }

    private void OnDestroy()
    {
        RemoveListener();
    }

#endregion

    /// <summary>
    /// 简单的伤害计算
    /// </summary>
    private void CommonDamage(Object msg)
    {
        //Todo:计算伤害
        EventSystem.Dispatch(EventEnumType.EnemyGetHitEvent,5);
    }

#region AddListener

    private void AddListener()
    {
        EventSystem.AddListener(EventEnumType.PlayerAttackDamageEvent, CommonDamage);
    }

    private void RemoveListener()
    {
        EventSystem.RemoveListener(EventEnumType.PlayerAttackDamageEvent, CommonDamage);
    }

#endregion
}