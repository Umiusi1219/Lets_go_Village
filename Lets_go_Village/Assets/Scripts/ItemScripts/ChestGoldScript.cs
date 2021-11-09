using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestGoldScript : MonoBehaviour
{
    [SerializeField]
    GameObject Iteme;

    private Animator chestAnim;

    private BoxCollider2D chestDollider2D;

    [SerializeField] float generationTime;

    private void Start()
    {
        chestAnim = GetComponent<Animator>();
        chestDollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            chestAnim.SetBool("Open", true);
            chestDollider2D.enabled = false;

            StartCoroutine(ItemGenerationTime());
        }
    }

    IEnumerator ItemGenerationTime()
    {
        yield return new WaitForSeconds(generationTime);
        Instantiate(Iteme).transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y+1,0) ;
    }
}
