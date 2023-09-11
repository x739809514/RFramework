using UnityEngine;

public class EnemyAttribute : MonoBehaviour
{
    private float attack;
    private float defense;

    /// <summary>
    /// 血量
    /// </summary>
    public float Hp { get; set; }
    
    /// <summary>
    /// 怪物等级
    /// </summary>
    public int Level { get; set; }

    /// <summary>
    /// 怪物攻击力
    /// </summary>
    public float Attack => Level * 2;

    /// <summary>
    /// 怪物防御
    /// </summary>
    public float Defense => Level + 2;
}