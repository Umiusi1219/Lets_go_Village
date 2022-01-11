using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianScript : EnemyAdstract
{

    [SerializeField] float magicianHp;

    [SerializeField] int magicianPower;

    [SerializeField] bool magicianDed;

    [SerializeField] bool doTeleport;

    [SerializeField] float teleportCoolTime;

    [SerializeField] GameObject player;

    [SerializeField] GameObject rSensor;

    [SerializeField] GameObject lSensor;

    [SerializeField] private int randNum;


    [SerializeField] Vector3 toPlayerDistance;

    // Start is called before the first frame update
    void Start()
    {
        rSensor = GameObject.Find("MagicianRSensor");

        lSensor = GameObject.Find("MagicianLSensor");

        doTeleport = true;

        player = GameObject.Find("WizardVariant");

        gameObject.transform.position += new Vector3(0, 2, 0);

        gameObject.GetComponent<Animator>().SetTrigger("bron");
    }

    // Update is called once per frame
    void Update()
    {
        if (!magicianDed)
        {
            toPlayerDistance = player.transform.position - gameObject.transform.position;



            if (toPlayerDistance.x < 0)
            {
                gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
            }
            if (0 < toPlayerDistance.x)
            {
                gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            }

            if (0 >= magicianHp || 50 <= toPlayerDistance.x)
            {
                dead();
            }

            if(doTeleport)
            {
                StartCoroutine(TeleportTime());
            }
            

        }
    }

    void Hurt(float bulletPower)
    {
        gameObject.GetComponent<Animator>().SetTrigger("hurt");
        magicianHp -= bulletPower;
    }


    public void Attack()
    {
        gameObject.GetComponent<Animator>().SetTrigger("attack");
        StartCoroutine(AttackTime());
    }


    void dead()
    {
        magicianDed = true;
        gameObject.GetComponent<Animator>().SetTrigger("ded");
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        StartCoroutine(DethTime());
    }

    void TeleportMain()
    {
        randNum = Random.Range(1, 2 + 1);

        switch(randNum)
        {
            case 1:
                if(!rSensor.GetComponent<MagicianSensorScript>().GetOnObj())
                {
                    Teleport(rSensor);
                }
                else if(!lSensor.GetComponent<MagicianSensorScript>().GetOnObj())
                {
                    Teleport(lSensor);
                }
                else
                {
                    return;
                }
                break;

            case 2:
                if (!lSensor.GetComponent<MagicianSensorScript>().GetOnObj())
                {
                    Teleport(lSensor);
                }
                else if (!rSensor.GetComponent<MagicianSensorScript>().GetOnObj())
                {
                    Teleport(rSensor);
                }
                else
                {
                    return;
                }
                break;

            default:
                return;
        }
    }

    void Teleport( GameObject obj)
    {
        gameObject.transform.position = obj.transform.position;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<Animator>().SetTrigger("bron");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet" || collision.tag == "VehicleBullet")
        {
            Hurt(collision.GetComponent<PlayerBulletAdstract>().GetBulletPower());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Map" || collision.tag == "VehicleBullet")
        {
            dead();
        }
    }


    IEnumerator DethTime()
    {
        yield return new WaitForSeconds(0.7f);
        Destroy(gameObject);
    }

    IEnumerator AttackTime()
    {

        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(1.1f);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

    }

    IEnumerator TeleportTime()
    {
        
        doTeleport = false;
        yield return new WaitForSeconds(teleportCoolTime);

        TeleportMain();

        yield return new WaitForSeconds(0.5f);

        Attack();
 
        doTeleport = true;
    }

    public override int GetEnemyPower()
    {
        return magicianPower;
    }
}
