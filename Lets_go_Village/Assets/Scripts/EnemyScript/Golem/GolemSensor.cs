using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemSensor : MonoBehaviour
{
    [SerializeField] GameObject golem;

    [SerializeField] bool doAttack = true;

    // Start is called before the first frame update
    void Start()
    {
        doAttack = true;
        golem = transform.parent.gameObject;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if(doAttack)
            {
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        doAttack = false;
        golem.GetComponent<GolemScript>().Attack();
        yield return new WaitForSeconds(golem.GetComponent<GolemScript>().attackCoolTime);
        doAttack = true;
    }
}
