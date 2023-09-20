using UnityEngine;

public class LevelStart : MonoBehaviour
{
    public LevelScriptableObject levelData;
    private GameObject teleportParent;
    public Transform enemyParent;

    private void Awake()
    {
        teleportParent = new GameObject("Tlp");
        GeneratePlayer();
        
    }

    private void Start()
    {
        GenerateEnemy();
        GenerateTeleport();
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
            enemy.transform.localPosition = data.birthPos;
            enemy.transform.SetParent(enemyParent);
        }
    }

    private void GenerateTeleport()
    {
        foreach (var teleport in levelData.teleportPos)
        {
            var go = (GameObject)Resources.Load("Prefab/teleport");
            var tel = Instantiate(go, teleportParent.transform);
            tel.SetActive(true);
            tel.transform.localPosition = teleport;
        }
    }
}