using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEgg : MonoBehaviour
{
    [SerializeField] private float generationCoolTime;

    [SerializeField] private bool generationPossible = true;

    [SerializeField] GameObject generationMonster;

    [SerializeField] GameObject enemysParent;

   

    // Start is called before the first frame update
    void Start()
    {
        enemysParent = GameObject.Find("Enemys");

    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (generationPossible && collision.tag == "Player")
        {
            Instantiate(generationMonster).transform.position = gameObject.transform.position;

            generationPossible = false;

            StartCoroutine(MonsterGenerationCoolTime());
        }
    }


    IEnumerator MonsterGenerationCoolTime()
    {
        yield return new WaitForSeconds(generationCoolTime);

        generationPossible = true;
    }
}
