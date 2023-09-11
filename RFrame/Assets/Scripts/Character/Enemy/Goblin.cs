using System;
using Tool;
using Unity.VisualScripting;

public class Goblin : Enemy
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
        InitGoblin();
    }

    protected override void DoDamage(object msg)
    {
        if (msg is HitData data && data.characterType == CharacterType.Goblin)
        {
            enemyAttribute.Hp = data.damage - enemyAttribute.Defense;

            if (enemyAttribute.Hp < 0)
            {
                Destroy(gameObject);
            }
        }
    }

#endregion


#region 伤害测试

    private void InitGoblin()
    {
        enemyAttribute.Hp = 5;
        enemyAttribute.Level = 1;
    }

#endregion
}