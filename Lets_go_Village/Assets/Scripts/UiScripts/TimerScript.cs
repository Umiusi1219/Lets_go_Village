using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    [SerializeField] float m_Timelimit;

    float m_TimelimitMAX;

    [SerializeField] GameObject player;

    void Start()
    {
        m_TimelimitMAX = m_Timelimit;
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        m_Timelimit -= Time.deltaTime;


        if (0 < m_Timelimit)
        {
            gameObject.GetComponent<Text>().text = m_Timelimit.ToString("F1");
        }
        else
        {
            gameObject.GetComponent<Text>().text = "Time Over";

            player.GetComponent<PlayerController>().Die();
        }
    }

    void TimeReset()
    {
        m_Timelimit = m_TimelimitMAX;
    }

    void TimeRecovery(float recoveryNum )
    {
        m_Timelimit += recoveryNum;
    }
}
