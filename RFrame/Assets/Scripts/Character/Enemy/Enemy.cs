public class Enemy : Character
{
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

#region Hurt

    private void GetHurt(object msg)
    {
        var damage = (int)msg;
        Destroy(gameObject);
    }

#endregion

#region AddListener

    private void AddListener()
    {
        EventSystem.AddListener(EventEnumType.EnemyGetHitEvent, GetHurt);
    }

    private void RemoveListener()
    {
        EventSystem.RemoveListener(EventEnumType.EnemyGetHitEvent, GetHurt);
    }

#endregion
}