using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealMaster : MonoBehaviour {

	public int itemBaseValue = 50;
	public bool dealOver = false;

	public int playerCounterOfferPrevious = 0;

	public int sellerCurrentPrice = 100;
	public int sellerMinPrice = 0;
	public int sellerMinPriceThreshold = 5;
	public int sellerCurrentPriceThreshold = 5;

	public int sellerAnger = 0;

	private GameManager gm;

	// Use this for initialization
	void Start () {
		gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (sellerCurrentPrice > sellerMinPrice)
			sellerCurrentPrice = sellerMinPrice;
	}

	void makeOffer(int counterOffer) {
		if (!dealOver) {
			if (counterOffer < playerCounterOfferPrevious) {
				//TODO: Offered less than before, GET ANGRY
				sellerAnger += 1;
			} 
			else if (counterOffer == playerCounterOfferPrevious) {
				//TODO: That's the same thing. Do nothing... maybe even get angry.
				sellerAnger += 1;
			} 
			else if (counterOffer < (sellerMinPrice - sellerMinPriceThreshold)) {
				//TODO: Insultingly low offer, GET ANGRY
				sellerAnger += 1;
			} 
			else if ((counterOffer >= (sellerMinPrice - sellerMinPriceThreshold)) && (counterOffer < (sellerCurrentPrice - sellerCurrentPriceThreshold))) {
				//TODO: Normal offer, reduce asking price.
				sellerCurrentPrice -= 1;
			} 
			else if ((counterOffer >= (sellerCurrentPrice - sellerCurrentPriceThreshold)) && (counterOffer < sellerCurrentPrice)) {
				//TODO: Very close to current offer, either accept or enter "Final Offer"
				sellerCurrentPrice = counterOffer;
				sellItem ();
			} 
			else if (counterOffer == sellerCurrentPrice) {
				//TODO: That's what I just said. Deal, but you're kind of a dick.
				sellerCurrentPrice = counterOffer;
				sellItem ();

			} 
			else if (counterOffer > sellerCurrentPrice) {
				//TODO: Offered more than asked, GET HAPPY
				sellerAnger -= 1;
				sellerCurrentPrice = counterOffer;
				sellItem ();
			} 
			else {
				Debug.Log ("You offered something really weird... THIS SHOULDN'T BE HAPPENING!");
			}

			if (playerCounterOfferPrevious < counterOffer)
				playerCounterOfferPrevious = counterOffer;
		}
	}

	void sellItem () {
		Debug.Log ("YOU WIN! You have purchased an item worth $" + itemBaseValue + " for $" + sellerCurrentPrice);
		dealOver = true;
	} 
}
