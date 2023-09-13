using UnityEngine;

public class Mark : Bullets
{
    private Transform player;
    private PlayerBehavior behavior;


#region Override

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        behavior = player.GetComponent<PlayerBehavior>();
    }

    protected override void OnTrigger(Collider2D go)
    {
        if (go.CompareTag("Player") == false)
        {
            ObjectPool<Mark>.Instance.Recycle(this);
        }

        if (go.gameObject.layer == LayerMask.NameToLayer("character"))
        {
            var transform1 = go.transform;
            (transform1.position, player.position) = (player.position, transform1.position);
        }

        if (go.CompareTag("Teleport"))
        {
            player.position = go.transform.position;
            behavior.jumpReset = true;
        }
    }

#endregion
}