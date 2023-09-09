using Tool;
using UnityEngine;

public interface IEnemyFactory : IFactory
{
    public GameObject GenerateEnemy(GameObject prefab, CharacterType characterType);
}