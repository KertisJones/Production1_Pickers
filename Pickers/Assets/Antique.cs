using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antique : MonoBehaviour {
    private DealMaster dealMaster;
    private GameManager GM;
    public bool clicked = false;
    public bool gaveFinal = false;

    public int itemBaseValue = 500;

    private int itemSentimentalValue = 0;
    public bool dealOver = false;
    public bool dealMade = false;
    public bool notEnoughMoney = false;
    public bool hideValue = true;


    public int playerCounterOfferPrevious = 0;

    public int sellerCurrentPrice = 100;
    public int sellerMinPrice = 0;
    public int sellerMinPriceThreshold = 5;
    public int sellerCurrentPriceThreshold = 5;

    // Use this for initialization
    void Start()
    {
        itemBaseValue = Mathf.RoundToInt(Random.Range(50f, 10000f));
        itemSentimentalValue = Mathf.RoundToInt(itemBaseValue + Random.Range(0.8f, 1.2f) * RandomSign());

        sellerCurrentPrice = roundTo5(Mathf.RoundToInt(itemSentimentalValue * Random.Range(1.2f, 1.6f)));
        sellerMinPrice = Mathf.RoundToInt(itemSentimentalValue * Random.Range(0.2f, 0.6f));

        sellerMinPriceThreshold = Mathf.RoundToInt(sellerMinPrice * Random.Range(0.075f, 0.2f));
        sellerCurrentPriceThreshold = Mathf.RoundToInt(sellerCurrentPrice * Random.Range(0.075f, 0.2f));

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

        if (dealMaster == null && this.transform.childCount == 0)
        {
            clicked = true;
            //Destroy(this.gameObject);
            GM.currentItem = this.gameObject;
            GM.triggerNegotiation = true;
        }
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
}
