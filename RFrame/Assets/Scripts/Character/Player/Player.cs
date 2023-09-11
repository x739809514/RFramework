using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
    public Rigidbody2D rig;
    public PlayerScriptableObject playerData;
    public GameObject bullet;
    public Transform bulletPool;
    public GameObject mark;
    public Transform markPool;
    [HideInInspector] public PlayerAttribute playerAttribute;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        transform.AddComponent<PlayerBehavior>();
        playerAttribute = transform.AddComponent<PlayerAttribute>();
    }

    private void Start()
    {
        InitPlayer();
        playerAttribute.playerScriptableObject = playerData;
    }

#region 玩家伤害系统测试

    private void InitPlayer()
    {
        playerData.hp = 10;
        playerData.attack = 5;
        playerData.jumpSpeed = 7;
        playerData.walkSpeed = 5;
    }

#endregion
}