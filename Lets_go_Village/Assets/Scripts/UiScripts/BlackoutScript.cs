using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackoutScript : MonoBehaviour
{
    [SerializeField] private bool doShow = false;

    [SerializeField] private float showAddNum;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Image>().color = new Color (0, 0, 0, 0f);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (doShow)
        {
            if (this.GetComponent<Image>().color != new Color(0, 0, 0, 1f))
            {
                this.GetComponent<Image>().color += new Color(0, 0, 0, showAddNum);
            }
        }
    }

    public void Onbool_doShow()
    {
        doShow = true;
    }
}
