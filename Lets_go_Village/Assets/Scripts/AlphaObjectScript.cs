using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaObjectScript : MonoBehaviour
{
    [SerializeField] private bool doAlpha = false;

    [SerializeField] private float alphaAddNum;

    [SerializeField] private float showTime;

    void Start()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
    }

    private void FixedUpdate()
    {
        StartCoroutine(AlphaChange());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            doAlpha = true;
        }
    }

    IEnumerator AlphaChange()
    {
        
        if (doAlpha)
        {
            if (this.GetComponent<SpriteRenderer>().color != new Color(1f, 1f, 1f, 1f))
            {
                this.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, alphaAddNum);
            }
            else if (this.GetComponent<SpriteRenderer>().color == new Color(1f, 1f, 1f, 1f))
            {
                yield return new WaitForSeconds(showTime);

                doAlpha = false;

            }
        }
        else if (!doAlpha)
        {
            if (this.GetComponent<SpriteRenderer>().color != new Color(1f, 1f, 1f, 0f))
            {
                this.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, alphaAddNum);
            }
        }
    }
}
