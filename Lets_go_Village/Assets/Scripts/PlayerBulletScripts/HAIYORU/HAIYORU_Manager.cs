using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HAIYORU_Manager : PlayerBulletAdstract
{
    [SerializeField] playerBulletType bulletType;

    [SerializeField] public bool m_HAIYORU_UnderSensor;

    [SerializeField] public bool m_HAIYORU_ForwardSensor = false;

    [SerializeField]�@public bool destroyFlag = false;

    //�������ł܂ł̎���
    [SerializeField] float bulletAliveTime;
    //�N�[���^�C���̕b��(�v���C���[�ɒl��n���Ďg�p����)
    [SerializeField] float m_BulletCoolTime;

    //�v���C���[���Q�Ƃ��邽�߂̃I�u�W�F�N�g
    GameObject player;

    Rigidbody2D rbody2D;

    //�e�̐������Ԃ��L������ϐ�
    private float elapsedTime = 0;

    //�e�̑��x
    [SerializeField] float bulletSpeed;
    private void Start()
    {
        m_HAIYORU_UnderSensor = true;

        //�v���C���[���L��
        player = GameObject.Find("WizardVariant");

        rbody2D = gameObject.GetComponent<Rigidbody2D>();

        //�v���C���[�̌������Q�Ƃ��āA�e�̈ړ�������ύX
        bulletSpeed = bulletSpeed * player.GetComponent<PlayerController>().m_pDirection;

        gameObject.transform.position -= new Vector3(0,0.8f,0);

        //�v���C���[�̌������Q�Ƃ��āA�e�𔽓] & �X�P�[���̒���
        gameObject.transform.localScale = new Vector3(
            2.5f * player.GetComponent<PlayerController>().m_pDirection
            , 2.5f, 1);

    }
    private void FixedUpdate()
    {


        //���������Ԃ��L��
        elapsedTime += Time.deltaTime;

        //�e�̈ړ����x�ƁA�e�I�u�W�F�N�g�̃Z���T�[����Ɉړ�

        if (m_HAIYORU_ForwardSensor)
        {
            rbody2D.gravityScale = 0;
            rbody2D.velocity -= new Vector2(0, rbody2D.velocity.y);
            gameObject.transform.position += new Vector3(0, Mathf.Abs(bulletSpeed), 0);
        }
        else if (m_HAIYORU_UnderSensor)
        {
            rbody2D.gravityScale = 1; 
        }
        else
        {
            rbody2D.gravityScale = 0;
            rbody2D.velocity -= new Vector2(0, rbody2D.velocity.y);
            gameObject.transform.position += new Vector3(bulletSpeed, 0, 0);
        }

        //�������Ԃ��A�ݒ肳��Ă��鎩�����ł܂ł̎��Ԃ��������玩�g������
        if (bulletAliveTime < elapsedTime)
        {
            Destroy(gameObject);
        }
        //�q�I�u�W�F�N�g�ɂ���āA�t���O��true�ɂ��ꂽ��A���g������
        if (destroyFlag)
        {
            Destroy(gameObject);
        }
    }


    public override float GetCooltime()
    {
        return m_BulletCoolTime;
    }

    public override float GetBulletPower()
    {
        return 0;
    }

    public override string GetBulletType()
    {
        return bulletType.ToString();
    }
}
