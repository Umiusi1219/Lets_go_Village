using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemScript : EnemyAdstract
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float golemMovePower;

    [SerializeField]
    private float golemHp;

    [SerializeField]
    private float doAttackRange;

    [SerializeField] bool doAttack;

    [SerializeField]
    private int golemPower;

    [SerializeField]
    float toPlayerDistance;

    public float attackCoolTime;

    private int golemDirection = 1;

    [SerializeField]
    bool golemDed = false;

    [SerializeField] Vector3 bronPosAdd;

    [SerializeField] GameObject Golem;

    // Start is called before the first frame update
    void Start()
    {
        doAttack = true;

        Golem = transform.root.gameObject;

        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        gameObject.transform.position += bronPosAdd;

        player = GameObject.Find("WizardVariant");
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!golemDed)
        {
            toPlayerDistance = player.transform.position.x - Golem.transform.position.x;
            Run();

            if (0 >= golemHp || 50 <= toPlayerDistance)
            {
                dead();
            }

            if (doAttackRange < Mathf.Abs(toPlayerDistance) && doAttack)
            {
                StartCoroutine(AttackTime());
            }
        }
    }

    void Run()
    {

        Vector3 moveVelocity = Vector3.zero;

        if (toPlayerDistance < 0)
        {
            golemDirection = 1;
            moveVelocity = Vector3.left;

            Golem.transform.rotation = new Quaternion(0, 0, 0, 0);

        }
        if (0 < toPlayerDistance)
        {
            golemDirection = -1;
            moveVelocity = Vector3.right;

            Golem.transform.rotation = new Quaternion(0, 180, 0,0);
        }
        Golem.transform.position += moveVelocity * golemMovePower * Time.deltaTime;

    }

    void Hurt(float bulletPower)
    {
        gameObject.GetComponent<Animator>().SetTrigger("hurt");
        golemHp -= bulletPower;
    }


    public void Attack()
    {
        gameObject.GetComponent<Animator>().SetTrigger("attack");
        StartCoroutine(AttackTime());
    }


    void dead()
    {
        golemDed = true;
        gameObject.GetComponent<Animator>().SetTrigger("death");
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
        Destroy(gameObject.transform.parent.gameObject);
    }

    IEnumerator AttackTime()
    {
        doAttack = false;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        yield return new WaitForSeconds(attackCoolTime);
        doAttack = true;
    }

    public override int GetEnemyPower()
    {
        return golemPower;
    }
}

