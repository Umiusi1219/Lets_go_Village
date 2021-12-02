using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPScript : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject playerHpUi;


    private void Start()
    {
        for (int i = 0; i < player.GetComponent<PlayerController>().m_playerHPMAX; i++)
        {
            Instantiate(playerHpUi).transform.SetParent(gameObject.transform);
        }
    }

    public void ChangePlayerHpUi(int max ,int hp)
    {
        for (int i = 0; i < max; i++) 
        {
            if(i < hp)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
