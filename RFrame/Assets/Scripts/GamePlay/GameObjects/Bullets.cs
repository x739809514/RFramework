using UnityEngine;

public class Bullets : MonoBehaviour
{
    private float nextFire;
    private readonly float fireRate = 1f;
    private readonly float bulletSpeed = 5f;

    private void FixedUpdate()
    {
        BulletLaunch();
    }

    protected virtual void BulletLaunch()
    {
        nextFire += Time.deltaTime;
        if (nextFire > fireRate)
        {
            var worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldMousePos.z = 0;
            var angle = Vector2.Angle(worldMousePos - transform.position, Vector2.up);

            if (worldMousePos.x < transform.position.x)
                angle = -angle;

            nextFire = 0f;

            Transform transform1;
            (transform1 = transform).GetComponent<Rigidbody2D>().velocity =
                (worldMousePos - transform.position).normalized * bulletSpeed;
            transform1.eulerAngles = new Vector3(0, 0, angle);
        }
    }
}