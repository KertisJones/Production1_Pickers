using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DealMaster : MonoBehaviour {

	public int itemBaseValue = 50;
    private int itemSentimentalValue = 0;
    public bool dealOver = false;
    public bool dealMade = false;
    public bool hideValue = true;

    public int playerCounterOfferPrevious = 0;

    public int sellerCurrentPrice = 100;
    public int sellerMinPrice = 0;
    public int sellerMinPriceThreshold = 5;
    public int sellerCurrentPriceThreshold = 5;

	public int sellerAnger = 0;

	private GameManager gm;
    public GameObject counterOfferButton;
    public GameObject dealButton;
    public GameObject noDealButton;
    public GameObject restartButton;
    public GameObject counterOfferInput;
    public GameObject finalButton;

    // Use this for initialization
    void Start () {
		gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();

        counterOfferButton.GetComponent<Button>().onClick.AddListener(delegate { counterClick(); });
        dealButton.GetComponent<Button>().onClick.AddListener(delegate { sellItem(); });
        restartButton.GetComponent<Button>().onClick.AddListener(delegate { restart(); });
        noDealButton.GetComponent<Button>().onClick.AddListener(delegate { noDeal(); });
        finalButton.GetComponent<Button>().onClick.AddListener(delegate { finalOffer(); });

        itemBaseValue = Mathf.RoundToInt(Random.Range(50f, 10000f));

        itemSentimentalValue = Mathf.RoundToInt(itemBaseValue + Random.Range(0.8f, 1.2f) * RandomSign());

        sellerCurrentPrice = roundTo5(Mathf.RoundToInt(itemSentimentalValue * Random.Range(1.2f, 1.6f)));
        sellerMinPrice = Mathf.RoundToInt(itemSentimentalValue * Random.Range(0.2f, 0.6f));

        sellerMinPriceThreshold = Mathf.RoundToInt(sellerMinPrice * Random.Range(0.075f, 0.2f));
        sellerCurrentPriceThreshold = Mathf.RoundToInt(sellerCurrentPrice * Random.Range(0.075f, 0.2f));
    }

    int RandomSign()
    {
        int randomSign = Random.Range(0, 1);
        if (randomSign == 0)
        {
            randomSign = -1;
        }
        return randomSign;
    }

    int roundTo5(int n)
    {
        return Mathf.RoundToInt(n / 5.0f) * 5;
    }
	
	// Update is called once per frame
	void Update () {
		if (sellerCurrentPrice < sellerMinPrice)
			sellerCurrentPrice = sellerMinPrice;
		if (Input.GetButtonDown ("Submit"))
			counterClick ();
	}

    void counterClick()
    {
		if (counterOfferInput.GetComponent<InputField> ().text != "") {
			makeOffer (System.Convert.ToInt32 (counterOfferInput.GetComponent<InputField> ().text));
			counterOfferInput.GetComponent<InputField> ().text = "";
			counterOfferInput.GetComponentInChildren<Text> ().text = "";
		}
    }

	void makeOffer(int counterOffer) {
		if (!dealOver) {
			if (counterOffer < playerCounterOfferPrevious) {
				//TODO: Offered less than before, GET ANGRY
				sellerAnger += 1;
                Debug.Log("Offered less than before, GET ANGRY");
			} 
			else if (counterOffer == playerCounterOfferPrevious) {
				//TODO: That's the same thing. Do nothing... maybe even get angry.
				sellerAnger += 1;
                Debug.Log("That's the same thing. Do nothing... maybe even get angry.");
            } 
			else if (counterOffer < (sellerMinPrice - sellerMinPriceThreshold)) {
				//TODO: Insultingly low offer, GET ANGRY
				sellerAnger += 1;
                Debug.Log("Insultingly low offer, GET ANGRY");
            } 
			else if ((counterOffer >= (sellerMinPrice - sellerMinPriceThreshold)) && (counterOffer < (sellerCurrentPrice - sellerCurrentPriceThreshold))) {
                //TODO: Normal offer, reduce asking price.
                //Find difference between offer and counteroffer
                int differenceInOffer = 0;
                
                if (playerCounterOfferPrevious == 0) //first offer
                {
                    if (counterOffer <= sellerMinPrice)
                    {
                        differenceInOffer = 0;
                    }
                    //else if (counterOffer < itemSentimentalValue)
                    //{

                    //}
                    else
                    {
                        differenceInOffer = (counterOffer - sellerMinPrice) / 2;
                    }
                }
                else //not first offer
                {
                    differenceInOffer = counterOffer - playerCounterOfferPrevious;
                }

                //reduce asking price.
                //int differenceBetweenPrice = sellerCurrentPrice - counterOffer;
                //if (differenceBetweenPrice >= differenceInOffer) //came halfway (or more)

                if (counterOffer > sellerMinPrice && playerCounterOfferPrevious >= (sellerMinPrice - sellerMinPriceThreshold))
                {
                    if (sellerCurrentPrice - differenceInOffer > counterOffer + sellerCurrentPriceThreshold)
                    {
                        sellerCurrentPrice = sellerCurrentPrice - differenceInOffer;
                    }
                    else
                    {
                        sellerCurrentPrice = counterOffer;
                        sellItem();
                        Debug.Log("That's a fair price. I'll take it.");
                    }
                }
                else if (counterOffer > sellerMinPrice && playerCounterOfferPrevious < (sellerMinPrice - sellerMinPriceThreshold) * 2)
                {
                    //sellerCurrentPrice = sellerCurrentPrice - (differenceInOffer / 100);
                    if (sellerCurrentPrice - (differenceInOffer / 100) > counterOffer + sellerCurrentPriceThreshold)
                    {
                        sellerCurrentPrice = sellerCurrentPrice - ((counterOffer - sellerMinPrice) / 2);
                        Debug.Log("Offer under over min price, but last offer was under threshold. Reduce price via min price,");
                    }
                    else
                    {
                        sellerCurrentPrice = counterOffer;
                        sellItem();
                        Debug.Log("That's a fair price. I'll take it.");
                    }
                }
                else
                {
                    if (sellerCurrentPrice - (differenceInOffer / 10) > counterOffer + sellerCurrentPriceThreshold)
                    {
                        sellerCurrentPrice = sellerCurrentPrice - (differenceInOffer / 10);
                        Debug.Log("Offer under Min price, reduce price less");
                    }
                    else
                    {
                        sellerCurrentPrice = counterOffer;
                        sellItem();
                        Debug.Log("That's a fair price. I'll take it.");
                    }
                }

                Debug.Log("Normal offer, reduce asking price.");
            } 
			else if ((counterOffer >= (sellerCurrentPrice - sellerCurrentPriceThreshold)) && (counterOffer < sellerCurrentPrice)) {
				//TODO: Very close to current offer, either accept or enter "Final Offer"
				sellerCurrentPrice = counterOffer;
				sellItem ();
                Debug.Log("Very close to current offer, either accept or enter Final Offer");
            } 
			else if (counterOffer == sellerCurrentPrice) {
				//TODO: That's what I just said. Deal, but you're kind of a dick.
				sellerCurrentPrice = counterOffer;
				sellItem ();
                Debug.Log("That's what I just said. Deal, but you're kind of a dick.");
            } 
			else if (counterOffer > sellerCurrentPrice) {
				//TODO: Offered more than asked, GET HAPPY
				sellerAnger -= 1;
				sellerCurrentPrice = counterOffer;
				sellItem ();
                Debug.Log("Offered more than asked, GET HAPPY");
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
        playerCounterOfferPrevious = sellerCurrentPrice;
		dealOver = true;
        dealMade = true;
        hideValue = false;
    }

    void noDeal()
    {
        Debug.Log("NO DEAL. You turned down an item worth $" + itemBaseValue + " for $" + sellerCurrentPrice);
        //playerCounterOfferPrevious = 0;
        dealOver = true;
        dealMade = false;
        hideValue = false;
    }

    void finalOffer()
    {
        if (playerCounterOfferPrevious >= itemBaseValue)
        {
            sellerCurrentPrice = playerCounterOfferPrevious;
            sellItem();
            Debug.Log("FINAL OFFER... Offer greater than value, deal.");
        }
        else
        {
            sellerAnger += 1;
            int randint = Random.Range(0, 75) + 10;
            double distanceFromVal = ((playerCounterOfferPrevious * 1.0) / itemBaseValue) * 100;

            if (randint <= distanceFromVal)
            {
                sellerCurrentPrice = playerCounterOfferPrevious;
                sellItem();
            }
            else
            {
                noDeal();
            }
            Debug.Log("FINAL OFFER... Random Num: " + randint + ", Percentage progress to item value: " + distanceFromVal + "%");
        }
    }

    void restart()
    {
        dealOver = false;
        dealMade = false;
        hideValue = true;
        playerCounterOfferPrevious = 0;
        sellerAnger = 0;

        itemBaseValue = Mathf.RoundToInt(Random.Range(50f, 10000f));

        itemSentimentalValue = Mathf.RoundToInt(itemBaseValue + Random.Range(0.8f, 1.2f) * RandomSign());

        sellerCurrentPrice = roundTo5(Mathf.RoundToInt(itemSentimentalValue * Random.Range(1.2f, 1.6f)));
        sellerMinPrice = Mathf.RoundToInt(itemSentimentalValue * Random.Range(0.2f, 0.6f));

        sellerMinPriceThreshold = Mathf.RoundToInt(sellerMinPrice * Random.Range(0.075f, 0.2f));
        sellerCurrentPriceThreshold = Mathf.RoundToInt(sellerMinPrice * Random.Range(0.075f, 0.2f));
    }
}
