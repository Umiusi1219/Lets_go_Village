using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    //弾の速度
    [SerializeField] float bulletSpeed;
    //自動消滅までの時間
    [SerializeField] float bulletAliveTime;
    //クールタイムの秒数(プレイヤーに値を渡して使用する)
    public float m_BulletCoolTime;
    //弾の威力
    [SerializeField]
    int bulletPower;

    //プレイヤーを参照するためのオブジェクト
    GameObject player;

    //弾の生存時間を記憶する変数
    private float elapsedTime = 0;

    private void Start()
    {
        //プレイヤーを記憶
        player = GameObject.Find("WizardVariant");

        //プレイヤーの向きを参照して、弾の移動方向を変更
        bulletSpeed = bulletSpeed * player.GetComponent<PlayerController>().m_pDirection;
        //プレイヤーの向きを参照して、弾を反転 & スケールの調整
        gameObject.transform.localScale = new Vector3(
            2.5f * player.GetComponent<PlayerController>().m_pDirection
            ,2.5f, 1);
            
    }
    private void FixedUpdate()
    {
        //生存中時間を記憶
        elapsedTime += Time.deltaTime;

        //弾の移動速度を元に移動
        gameObject.transform.position += new Vector3(bulletSpeed, 0, 0);

        //生存時間が、設定されている自動消滅までの時間を上回ったら自身を消去
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
