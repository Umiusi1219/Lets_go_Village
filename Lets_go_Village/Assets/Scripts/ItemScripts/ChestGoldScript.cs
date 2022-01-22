using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestGoldScript : MonoBehaviour
{
    [SerializeField]
    GameObject Iteme;

    [SerializeField]
    List<GameObject> ItemeList;

    [SerializeField] int randNum;

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
        if (collision.tag == "PlayerBullet" || collision.tag == "VehicleBullet")
        {
            chestAnim.SetBool("Open", true);
            chestDollider2D.enabled = false;

            if (Iteme != null)
            {
                StartCoroutine(ItemGenerationTime(Iteme));
            }
            else if(ItemeList[0] != null)
            {
                randNum = Random.Range(0, ItemeList.Count);
                StartCoroutine(ItemGenerationTime(ItemeList[randNum]));
            }
            else
            {
                return;
            }
        }
    }


    IEnumerator ItemGenerationTime(GameObject GeneratObj)
    {
        yield return new WaitForSeconds(generationTime);
        Instantiate(GeneratObj).transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y+1,0) ;
    }
}
