using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickedOnAntique : MonoBehaviour {
    private DealMaster dealMaster;
    private GameManager GM;

    // Use this for initialization
    void Start()
    {
        //dealMaster = GameObject.FindGameObjectWithTag("DealMaster").GetComponent<DealMaster>();
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dealMaster == null && GameObject.FindGameObjectWithTag("DealMaster") != null)
            dealMaster = GameObject.FindGameObjectWithTag("DealMaster").GetComponent<DealMaster>();
        if (GM == null)
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

        void OnMouseDown()
    {
        // this object was clicked - do something

        if (dealMaster == null)
        {
            Destroy(this.gameObject);
            GM.triggerNegotiation = true;
        }
    }
}
