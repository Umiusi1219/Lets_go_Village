using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript1 : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    [SerializeField] float bulletAliveTime;
    GameObject player;

    private void Start()
    {
        player = GameObject.Find("WizardVariant");

        bulletSpeed = bulletSpeed * player.GetComponent<PlayerController>().m_pDirection;
        gameObject.transform.localScale = new Vector3(
            2.5f * player.GetComponent<PlayerController>().m_pDirection
            , 2.5f, 1);
            
    }

    private float elapsedTime = 0;
    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        gameObject.transform.position += new Vector3(bulletSpeed, 0,0);

        if (bulletAliveTime < elapsedTime)
        {
            Destroy(gameObject);
        }
    }
}
