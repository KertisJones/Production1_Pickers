using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayStat : MonoBehaviour {

    private Text txt;
    private DealMaster persistentScript;

    public enum StatType { currentPrice, itemBaseValue, previousOffer };
    public StatType statType;
    // Use this for initialization
    void Start()
    {
        txt = GetComponent<Text>();
        persistentScript = GameObject.FindGameObjectWithTag("DealMaster").GetComponent<DealMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (statType == StatType.currentPrice)
        {
            if (persistentScript.playerCounterOfferPrevious == 0) {
                txt.text = "";
            }
            else if (persistentScript.dealMade)
            {
                txt.text = "Deal!";
            }
            else {
                txt.text = "Asking Price: $" + persistentScript.sellerCurrentPrice;
            }
        }
        if (statType == StatType.itemBaseValue)
        {
            if (persistentScript.hideValue)
            {
                if (persistentScript.itemBaseValue < 140)
                {
                    txt.text = "Item Value: Hmm... That's probably worth less than $100";
                }
                else if (persistentScript.itemBaseValue < 750)
                {
                    txt.text = "Item Value: Hmm... That's probably worth a few hundred";
                }
                else if (persistentScript.itemBaseValue < 1500)
                {
                    txt.text = "Item Value: Hmm... That's probably around a thousand";
                }
                else if (persistentScript.itemBaseValue < 7500)
                {
                    txt.text = "Item Value: Hmm... That's probably around a few thousand";
                }
                else
                {
                    txt.text = "Item Value: This is a big ticket item, right here!";
                }
            }
            else
            {
                txt.text = "Item Value: $" + persistentScript.itemBaseValue;
            }
        }
        else if (statType == StatType.previousOffer)
        {
            if (persistentScript.playerCounterOfferPrevious != 0)
                txt.text = "Previous Offer: $" + persistentScript.playerCounterOfferPrevious;
            else
                txt.text = "";

            if (persistentScript.dealMade)
            {
                txt.text = "Ammount Spent: $" + persistentScript.playerCounterOfferPrevious;
                this.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
            }
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
