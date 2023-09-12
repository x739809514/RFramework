using Tool;
using UnityEngine;

public class EnemyDeath : SingletonMono<EnemyDeath>
{
    public void OnDeath(GameObject go, CharacterType characterType)
    {
        Destroy(go);
        //Todo:查表获得杀敌经验,目前先从敌人属性中获得
        var exp = 0;
        switch (characterType)
        {
            case CharacterType.Goblin:
                exp = 5;
                break;
            case CharacterType.Ocar:
                exp = 10;
                break;
        }

        EventSystem.Dispatch(EventEnumType.BattleSettlementEvent, exp);
    }
}