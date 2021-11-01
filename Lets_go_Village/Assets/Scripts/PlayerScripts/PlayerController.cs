using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    // player�X�e�[�^�X
    public int m_playerHP = 3;
    public float m_pMovePower = 10f;
    public float m_pjumpPower = 15f; //Set Gravity Scale in Rigidbody2D Component to 5
    public bool m_pDoAttack = true;  //�@playerAttack�\���ǂ���

    private Rigidbody2D rb;
    public Animator anim;
    Vector3 movement;
    public int m_pDirection = 1;
    bool isJumping = false;
    private bool alive = true;


    [SerializeField] GameObject playerBulletManager;
    
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
            
            Jump();
            Run();

            if(m_pDoAttack)
            {
                Attack();

            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        anim.SetBool("isJump", false);
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
            //�A�^�b�N�A�j���[�V�������Đ�
            anim.SetTrigger("attack");
            //�������Ă���e�̐���
            ShootPlayerBullet();

            //�N�[���^�C��
            StartCoroutine(AtaackCoolTime(playerBulletManager.GetComponent<PlayerBulletManagerScript>().
            m_PlayerBullet.GetComponent<PlayerBulletScript>().m_BulletCoolTime));
        }
    }
    void Hurt()
    {

        anim.SetTrigger("hurt");
        if (m_pDirection == 1)
        {
            rb.AddForce(new Vector2(-5f, 1f), ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(new Vector2(5f, 1f), ForceMode2D.Impulse);
        }
    }

    void Die()
    {
        anim.SetTrigger("die");
        alive = false;

    }

    void Restart()
    {
 
        anim.SetTrigger("idle");
        alive = true;

    }


    public void ShootPlayerBullet()
    {
        Instantiate(playerBulletManager.GetComponent<PlayerBulletManagerScript>().m_PlayerBullet, 
            new Vector3(gameObject.transform.position.x + 0.5f* m_pDirection,
            gameObject.transform.position.y + 1.5f, 0.0f), Quaternion.identity);
    }

    //�v���C���[�̃A�^�b�N���g�p���Ă���Bullet�Ō��߂Ă���CoolTime���g�p�s�ɂ���֐�
    IEnumerator AtaackCoolTime(float Time)
    {
        //player���A�^�b�N�s�ɂ���
        m_pDoAttack = false;

        //�g�p���Ă���Bullet�̃N�[���^�C������~
        yield return new WaitForSeconds(Time);

        //player���A�^�b�N�\�ɂ���
        m_pDoAttack = true;
    }
}

