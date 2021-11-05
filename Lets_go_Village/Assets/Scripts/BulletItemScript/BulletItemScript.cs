using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletItemScript : MonoBehaviour
{

    [SerializeField]
    private int itemNum;

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
            playerBulletManager.GetComponent<PlayerBulletManagerScript>().SetPlayerBullet(itemNum);

            playerBulletUi.GetComponent<PlayerBulletUiScript>().ChangePlayerBulletUi(itemNum);

            Destroy(gameObject); 
        }

        //�G�l�~�[�ɐڐG������A���g���폜
        if (collision.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
