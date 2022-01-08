using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatScript : EnemyAdstract
{

    [SerializeField] float batHp;

    [SerializeField] float shootCoolTime;

    [SerializeField] float doAttackRange;

    [SerializeField] int batPower;

    [SerializeField] bool batDed;

    [SerializeField] bool doShoot;

    [SerializeField] GameObject player;

    Rigidbody2D rbody2D;

    [SerializeField] Vector3 toPlayerDistance;

    // Start is called before the first frame update
    void Start()
    {
        doShoot = true;

        rbody2D = gameObject.GetComponent<Rigidbody2D>();

        player = GameObject.Find("WizardVariant");

        gameObject.transform.position += new Vector3(0, 2, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!batDed)
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

            if (0 >= batHp || 50 <= toPlayerDistance.x)
            {
                dead();
            }

            if (doAttackRange > Mathf.Abs(toPlayerDistance.x) && doShoot)
            {
                Attack();
            }
        }
    }

    void Hurt(float bulletPower)
    {
        gameObject.GetComponent<Animator>().SetTrigger("hurt");
        batHp -= bulletPower;
    }


    public void Attack()
    {
        gameObject.GetComponent<Animator>().SetTrigger("attack");
        StartCoroutine(AttackTime());
    }


    void dead()
    {
        batDed = true;
        gameObject.GetComponent<Animator>().SetTrigger("ded");
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        StartCoroutine(DethTime());
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet" || collision.tag == "VehicleBullet")
        {
            Hurt(collision.GetComponent<PlayerBulletAdstract>().GetBulletPower());
        }
    }

    IEnumerator DethTime()
    {
        yield return new WaitForSeconds(0.7f);
        Destroy(gameObject);
    }

    IEnumerator AttackTime()
    {
        doShoot = false;
        yield return new WaitForSeconds(0.15f);
        rbody2D.AddForce(new Vector3(toPlayerDistance.x *3, toPlayerDistance.y * 1.5f
            , 0 ),ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.4f);
        rbody2D.velocity = Vector3.zero;

        yield return new WaitForSeconds(shootCoolTime);
        doShoot = true;
    }

    public override int GetEnemyPower()
    {
        return batPower;
    }
}
