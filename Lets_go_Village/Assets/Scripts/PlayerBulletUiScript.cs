using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletUiScript : MonoBehaviour
{
    [SerializeField]
    List<GameObject> playerBulletUi;

    private void Start()
    {
        Instantiate(playerBulletUi[0]).transform.SetParent(gameObject.transform);
    }

    public void ChangePlayerBulletUi(int num)
    {
        Destroy(transform.GetChild(0).gameObject);
        Instantiate(playerBulletUi[num]).transform.SetParent(gameObject.transform);
    }

}
