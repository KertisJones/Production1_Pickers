﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideInput : MonoBehaviour {

    public bool hideOnDealOver = false;
    public bool hideOnFirstOffer = false;
    public bool hideDuringDeal = false;
    public bool hideAfterExpert = false;
    public bool hideAfterFinalOffer = false;

    private Vector2 origPos = new Vector2();

    public GameObject dealMaster;

    // Use this for initialization
    void Start () {
        origPos = this.GetComponent<RectTransform>().position;
	}
	
	// Update is called once per frame
	void Update () {
		if((hideOnDealOver && dealMaster.GetComponent<DealMaster>().dealOver) 
            || (hideOnFirstOffer && dealMaster.GetComponent<DealMaster>().antique.playerCounterOfferPrevious == 0) 
            || (hideDuringDeal && !dealMaster.GetComponent<DealMaster>().dealOver && dealMaster.GetComponent<DealMaster>().antique.playerCounterOfferPrevious != 0) 
            || (hideAfterExpert && !dealMaster.GetComponent<DealMaster>().antique.hideValue)
            || (hideAfterFinalOffer && dealMaster.GetComponent<DealMaster>().antique.gaveFinal))
        {
            hide();
        }
        else
        {
            stopHide();
        }
	}

    void hide()
    {
        this.transform.position = new Vector2(this.transform.position.x + 9999, this.transform.position.y + 9999);
    }

    void stopHide()
    {
        this.GetComponent<RectTransform>().position = origPos;
    }
}
