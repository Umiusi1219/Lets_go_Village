using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    // playerステータス
    public int m_playerHP = 3;
    public int m_playerHPMAX = 3;
    public float m_pMovePower = 0.5f;
    public float m_pjumpPower = 15f; //Set Gravity Scale in Rigidbody2D Component to 5
    public bool m_pDoAttack = true;  //　playerAttack可能かどうか

    [SerializeField] private float pKnockBackPoewr;
    //ダメージ直後の無敵時間
    [SerializeField] private float invincibleTime;

    private Rigidbody2D rb;
    public Animator anim;
    Vector3 movement;
    public int m_pDirection = 1;
    bool isJumping = false;
    private bool alive = true;
    private bool possibleHurt = true;


    [SerializeField] GameObject playerBulletManager;

    [SerializeField] GameObject playerHpUi;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Restart();
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
            if (m_playerHP <= 0)
            {
                Die();
            }
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            Hurt();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            Restart();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Map")
        {
            anim.SetBool("isJump", false);
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
        if ((Input.GetButtonDown("Jump") || Input.GetAxisRaw("Vertical") > 0)
        && !anim.GetBool("isJump"))
        {
            isJumping = true;
            anim.SetBool("isJump", true);
        }
        if (!isJumping)
        {
            return;
        }

        rb.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0, m_pjumpPower);
        rb.AddForce(jumpVelocity, ForceMode2D.Impulse);

        isJumping = false;
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //アタックアニメーションを再生
            anim.SetTrigger("attack");
            //クールタイム
            StartCoroutine(AtaackCoolTime(playerBulletManager.GetComponent<PlayerBulletManagerScript>().
            m_PlayerBullet.GetComponent<PlayerBulletScript>().m_BulletCoolTime));

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
        alive = false;
        anim.SetTrigger("die");

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
}

