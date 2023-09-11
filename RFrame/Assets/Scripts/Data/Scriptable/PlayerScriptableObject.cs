using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData",menuName = "ScriptableObjects/PlayerScriptableObject")]
public class PlayerScriptableObject : ScriptableObject
{
    /// <summary>
    /// 行走速度
    /// </summary>
    public float walkSpeed;
    
    /// <summary>
    /// 跳跃速度
    /// </summary>
    public float jumpSpeed;
    
    /// <summary>
    /// 生命值
    /// </summary>
    public float hp;
    
    /// <summary>
    /// 防御力
    /// </summary>
    public float defense;

    /// <summary>
    /// 攻击力
    /// </summary>
    public float attack;
}