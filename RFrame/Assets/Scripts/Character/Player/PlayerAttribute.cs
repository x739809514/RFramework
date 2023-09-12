using System;
using UnityEngine;

public class PlayerAttribute : MonoBehaviour
{
    public PlayerScriptableObject playerScriptableObject;


#region Properties

    public float WalkSpeed
    {
        get => playerScriptableObject.walkSpeed;
        set => playerScriptableObject.walkSpeed = value;
    }

    public float JumpSpeed
    {
        get => playerScriptableObject.jumpSpeed;
        set => playerScriptableObject.jumpSpeed = value;
    }

    public float Hp => playerScriptableObject.level + 5;


    public float Defense => playerScriptableObject.level + 2;


    public float Attack => playerScriptableObject.level * 2;

    public int Level => playerScriptableObject.level;

    public int Exp
    {
        get => playerScriptableObject.exp;
        set => playerScriptableObject.exp = value;
    }

#endregion


#region Override

    private void Start()
    {
        AddListener();
    }

    private void OnDestroy()
    {
        RemoveListener();
    }

#endregion


#region Method

    private void AddExp(object msg)
    {
        var exp = (int)msg;
        Exp += exp;
        playerScriptableObject.level = Exp / 10;
        Debug.Log(Exp);
        Debug.Log(Level);
    }

#endregion


#region Addlistener

    private void AddListener()
    {
        EventSystem.AddListener(EventEnumType.BattleSettlementEvent, AddExp);
    }

    private void RemoveListener()
    {
        EventSystem.RemoveListener(EventEnumType.BattleSettlementEvent, AddExp);
    }

#endregion
}