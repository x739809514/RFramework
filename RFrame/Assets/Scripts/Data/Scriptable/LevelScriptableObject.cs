using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelScriptableObject")]
public class LevelScriptableObject : ScriptableObject
{
    public List<EnemyGenerateData> enemyPool;
    public Vector2 playerBirthPos;

    [Header("Teleport")] 
    public List<Vector3> teleportPos;
}