using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{
    public Rigidbody2D rig;
    public PlayerScriptableObject playerData;
    public GameObject bullet;
    public Transform bulletPool;
    public GameObject mark;
    public Transform markPool;
    [HideInInspector] public PlayerAttribute playerAttribute;


#region Override

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        playerAttribute = transform.GetComponent<PlayerAttribute>();
    }

    private void Start()
    {
        InitPlayer();
        playerAttribute.playerScriptableObject = playerData;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Exit"))
        {
            var changeScene = other.transform.GetComponent<ChangeScene>();
            changeScene.Teleport();
        }
    }

#endregion


#region 玩家伤害系统测试

    private void InitPlayer()
    {
        playerAttribute.JumpSpeed = 7;
        playerAttribute.WalkSpeed = 5;
    }

#endregion
}