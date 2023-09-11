using UnityEngine;

public class PlayerAttribute : MonoBehaviour
{
    public PlayerScriptableObject playerScriptableObject;

    public float walkSpeed
    {
        get => playerScriptableObject.walkSpeed;
        set => playerScriptableObject.walkSpeed = value;
    }

    public float jumpSpeed
    {
        get => playerScriptableObject.jumpSpeed;
        set => playerScriptableObject.jumpSpeed = value;
    }

    public float hp
    {
        get => playerScriptableObject.hp;
        set => playerScriptableObject.hp = value;
    }

    public float defense
    {
        get => playerScriptableObject.defense;
        set => playerScriptableObject.defense = value;
    }

    public float attack
    {
        get => playerScriptableObject.attack;
        set => playerScriptableObject.attack = value;
    }
}