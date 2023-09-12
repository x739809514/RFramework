using System;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Character
{
#region Override

    private void Start()
    {
        OnStart();
    }

    private void OnDestroy()
    {
        DoDestory();
    }

#endregion


#region Hurt

    private void GetHurt(object msg)
    {
        DoDamage(msg);
    }

#endregion


#region AddListener

    private void AddListener()
    {
        EventSystem.AddListener(EventEnumType.EnemyGetHitEvent, GetHurt);
    }

    private void RemoveListener()
    {
        EventSystem.RemoveListener(EventEnumType.EnemyGetHitEvent, GetHurt);
    }

#endregion


#region Virtual

    protected virtual void DoDamage(object msg)
    {
    }

    protected virtual void OnStart()
    {
        AddListener();
        transform.AddComponent<EnemyDeath>();
    }

    protected virtual void DoDestory()
    {
        RemoveListener();
    }

#endregion
}