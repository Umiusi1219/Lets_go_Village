using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianSensorScript : MonoBehaviour
{
    public bool onObj;

    [SerializeField] int rlNum;
    [SerializeField] int distance;

    GameObject player;

    private void Start()
    {
        onObj = false;

        player = player = GameObject.Find("WizardVariant");
    }

    private void Update()
    {
        gameObject.transform.position = new Vector3(player.transform.position.x +
            (distance * rlNum), player.transform.position.y + 1.5f, 0);
    }

    public bool GetOnObj()
    {
        return onObj;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Map" || collision.tag == "Enemy")
        {
            onObj = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Map" || collision.tag == "Enemy")
        {
            onObj = false;
        }
    }
}
