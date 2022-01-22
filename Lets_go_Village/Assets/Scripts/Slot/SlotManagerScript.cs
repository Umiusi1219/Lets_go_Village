using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManagerScript : MonoBehaviour
{
    [SerializeField] GameObject slotR;
    [SerializeField] GameObject slotS;
    [SerializeField] GameObject slotL;

    [SerializeField] public bool m_stopFirstSlot;
    [SerializeField] public int m_slotNum;

    // Start is called before the first frame update
    void Start()
    {
        m_stopFirstSlot = false;
        m_slotNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
