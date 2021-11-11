using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonScript : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float skeletonMovePower;

    [SerializeField]
    float toPlayerDistance;

    private int skeletonDirection = 1;

    bool skeletonDed = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("WizardVariant");
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(!skeletonDed)
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
            skeletonDirection = 1;
            moveVelocity = Vector3.left;

            transform.localScale = new Vector3(skeletonDirection, 1, 1);

        }
        if (0 < toPlayerDistance)
        {
            skeletonDirection = -1;
            moveVelocity = Vector3.right;

            transform.localScale = new Vector3(skeletonDirection, 1, 1);
        }
        transform.position += moveVelocity * skeletonMovePower * Time.deltaTime;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerBullet")
        {
            skeletonDed = true;

            gameObject.GetComponent<Animator>().SetBool("Ded", true);
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
