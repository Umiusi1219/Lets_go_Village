using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    //�e�̑��x
    [SerializeField] float bulletSpeed;
    //�������ł܂ł̎���
    [SerializeField] float bulletAliveTime;
    //�N�[���^�C���̕b��(�v���C���[�ɒl��n���Ďg�p����)
    public float m_BulletCoolTime;
    //�e�̈З�
    [SerializeField]
    int bulletPower;

    //�v���C���[���Q�Ƃ��邽�߂̃I�u�W�F�N�g
    GameObject player;

    //�e�̐������Ԃ��L������ϐ�
    private float elapsedTime = 0;

    private void Start()
    {
        //�v���C���[���L��
        player = GameObject.Find("WizardVariant");

        //�v���C���[�̌������Q�Ƃ��āA�e�̈ړ�������ύX
        bulletSpeed = bulletSpeed * player.GetComponent<PlayerController>().m_pDirection;
        //�v���C���[�̌������Q�Ƃ��āA�e�𔽓] & �X�P�[���̒���
        gameObject.transform.localScale = new Vector3(
            2.5f * player.GetComponent<PlayerController>().m_pDirection
            ,2.5f, 1);
            
    }
    private void FixedUpdate()
    {
        //���������Ԃ��L��
        elapsedTime += Time.deltaTime;

        //�e�̈ړ����x�����Ɉړ�
        gameObject.transform.position += new Vector3(bulletSpeed, 0, 0);

        //�������Ԃ��A�ݒ肳��Ă��鎩�����ł܂ł̎��Ԃ��������玩�g������
        if (bulletAliveTime < elapsedTime)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag =="Map")
        {
            Destroy(gameObject);
        }
    }
}
