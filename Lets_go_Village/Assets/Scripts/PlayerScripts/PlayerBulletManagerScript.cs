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



    private void Start()
    {
        //�f�t�H���g��Bullet��ݒ�
        m_PlayerBullet = playerBulletList[0];
    }


    public void SetPlayerBullet(int num)
    {
        m_PlayerBullet = playerBulletList[num];

    }


}

