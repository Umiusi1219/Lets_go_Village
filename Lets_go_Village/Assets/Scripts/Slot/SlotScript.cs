using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotScript : MonoBehaviour
{
    [SerializeField] GameObject slotManager;

    [SerializeField] playerBulletType hitBulletType;

    [SerializeField] int slotNum;

    [SerializeField] int slotNumMax;

    [SerializeField] bool hitBullet;

    [SerializeField] bool doSlot;

    [SerializeField] float rotationTime;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        slotNum = 1;
        doSlot = true;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!slotManager.GetComponent<SlotManagerScript>().m_stopFirstSlot)
        {
            if (!hitBullet)
            {

                if (doSlot)
                {
                    StartCoroutine(SlotTime());
                }
            }
            else
            {
                slotManager.GetComponent<SlotManagerScript>().m_slotNum = slotNum;

                slotManager.GetComponent<SlotManagerScript>().m_stopFirstSlot = true;
            }
        }
        else
        {
            if (!hitBullet)
            {

                if (doSlot)
                {
                    StartCoroutine(SlotTime());
                }
            }
            else if (slotNum != slotManager.GetComponent<SlotManagerScript>().m_slotNum)
            {
                if (doSlot)
                {
                    StartCoroutine(SlotTime());
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet" || collision.tag == "VehicleBullet")
        {
            if (collision.GetComponent<PlayerBulletAdstract>().GetBulletType()
            == hitBulletType.ToString())
            {
                hitBullet = true;

            }
        }
    }

    IEnumerator SlotTime()
    {
        doSlot = false;

        yield return new WaitForSeconds(rotationTime);
        slotNum++;
        
        if(slotNumMax < slotNum )
        {
            slotNum = 1;
        }

        anim.SetTrigger(slotNum.ToString());
        doSlot = true;
    }
}
