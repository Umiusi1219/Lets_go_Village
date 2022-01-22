using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HAIYORU_UnderSensorScript : MonoBehaviour
{
    [SerializeField] playerBulletType bulletType;

    [SerializeField] GameObject haiyoru_manage;

    // Start is called before the first frame update
    void Start()
    {
        haiyoru_manage = transform.root.gameObject;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Map")
        {
            haiyoru_manage.GetComponent<HAIYORU_Manager>().m_HAIYORU_UnderSensor = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Map")
        {
            haiyoru_manage.GetComponent<HAIYORU_Manager>().m_HAIYORU_UnderSensor = true;
        }
    }
}
