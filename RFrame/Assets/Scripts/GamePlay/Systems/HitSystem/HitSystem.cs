using Tool;
using UnityEngine;
using Object = System.Object;

public class HitSystem : MonoBehaviour
{
    public PlayerScriptableObject playerData;

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
        var enemyTransform = msg as Transform;
        var damage = 0f;
        var characterType = CharacterType.Null;
        if (enemyTransform!=null)
        {
            characterType = enemyTransform.GetComponent<Enemy>().characterType;
            switch (characterType)
            {
                case CharacterType.Goblin:
                    var goblin = enemyTransform.GetComponent<Goblin>();
                    damage = playerData.attack - goblin.enemyAttribute.Defense;
                    break;
                case CharacterType.Ocar:
                    var ocar = enemyTransform.GetComponent<Ocar>();
                    damage = playerData.attack = ocar.enemyAttribute.Defense;
                    break;
            }
        }
        EventSystem.Dispatch(EventEnumType.EnemyGetHitEvent, new HitData()
        {
            damage = damage,
            characterType = characterType
        });
        
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


public class HitData
{
    public float damage;
    public CharacterType characterType;
}