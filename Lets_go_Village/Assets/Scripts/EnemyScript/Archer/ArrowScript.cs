using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : EnemyAdstract
{
    [SerializeField] int arrowPower;

    [SerializeField] float toPlayerDistance;

    [SerializeField] GameObject player;

    Rigidbody2D rbody2d;

    [SerializeField] Vector3 shootPowerVecter;

    public override int GetEnemyPower()
    {
        return arrowPower;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("WizardVariant");

        toPlayerDistance = player.transform.position.x - gameObject.transform.position.x;

        if (toPlayerDistance < 0)
        {
            gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        if (0 < toPlayerDistance)
        {
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        rbody2d = gameObject.GetComponent<Rigidbody2D>();

        rbody2d.AddForce( new Vector3(shootPowerVecter.x*toPlayerDistance, shootPowerVecter.y -transform.position.y
            ,0) , ForceMode2D.Impulse);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" || collision.tag == "Playerbullet" 
            || collision.tag == "Chest" || collision.tag == "Map" 
            || collision.tag == "AlphaMap")
        {
            Destroy(gameObject);
        }

    }
}
