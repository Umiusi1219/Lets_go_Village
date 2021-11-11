using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEgg : MonoBehaviour
{
    [SerializeField] private float generationCoolTime;

    [SerializeField] private bool generationPossible = true;

    [SerializeField] GameObject generationMonster;

    private void OnTriggerEnter2D(Collider2D collision)
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
