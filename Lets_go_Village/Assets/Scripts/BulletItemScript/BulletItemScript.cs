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
        //プレイヤーに接触したときに、自身の番号に対応するBulletをSetする
        if(collision.tag == "Player")
        {
            //playerBulletManagerにset
            playerBulletManager.GetComponent<PlayerBulletManagerScript>().SetPlayerBullet(itemNum);

            //playerBulletUiにset
            playerBulletUi.GetComponent<PlayerBulletUiScript>().ChangePlayerBulletUi(itemNum);

            //自身を消去
            Destroy(gameObject); 
        }

        //エネミーに接触したら、自身を削除
        if (collision.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
