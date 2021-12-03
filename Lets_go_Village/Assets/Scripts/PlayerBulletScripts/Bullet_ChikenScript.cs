using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_ChikenScript : PlayerBulletAdstract
{
    //クールタイムの秒数(プレイヤーに値を渡して使用する)
    [SerializeField] float m_BulletCoolTime;
    //弾の威力
    [SerializeField] float bulletPower;

    //プレイヤーを参照するためのオブジェクト
    GameObject player;

    Rigidbody2D rbody2;

    [SerializeField] Vector2 impulseFoce;

    //弾の生存時間を記憶する変数
    private float elapsedTime = 0;

    private void Start()
    {
        //プレイヤーを記憶
        player = GameObject.Find("WizardVariant");

        //プレイヤーの向きを参照して、弾の移動方向を変更
        impulseFoce.x = impulseFoce.x * player.GetComponent<PlayerController>().m_pDirection;
        //プレイヤーの向きを参照して、弾を反転 & スケールの調整
        gameObject.transform.localScale = new Vector3(
            2.5f * player.GetComponent<PlayerController>().m_pDirection
            , 2.5f, 1);

        rbody2 = gameObject.GetComponent<Rigidbody2D>();

        //弾の移動速度を元に移動
        rbody2.AddForce(impulseFoce, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Chest" ||
             collision.tag == "Map" || collision.tag == "AlphaMap")
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
}

