using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HAIYORU_ForwardSensorScript : MonoBehaviour
{
    [SerializeField] playerBulletType bulletType;

    [SerializeField] GameObject haiyoru_manager;

    // Start is called before the first frame update
    void Start()
    {
        haiyoru_manager = transform.root.gameObject;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Map")
        {
            haiyoru_manager.GetComponent<HAIYORU_Manager>().m_HAIYORU_ForwardSensor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Map")
        {
            haiyoru_manager.GetComponent<HAIYORU_Manager>().m_HAIYORU_ForwardSensor = false;
        }
    }
}
