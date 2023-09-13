using UnityEngine;

public class PlayerBullets : Bullets
{
    protected override void OnTrigger(Collider2D go)
    {
        if (go.CompareTag("Player")==false)
        {
            ObjectPool<PlayerBullets>.Instance.Recycle(this);
        }
        if (go.CompareTag("Enemy"))
        {
            EventSystem.Dispatch(EventEnumType.PlayerAttackDamageEvent,go.transform);
        }
    }
}