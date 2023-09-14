using Tool;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyFactory : MonoBehaviour,IEnemyFactory 
{
    public GameObject GenerateEnemy(GameObject prefab, CharacterType characterType)
    {
        var enemy = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        enemy.SetActive(true);
        var temp = enemy.GetComponent<Enemy>();
        temp.characterType = characterType;
        //Todo: read XML
        /*temp.hp = 3;
        temp.defense = 1;*/
        
        return enemy;
    }
}
