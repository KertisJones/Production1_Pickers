using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayStat : MonoBehaviour {

    private Text txt;
    private DealMaster dealMaster;

    public enum StatType { currentPrice, itemBaseValue, previousOffer, playerMoney };
    public StatType statType;
    // Use this for initialization
    void Start()
    {
        txt = GetComponent<Text>();
		//dealMaster = GameObject.FindGameObjectWithTag("DealMaster").GetComponent<DealMaster>();
    }

    // Update is called once per frame
    void Update()
    {
		if (dealMaster == null && GameObject.FindGameObjectWithTag("DealMaster") != null)
			dealMaster = GameObject.FindGameObjectWithTag("DealMaster").GetComponent<DealMaster>();
        if (dealMaster != null)
        {
            if (statType == StatType.currentPrice)
            {
                if (dealMaster.playerCounterOfferPrevious == 0)
                {
                    txt.text = "";
                }
                else if (dealMaster.dealMade)
                {
                    txt.text = "Deal!";
                }
                else if (dealMaster.dealOver && !dealMaster.dealMade)
                {
                    txt.text = "";
                }
                else
                {
                    txt.text = "Asking Price: $" + dealMaster.sellerCurrentPrice;
                }
            }
            if (statType == StatType.itemBaseValue)
            {
                if (dealMaster.hideValue)
                {
                    if (dealMaster.itemBaseValue < 90)
                    {
                        txt.text = "Item Value: Hmm... That's probably worth less than $100";
                    }
                    if (dealMaster.itemBaseValue < 200)
                    {
                        txt.text = "Item Value: Hmm... That's probably worth around $100";
                    }
                    else if (dealMaster.itemBaseValue < 750)
                    {
                        txt.text = "Item Value: Hmm... That's probably worth a few hundred";
                    }
                    else if (dealMaster.itemBaseValue < 1250)
                    {
                        txt.text = "Item Value: Hmm... That's probably worth around a thousand";
                    }
                    else if (dealMaster.itemBaseValue < 4750)
                    {
                        txt.text = "Item Value: Hmm... That's probably worth about a couple thousand";
                    }
                    else if (dealMaster.itemBaseValue < 8500)
                    {
                        txt.text = "Item Value: Hmm... That's probably worth over $5000";
                    }
                    else
                    {
                        txt.text = "Item Value: This is a big ticket item, right here!";
                    }
                }
                else
                {
                    txt.text = "Item Value: $" + dealMaster.itemBaseValue;
                }
            }
            if (statType == StatType.previousOffer)
            {
                if (dealMaster.playerCounterOfferPrevious != 0)
                    txt.text = "Previous Offer: $" + dealMaster.playerCounterOfferPrevious;
                else
                    txt.text = "";

                if (dealMaster.dealMade)
                {
                    txt.text = "Ammount Spent: $" + dealMaster.playerCounterOfferPrevious;
                    this.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
                }
                else if (dealMaster.dealOver && !dealMaster.dealMade)
                {                    
                    this.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
                    if (dealMaster.notEnoughMoney)
                    {
                        txt.text = "You don't have the money!";
                    }
                    else
                    {
                        txt.text = "No Deal.";
                    }
                }
            }
        }
        if (statType == StatType.playerMoney)
        {
            txt.text = "$" + string.Format("{0:n0}", GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().playerMoney);
        }/*
        else if (statType == StatType.Yeast)
        {
            txt.text = "Yeast: " + persistentScript.playerYeast;
        }
        else if (statType == StatType.Hops)
        {
            txt.text = "Hops: " + persistentScript.playerHops;
        }
        else if (statType == StatType.Gold)
        {
            txt.text = "Gold: " + persistentScript.playerGold;
        }
        else if (statType == StatType.Drink1)
        {
            txt.text = "Drinks: " + persistentScript.playerDrink1;
        }
        else if (statType == StatType.Drink1WaterCost)
        {
            txt.text = "Ammount of \n Water: " + persistentScript.playerDrink1WaterCost;
        }
        else if (statType == StatType.Drink1BarleyCost)
        {
            txt.text = "Ammount of \n Barley: " + persistentScript.playerDrink1BarleyCost;
        }
        else if (statType == StatType.Drink1YeastCost)
        {
            txt.text = "Ammount of \n Yeast: " + persistentScript.playerDrink1YeastCost;
        }
        else if (statType == StatType.Drink1HopsCost)
        {
            txt.text = "Ammount of \n Hops: " + persistentScript.playerDrink1HopsCost;
        }*/
    }
}
