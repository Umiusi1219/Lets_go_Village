using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletManagerScript : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    List<GameObject> playerBulletList;
    [SerializeField]
    GameObject playerBulletUiList;

    public GameObject m_PlayerBullet;
    public float m_HaveBulletCoolTime;



    private void Start()
    {
        //デフォルトのBulletを設定
        SetPlayerBullet(0);
    }


    public void SetPlayerBullet(int num)
    {
        m_PlayerBullet = playerBulletList[num];

        m_HaveBulletCoolTime = m_PlayerBullet.GetComponent<PlayerBulletAdstract>().GetCooltime();
    }

}

