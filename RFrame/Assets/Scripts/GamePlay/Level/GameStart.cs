using UnityEngine;

public class GameStart : MonoBehaviour
{
    public LevelScriptableObject levelData;
    private void Awake()
    {
        GeneratePlayer();
        GenerateEnemy();
    }

    private void GeneratePlayer()
    {
        var factory = gameObject.AddComponent<PlayerFactory>();
        var player = factory.GeneratePlayer();
        player.transform.position = levelData.playerBirthPos;
    }

    private void GenerateEnemy()
    {
        var factory = gameObject.AddComponent<EnemyFactory>();

        foreach (var data in levelData.enemyPool)
        {
           var enemy = factory.GenerateEnemy(data.enemyPre, data.characterType);
           enemy.transform.position = data.birthPos;
        }
    }
}