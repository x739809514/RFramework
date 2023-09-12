using System;
using Tool;
using Unity.VisualScripting;
using UnityEngine;

public class Ocar : Enemy
{
    public EnemyAttribute enemyAttribute;

    private void Awake()
    {
        enemyAttribute = transform.AddComponent<EnemyAttribute>();
    }


#region Override

    protected override void OnStart()
    {
        base.OnStart();
        InitOcar();
    }

    protected override void DoDamage(object msg)
    {
        if (msg is HitData data && data.characterType == CharacterType.Ocar)
        {
            enemyAttribute.Hp = data.damage - enemyAttribute.Defense;
            if (enemyAttribute.Hp <= 0)
            {
                EnemyDeath.Instance.OnDeath(gameObject, data.characterType);
            }
        }
    }

#endregion


#region 伤害测试

    private void InitOcar()
    {
        enemyAttribute.Hp = 10;
        enemyAttribute.Level = 1;
    }

#endregion
}