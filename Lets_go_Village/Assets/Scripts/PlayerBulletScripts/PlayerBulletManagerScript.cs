using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletManagerScript : MonoBehaviour
{
    [SerializeField]
    List<GameObject> playerBulletList;

    public GameObject m_PlayerBullet;
    public float m_HaveBulletCoolTime;


    private void Start()
    {
        //�f�t�H���g��Bullet��ݒ�
        SetPlayerBullet(0);
    }

    public void SetPlayerBullet(int num)
    {
        m_PlayerBullet = playerBulletList[num];

        m_HaveBulletCoolTime = m_PlayerBullet.GetComponent<PlayerBulletAdstract>().GetCooltime();
    }

}

