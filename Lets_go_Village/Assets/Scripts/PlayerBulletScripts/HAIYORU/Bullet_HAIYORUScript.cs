using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_HAIYORUScript: PlayerBulletAdstract
{
    [SerializeField] playerBulletType bulletType;

    [SerializeField] float bulletPower;

    [SerializeField] GameObject haiyoru_manager;


    private void Start()
    {
        //�e�̃}�l�[�W���[���L��
        haiyoru_manager = transform.root.gameObject;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Chest" ||
            collision.tag == "Map" || collision.tag == "AlphaMap"
            || collision.tag == "Slot")
        {
            haiyoru_manager.GetComponent<HAIYORU_Manager>().destroyFlag = true;
        }
    }


    public override float GetCooltime()
    {
        //�Q�Ƃ���Ȃ��\��
        return 0;
    }

    public override float GetBulletPower()
    {
        return bulletPower;
    }

    public override string GetBulletType()
    {
        return bulletType.ToString();
    }
}
