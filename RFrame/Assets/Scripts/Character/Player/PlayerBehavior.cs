using System.Collections;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private GameObject checkGroundObj;
    private bool readyToJump;
    private bool isGround;
    private PlayerScriptableObject playerData;
    private Player player;

    public bool jumpReset=false; //在空中重置跳跃

    private void Awake()
    {
        player = GetComponent<Player>();
        checkGroundObj = transform.Find("checkGround").gameObject;
        playerData = player.playerData;
    }

    private void Start()
    {
        ObjectPool<PlayerBullets>.Instance.InitPool(player.bulletPool, player.bullet);
        ObjectPool<Mark>.Instance.InitPool(player.markPool, player.mark);
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
            jumpReset = false;
        }
    }


#region Behavior

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
        if (Input.GetKeyDown(KeyCode.Space) && (isGround || jumpReset))
        {
            readyToJump = true;
        }
    }

    private void Jump()
    {
        player.rig.velocity = new Vector2(player.rig.velocity.x, playerData.jumpSpeed);
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //射击实弹
            var bullet = ObjectPool<PlayerBullets>.Instance.Spawn();
            StartCoroutine(OnVanishBullet(bullet));
        }

        if (Input.GetMouseButtonDown(1))
        {
            //射击标记
            var mark = ObjectPool<Mark>.Instance.Spawn();
            StartCoroutine(OnVanishMark(mark));
        }
    }

#endregion


#region IEnumerator

    IEnumerator OnVanishBullet(PlayerBullets playerBullet)
    {
        yield return new WaitForSeconds(1f);

        ObjectPool<PlayerBullets>.Instance.Recycle(playerBullet);
    }


    IEnumerator OnVanishMark(Mark mark)
    {
        yield return new WaitForSeconds(1f);

        ObjectPool<Mark>.Instance.Recycle(mark);
    }

#endregion
}