﻿using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
    public Rigidbody2D rig;
    public PlayerScriptableObject playerData;
    public GameObject bullet;
    public Transform bulletPool;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        transform.AddComponent<PlayerBehavior>();
    }
}