using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HAIYORU_Manager : PlayerBulletAdstract
{
    [SerializeField] public bool m_HAIYORU_UnderSensor;

    [SerializeField] public bool m_HAIYORU_ForwardSensor = false;

    [SerializeField]　public bool destroyFlag = false;

    //自動消滅までの時間
    [SerializeField] float bulletAliveTime;
    //クールタイムの秒数(プレイヤーに値を渡して使用する)
    [SerializeField] float m_BulletCoolTime;

    //プレイヤーを参照するためのオブジェクト
    GameObject player;

    Rigidbody2D rbody2D;

    //弾の生存時間を記憶する変数
    private float elapsedTime = 0;

    //弾の速度
    [SerializeField] float bulletSpeed;
    private void Start()
    {
        m_HAIYORU_UnderSensor = true;

        //プレイヤーを記憶
        player = GameObject.Find("WizardVariant");

        rbody2D = gameObject.GetComponent<Rigidbody2D>();

        //プレイヤーの向きを参照して、弾の移動方向を変更
        bulletSpeed = bulletSpeed * player.GetComponent<PlayerController>().m_pDirection;

        gameObject.transform.position -= new Vector3(0,0.8f,0);

        //プレイヤーの向きを参照して、弾を反転 & スケールの調整
        gameObject.transform.localScale = new Vector3(
            2.5f * player.GetComponent<PlayerController>().m_pDirection
            , 2.5f, 1);

    }
    private void FixedUpdate()
    {


        //生存中時間を記憶
        elapsedTime += Time.deltaTime;

        //弾の移動速度と、親オブジェクトのセンサーを基準に移動

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

        //生存時間が、設定されている自動消滅までの時間を上回ったら自身を消去
        if (bulletAliveTime < elapsedTime)
        {
            Destroy(gameObject);
        }
        //子オブジェクトによって、フラグがtrueにされたら、自身を消去
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

}
