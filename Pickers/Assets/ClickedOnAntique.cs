using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickedOnAntique : MonoBehaviour {
    private DealMaster dealMaster;
    private GameManager GM;
    public bool clicked = false;
    public int itemValue = 500;

    // Use this for initialization
    void Start()
    {
        itemValue = Mathf.RoundToInt(Random.Range(50f, 10000f));
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
            clicked = true;
            //Destroy(this.gameObject);
            GM.currentItem = this.gameObject;
            GM.triggerNegotiation = true;
        }
    }
}
