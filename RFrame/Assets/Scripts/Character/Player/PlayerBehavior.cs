using System.Collections;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private GameObject checkGroundObj;
    private bool readyToJump;
    private bool isGround;
    private PlayerScriptableObject playerData;

    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
        checkGroundObj = transform.Find("checkGround").gameObject;
        playerData = player.playerData;
    }

    private void Start()
    {
        ObjectPool<Bullets>.Instance.InitPool(player.bulletPool, player.bullet);
    }

    private void Update()
    {
        Walk();
        ReadyToJump();
        Shoot();
    }

    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(checkGroundObj.transform.position, 0.1f, LayerMask.GetMask("Ground"));
        if (readyToJump)
        {
            Jump();
            readyToJump = false;
        }
    }


#region Movement

    private void Walk()
    {
        var horizontalVal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(horizontalVal - 0f) > 0.01f)
        {
            player.rig.velocity = new Vector2(horizontalVal * playerData.walkSpeed, player.rig.velocity.y);
        }
    }

    private void ReadyToJump()
    {
        if (Input.GetKeyDown(KeyCode.J) && isGround)
        {
            readyToJump = true;
        }
    }

    private void Jump()
    {
        player.rig.velocity = new Vector2(player.rig.velocity.x, playerData.jumpSpeed);
    }

#endregion

#region Shoot

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //Todo:射击逻辑
            var bullet = ObjectPool<Bullets>.Instance.Spawn();
            bullet.player = player;
            
            StartCoroutine(BulletVanish(bullet));
        }
    }

    IEnumerator BulletVanish(Bullets bullet)
    {
        yield return new WaitForSeconds(1f);
        
        ObjectPool<Bullets>.Instance.Recycle(bullet);
    }

#endregion
}