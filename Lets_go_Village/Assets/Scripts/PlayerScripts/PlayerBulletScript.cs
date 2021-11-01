using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    List<GameObject> playerBulletList;
    public GameObject playerBullet;

    public void SetplayerBullet(int num)
    {
        playerBullet = playerBulletList[num];
    }


}

