using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletItemScript : MonoBehaviour
{

    [SerializeField]
    playerBulletType playerBulletType;

    [SerializeField] GameObject playerBulletUi;

    private GameObject playerBulletManager;

    private void Start()
    {
        playerBulletManager = GameObject.Find("PlayerBulletManager");

        playerBulletUi = GameObject.Find("PlayerBulletUi");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�v���C���[�ɐڐG�����Ƃ��ɁA���g�̔ԍ��ɑΉ�����Bullet��Set����
        if(collision.tag == "Player")
        {
            //playerBulletManager��set
            playerBulletManager.GetComponent<PlayerBulletManagerScript>().SetPlayerBullet((int)playerBulletType);

            //playerBulletUi��set
            playerBulletUi.GetComponent<PlayerBulletUiScript>().ChangePlayerBulletUi((int)playerBulletType);

            //���g������
            Destroy(gameObject); 
        }

        //�G�l�~�[�ɐڐG������A���g���폜
        if (collision.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
