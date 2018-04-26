using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DealMaster : MonoBehaviour {

	//public int itemBaseValue = 50;
    //private int itemSentimentalValue = 0;
    public bool dealOver = false;
    public bool dealMade = false;
    public bool notEnoughMoney = false;
    //public bool hideValue = true;
    

    //public int playerCounterOfferPrevious = 0;

    //public int sellerCurrentPrice = 100;
    //public int sellerMinPrice = 0;
    //public int sellerMinPriceThreshold = 5;
    //public int sellerCurrentPriceThreshold = 5;

	public int sellerAnger = 0;

	private GameManager gm;
    public GameObject counterOfferButton;
    public GameObject dealButton;
    public GameObject noDealButton;
    public GameObject restartButton;
    public GameObject counterOfferInput;
    public GameObject finalButton;
    public GameObject expertButton;
    public AudioClip dealClip;

    public GameObject currentItem;
    public Antique antique;


    // Use this for initialization
    void Start () {
		gm = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();

        currentItem = gm.currentItem;
        antique = currentItem.GetComponent<Antique>();

        counterOfferButton.GetComponent<Button>().onClick.AddListener(delegate { counterClick(); });
        dealButton.GetComponent<Button>().onClick.AddListener(delegate { sellItem(); });
        restartButton.GetComponent<Button>().onClick.AddListener(delegate { restart(); });
        noDealButton.GetComponent<Button>().onClick.AddListener(delegate { noDeal(); });
        finalButton.GetComponent<Button>().onClick.AddListener(delegate { finalOffer(); });
        expertButton.GetComponent<Button>().onClick.AddListener(delegate { callExpert(); });

        //itemBaseValue = antique.itemValue;

        //itemSentimentalValue = Mathf.RoundToInt(itemBaseValue + Random.Range(0.8f, 1.2f) * RandomSign());

        //sellerCurrentPrice = roundTo5(Mathf.RoundToInt(itemSentimentalValue * Random.Range(1.2f, 1.6f)));
        //sellerMinPrice = Mathf.RoundToInt(itemSentimentalValue * Random.Range(0.2f, 0.6f));

        //sellerMinPriceThreshold = Mathf.RoundToInt(sellerMinPrice * Random.Range(0.075f, 0.2f));
        //sellerCurrentPriceThreshold = Mathf.RoundToInt(sellerCurrentPrice * Random.Range(0.075f, 0.2f));
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
        /*if (currentItem.GetComponent<ClickedOnAntique>() != null)
        {
            itemBaseValue = currentItem.GetComponent<ClickedOnAntique>().itemValue;
        }
        else
        {
            itemBaseValue = 500;
        }*/

        if (antique.sellerCurrentPrice < antique.sellerMinPrice)
            antique.sellerCurrentPrice = antique.sellerMinPrice;
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
			if (counterOffer < antique.playerCounterOfferPrevious) {
				//TODO: Offered less than before, GET ANGRY
				sellerAnger += 1;
                Debug.Log("Offered less than before, GET ANGRY");
			} 
			else if (counterOffer == antique.playerCounterOfferPrevious) {
				//TODO: That's the same thing. Do nothing... maybe even get angry.
				sellerAnger += 1;
                Debug.Log("That's the same thing. Do nothing... maybe even get angry.");
            } 
			else if (counterOffer < (antique.sellerMinPrice - antique.sellerMinPriceThreshold)) {
				//TODO: Insultingly low offer, GET ANGRY
				sellerAnger += 1;
                Debug.Log("Insultingly low offer, GET ANGRY");
            }
            else if (counterOffer > antique.sellerCurrentPrice)
            {
                //TODO: Offered more than asked, GET HAPPY
                sellerAnger -= 1;
                antique.sellerCurrentPrice = counterOffer;
                sellItem();
                Debug.Log("Offered more than asked, GET HAPPY");
            }
            else if ((counterOffer >= (antique.sellerMinPrice - antique.sellerMinPriceThreshold)) && (counterOffer < (antique.sellerCurrentPrice - antique.sellerCurrentPriceThreshold))) {
                //TODO: Normal offer, reduce asking price.
                //Find difference between offer and counteroffer
                int differenceInOffer = 0;
                
                if (antique.playerCounterOfferPrevious == 0) //first offer
                {
                    if (counterOffer <= antique.sellerMinPrice)
                    {
                        differenceInOffer = 0;
                    }
                    //else if (counterOffer < itemSentimentalValue)
                    //{

                    //}
                    else
                    {
                        differenceInOffer = (counterOffer - antique.sellerMinPrice) / 2;
                    }
                }
                else //not first offer
                {
                    differenceInOffer = counterOffer - antique.playerCounterOfferPrevious;
                }

                //reduce asking price.
                //int differenceBetweenPrice = sellerCurrentPrice - counterOffer;
                //if (differenceBetweenPrice >= differenceInOffer) //came halfway (or more)

                if (counterOffer > antique.sellerMinPrice && antique.playerCounterOfferPrevious >= (antique.sellerMinPrice - antique.sellerMinPriceThreshold))
                {
                    if (antique.sellerCurrentPrice - differenceInOffer > counterOffer + antique.sellerCurrentPriceThreshold)
                    {
                        antique.sellerCurrentPrice = antique.sellerCurrentPrice - differenceInOffer;
                    }
                    else
                    {
                        antique.sellerCurrentPrice = counterOffer;
                        sellItem();
                        Debug.Log("That's a fair price. I'll take it.");
                    }
                }
                else if (counterOffer > antique.sellerMinPrice && antique.playerCounterOfferPrevious < (antique.sellerMinPrice - antique.sellerMinPriceThreshold) * 2)
                {
                    //sellerCurrentPrice = sellerCurrentPrice - (differenceInOffer / 100);
                    if (antique.sellerCurrentPrice - (differenceInOffer / 100) > counterOffer + antique.sellerCurrentPriceThreshold)
                    {
                        antique.sellerCurrentPrice = antique.sellerCurrentPrice - ((counterOffer - antique.sellerMinPrice) / 2);
                        Debug.Log("Offer under over min price, but last offer was under threshold. Reduce price via min price,");
                    }
                    else
                    {
                        antique.sellerCurrentPrice = counterOffer;
                        sellItem();
                        Debug.Log("That's a fair price. I'll take it.");
                    }
                }
                else
                {
                    if (antique.sellerCurrentPrice - (differenceInOffer / 10) > counterOffer + antique.sellerCurrentPriceThreshold)
                    {
                        antique.sellerCurrentPrice = antique.sellerCurrentPrice - (differenceInOffer / 10);
                        Debug.Log("Offer under Min price, reduce price less");
                    }
                    else
                    {
                        antique.sellerCurrentPrice = counterOffer;
                        sellItem();
                        Debug.Log("That's a fair price. I'll take it.");
                    }
                }


                if (counterOffer > antique.sellerCurrentPrice)
                {
                    //Bugcatcher!
                    //TODO: Offered more than asked, GET HAPPY 
                    sellerAnger -= 1;
                    antique.sellerCurrentPrice = counterOffer;
                    sellItem();
                    Debug.Log("Offered more than asked, GET HAPPY");
                }
                Debug.Log("Normal offer, reduce asking price.");
            } 
			else if ((counterOffer >= (antique.sellerCurrentPrice - antique.sellerCurrentPriceThreshold)) && (counterOffer < antique.sellerCurrentPrice)) {
                //TODO: Very close to current offer, either accept or enter "Final Offer"
                antique.sellerCurrentPrice = counterOffer;
				sellItem ();
                Debug.Log("Very close to current offer, either accept or enter Final Offer");
            } 
			else if (counterOffer == antique.sellerCurrentPrice) {
                //TODO: That's what I just said. Deal, but you're kind of a dick.
                antique.sellerCurrentPrice = counterOffer;
				sellItem ();
                Debug.Log("That's what I just said. Deal, but you're kind of a dick.");
            } 			
			else {
				Debug.Log ("You offered something really weird... THIS SHOULDN'T BE HAPPENING!");
			}

			if (antique.playerCounterOfferPrevious < counterOffer)
                antique.playerCounterOfferPrevious = counterOffer;
		}
	}

	void sellItem () {
        antique.playerCounterOfferPrevious = antique.sellerCurrentPrice;
        if (gm.playerMoney >= antique.playerCounterOfferPrevious)
        {
            Destroy(currentItem);
            Debug.Log("YOU WIN! You have purchased an item worth $" + antique.itemBaseValue + " for $" + antique.sellerCurrentPrice);
            gm.playerMoney -= antique.playerCounterOfferPrevious;
            gm.playerMoney += antique.itemBaseValue;
            //gm.playerItemValue += itemBaseValue;

            dealOver = true;
            dealMade = true;
            antique.hideValue = false;

            AudioSource.PlayClipAtPoint(dealClip, new Vector3(0, 0, -10));
        }
        else
        {
            Debug.Log("You don't have the money, No Deal!");
            notEnoughMoney = true;
            noDeal();
        }
    }

    void noDeal()
    {
        //Destroy(currentItem);
        Debug.Log("NO DEAL. You turned down an item worth $" + antique.itemBaseValue + " for $" + antique.sellerCurrentPrice);
        //playerCounterOfferPrevious = 0;
        dealOver = true;
        dealMade = false;
        //antique.hideValue = false;
    }

    void callExpert()
    {
        if (gm.playerMoney >= 1000)
        {
            Debug.Log("Calling an expert... the current item is worth $" + antique.itemBaseValue);
            gm.playerMoney -= 1000;
            antique.hideValue = false;

            AudioSource.PlayClipAtPoint(dealClip, new Vector3(0, 0, -10));
        }
        else
        {
            Debug.Log("You don't have the money for an expert!");
        }
    }

    void finalOffer()
    {
        antique.gaveFinal = true;
        if (antique.playerCounterOfferPrevious < antique.sellerMinPrice)
        {
            noDeal();
            Debug.Log("FINAL OFFER... Less than min price, no deal");
        }
        else
        {
            sellerAnger += 1;
            int randint = Random.Range(0, 70) + 10;
            double distanceFromVal = ((antique.playerCounterOfferPrevious * 1.0) / antique.itemBaseValue) * 100;

            if (randint <= distanceFromVal)
            {
                antique.sellerCurrentPrice = antique.playerCounterOfferPrevious;
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
        if (currentItem != null)
        {
            currentItem.GetComponent<Antique>().clicked = false;
        }
        gm.endNegotiation();
        /*
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
        sellerCurrentPriceThreshold = Mathf.RoundToInt(sellerMinPrice * Random.Range(0.075f, 0.2f));*/
    }
}
