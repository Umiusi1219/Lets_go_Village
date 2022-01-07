using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndeadScript : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float undeadMovePower;

    [SerializeField]
    float toPlayerDistance;

    private int undeadDirection = 1;

    bool undeadDed = false;

    [SerializeField] Vector3 bronPosAdd;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position += bronPosAdd;

        player = GameObject.Find("WizardVariant");
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(!undeadDed)
        {
            toPlayerDistance = player.transform.position.x - gameObject.transform.position.x;
            Run();
        }
    }

    void Run()
    {

        Vector3 moveVelocity = Vector3.zero;

        if (toPlayerDistance < 0)
        {
            undeadDirection = -1;
            moveVelocity = Vector3.left;

            transform.localScale = new Vector3(undeadDirection, 1, 1);

        }
        if (0 < toPlayerDistance)
        {
            undeadDirection = 1;
            moveVelocity = Vector3.right;

            transform.localScale = new Vector3(undeadDirection, 1, 1);
        }
        transform.position += moveVelocity * undeadMovePower * Time.deltaTime;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerBullet" || collision.tag == "VehicleBullet")
        {
            undeadDed = true;

            gameObject.GetComponent<Animator>().SetBool("ded", true);
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            StartCoroutine(DetTime());
        }
    }

    IEnumerator DetTime()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject.transform.parent.gameObject);
    }
}
