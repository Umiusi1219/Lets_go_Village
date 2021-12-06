using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject playerParent;

    // playerステータス
    public int m_playerHP = 3;
    public int m_playerHPMAX = 3;
    public float m_pMovePower = 0.5f;
    public float m_pjumpPower = 8f; //Set Gravity Scale in Rigidbody2D Component to 5
    public bool m_pDoAttack = true;  //　playerAttack可能かどうか

    [SerializeField] private float pKnockBackPoewr;
    //ダメージ直後の無敵時間
    [SerializeField] private float invincibleTime;

    private Rigidbody2D rb;
    public Animator anim;
    Vector3 movement;
    public int m_pDirection = 1;
    public bool isJumping = false;
    [SerializeField] private bool keepPressingSpace;
    [SerializeField] private bool pressSpace;
    [SerializeField] private float jumpTime = 0;
    [SerializeField] private float jumpTimeLimit = 0.5f;
    private bool alive = true;
    private bool possibleHurt = true;

    [SerializeField] GameObject playerBulletManager;

    [SerializeField] GameObject playerHpUi;

    [SerializeField] GameObject gameSceneManager;
    [SerializeField] GameObject blackoutUI;
    [SerializeField] private float toGameOverTime = 1;


    [SerializeField] Vector3[] generatPoints = new Vector3[4];

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Restart();

        playerParent.transform.position = generatPoints[CheckPointScript.m_nowCheckpoint];
    }

    private void Update()
    {
        
        if (alive)
        {

            if(m_pDoAttack)
            {
                Jump();
                Run();
                Attack();
            }
            if (m_playerHP == 0)
            {
                Die();
            }
        }


        if (Input.GetKeyDown(KeyCode.U))
        {
            Restart();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Map" || other.tag == "AlphaMap"
            || other.tag == "Chest")
        {
            anim.SetBool("isJump", false);
            keepPressingSpace = false;
            pressSpace = false;
            jumpTime = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Map")
        {
            anim.SetBool("isJump", true);
        }
    }


    void Run()
    {

        Vector3 moveVelocity = Vector3.zero;
        anim.SetBool("isRun", false);


        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            m_pDirection = -1;
            moveVelocity = Vector3.left;

            transform.localScale = new Vector3(m_pDirection, 1, 1);
            if (!anim.GetBool("isJump"))
                anim.SetBool("isRun", true);

        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            m_pDirection = 1;
            moveVelocity = Vector3.right;

            transform.localScale = new Vector3(m_pDirection, 1, 1);
            if (!anim.GetBool("isJump"))
                anim.SetBool("isRun", true);

        }
        transform.position += moveVelocity * m_pMovePower * Time.deltaTime;

    }
    



    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !anim.GetBool("isJump") && !pressSpace)
        {
            isJumping = true;
            anim.SetBool("isJump", true);
            pressSpace = true;
        }
        else if ((Input.GetKey(KeyCode.Space) && jumpTime < jumpTimeLimit) && pressSpace)
        {
            jumpTime += Time.deltaTime;

            isJumping = true;
            keepPressingSpace = true;
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            keepPressingSpace = false;
            pressSpace = false;
        }

        if (!isJumping || jumpTime > jumpTimeLimit || !keepPressingSpace)
        {
            return;
        }

        rb.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0, m_pjumpPower);
        rb.AddForce(jumpVelocity, ForceMode2D.Impulse);

        if(keepPressingSpace)
        {
            Vector2 jumpVelocity2 = new Vector2(0, (m_pjumpPower / 160)* jumpTime);
            rb.AddForce(jumpVelocity2, ForceMode2D.Impulse);
        }

        isJumping = false;
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //アタックアニメーションを再生
            anim.SetTrigger("attack");
            //クールタイム
            StartCoroutine(AtaackCoolTime(playerBulletManager.GetComponent
                <PlayerBulletManagerScript>().m_HaveBulletCoolTime));

        }
    }

    //HPを減らしてノックバック
    void Hurt()
    {
        if (0 < m_playerHP)
        {
            m_playerHP--;
            anim.SetTrigger("hurt");
            rb.AddForce(new Vector2(pKnockBackPoewr * m_pDirection, 1f), ForceMode2D.Impulse);
        }

        playerHpUi.GetComponent<PlayerHPScript>().ChangePlayerHpUi(m_playerHPMAX, m_playerHP);
    }

    void Die()
    {
        if(alive)
        {
            m_playerHP = 0;
            playerHpUi.GetComponent<PlayerHPScript>().ChangePlayerHpUi(m_playerHPMAX, m_playerHP);
            alive = false;
            anim.SetTrigger("die");
            StartCoroutine(DieTime());
        }
    }

    void Restart()
    {
        m_playerHP = m_playerHPMAX;
        anim.SetTrigger("idle");
        alive = true;
        anim.SetBool("isJump", false);
        playerHpUi.GetComponent<PlayerHPScript>().ChangePlayerHpUi(m_playerHPMAX, m_playerHP);
    }


    //所持している弾をplayerの位置をもとにした場所に生み出す
    public void ShootPlayerBullet()
    {
        Instantiate(playerBulletManager.GetComponent<PlayerBulletManagerScript>().m_PlayerBullet, 
            new Vector3(gameObject.transform.position.x + 0.5f* m_pDirection,
            gameObject.transform.position.y + 1.5f, 0.0f), Quaternion.identity);
    }

    //プレイヤーのアタックを使用しているBulletで決めてあるCoolTime分使用不可にする関数
    IEnumerator AtaackCoolTime(float Time)
    {
        //playerをアタック不可にする
        m_pDoAttack = false;

        //使用しているBulletのクールタイム分停止
        yield return new WaitForSeconds(Time/2);

        //所持している弾の生成
        ShootPlayerBullet();

        //使用しているBulletのクールタイム分停止
        yield return new WaitForSeconds(Time / 2);

        //playerをアタック可能にする
        m_pDoAttack = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy"　&& possibleHurt)
        {
            {
                Hurt();
                StartCoroutine(HurtCoolTime());
            }
        }

        if (collision.gameObject.tag == "DeathDed" && alive)
        {
            {
                Die();
            }
        }
    }

    IEnumerator HurtCoolTime()
    {
        //playerがダメージを受けないようにする
        possibleHurt = false;


        //使用しているBulletのクールタイム分停止
        yield return new WaitForSeconds(invincibleTime);

        //playerがダメージを受けるようにする
        possibleHurt = true;
    }

    IEnumerator DieTime()
    {
        blackoutUI.GetComponent<BlackoutScript>().Onbool_doShow();

        //使用しているBulletのクールタイム分停止
        yield return new WaitForSeconds(toGameOverTime);

        gameSceneManager.GetComponent<SceneManagerScript>().PlayerDie();
    }
}

