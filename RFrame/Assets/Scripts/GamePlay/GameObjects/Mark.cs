using System;
using UnityEngine;

public class Mark : Bullets
{
    private Transform player;
    
#region Override

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    protected override void OnTrigger(Collider2D go)
    {
        if (go.gameObject.layer==LayerMask.NameToLayer("character"))
        {
            var transform1 = go.transform;
            (transform1.position, player.position) = (player.position, transform1.position);
        }
    }

#endregion
}