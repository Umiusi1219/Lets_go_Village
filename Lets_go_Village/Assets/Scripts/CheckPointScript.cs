using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    [SerializeField] int checkPointNum;

    [SerializeField] public static int m_nowCheckpoint;

    [SerializeField] private bool isFirst;

    private void Start()
    {
        isFirst = true;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isFirst && collision.tag == "Player")
        {
            m_nowCheckpoint = checkPointNum;

            isFirst = false;
        }
    }
}

