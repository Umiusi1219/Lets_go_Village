using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_MountainScript : PlayerBulletAdstract
{
    [SerializeField] playerBulletType bulletType;

    //�N�[���^�C���̕b��(�v���C���[�ɒl��n���Ďg�p����)
    [SerializeField] float m_BulletCoolTime;
    //�e�̈З�
    [SerializeField] float bulletPower;

    //�v���C���[���Q�Ƃ��邽�߂̃I�u�W�F�N�g
    GameObject player;

    Rigidbody2D rbody2;

    [SerializeField] Vector2 impulseFoce;



    private void Start()
    {
        //�v���C���[���L��
        player = GameObject.Find("WizardVariant");

        //�v���C���[�̌������Q�Ƃ��āA�e�̈ړ�������ύX
        impulseFoce.x = impulseFoce.x * player.GetComponent<PlayerController>().m_pDirection;
        //�v���C���[�̌������Q�Ƃ��āA�e�𔽓] & �X�P�[���̒���
        gameObject.transform.localScale = new Vector3(
            2.5f * player.GetComponent<PlayerController>().m_pDirection
            , 2.5f, 1);

        rbody2 = gameObject.GetComponent<Rigidbody2D>();

        //�e�̈ړ����x�����Ɉړ�
        rbody2.AddForce(impulseFoce, ForceMode2D.Impulse);
    }




    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Chest" ||
             collision.tag == "Map" || collision.tag == "AlphaMap"
             || collision.tag == "Slot")
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
        return bulletPower;
    }

    public override string GetBulletType()
    {
        return bulletType.ToString();
    }
}
