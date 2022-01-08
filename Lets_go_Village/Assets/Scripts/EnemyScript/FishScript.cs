using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishScript : EnemyAdstract
{

    [SerializeField] float fishHp;

    [SerializeField] float attackCoolTime;

    [SerializeField] float doAttackRange;

    [SerializeField] int fishPower;

    [SerializeField] bool fishDed;

    [SerializeField] bool doattack;

    [SerializeField] GameObject player;

    Rigidbody2D rbody2D;

    [SerializeField] Vector3 toPlayerDistance;

    // Start is called before the first frame update
    void Start()
    {
        doattack = true;

        rbody2D = gameObject.GetComponent<Rigidbody2D>();

        player = GameObject.Find("WizardVariant");

        gameObject.transform.position += new Vector3(0, 2, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!fishDed)
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

            if (0 >= fishHp || 50 <= toPlayerDistance.x)
            {
                dead();
            }

            if (doAttackRange > Mathf.Abs(toPlayerDistance.x) && doattack)
            {
                StartCoroutine(AttackTime());
            }
        }
    }

    void Hurt(float bulletPower)
    {
        fishHp -= bulletPower;
        StartCoroutine(Hurt());
    }


    void dead()
    {
        fishDed = true;
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Map" || collision.tag == "VehicleBullet")
        {
            dead();
        }
    }

    IEnumerator DethTime()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 1f);
        yield return new WaitForSeconds(0.1f);
        this.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0f, 0f, 0.8f);
        yield return new WaitForSeconds(0.1f);
        this.GetComponent<SpriteRenderer>().color = new Color(0.6f, 0f, 0f, 0.6f);
        yield return new WaitForSeconds(0.1f);
        this.GetComponent<SpriteRenderer>().color = new Color(0.4f, 0f, 0f, 0.4f);
        yield return new WaitForSeconds(0.1f);
        this.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0f, 0f, 0.2f);
        yield return new WaitForSeconds(0.1f);
        this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        Destroy(gameObject);
    }

    IEnumerator Hurt()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 0f);
        yield return new WaitForSeconds(0.1f);
        this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        yield return new WaitForSeconds(0.1f);
        this.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 0f);
        yield return new WaitForSeconds(0.1f);
        this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator AttackTime()
    {
        doattack = false;
        gameObject.GetComponent<Animator>().SetFloat("animSpeed", 1.8f);
        yield return new WaitForSeconds(0.1f);
        rbody2D.AddForce(new Vector3(toPlayerDistance.x * 3, toPlayerDistance.y * 1.5f
            , 0), ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.4f);
        rbody2D.velocity = Vector3.zero;
        gameObject.GetComponent<Animator>().SetFloat("animSpeed", 1);

        yield return new WaitForSeconds(attackCoolTime);
        doattack = true;
    }

    public override int GetEnemyPower()
    {
        return fishPower;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "water" || collision.tag == "AlphaMap"
             || collision.tag == "Chest")
        {
            dead();
        }
    }
}

