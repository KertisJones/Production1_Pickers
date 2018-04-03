using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seller : MonoBehaviour {
	public float minPrice = 0f;
	public float currentPrice = 100f;

	public float minPriceThreshold = 5f;
	public float currentPriceThreshold = 5f;

	public int anger = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		minPrice = Mathf.Round(minPrice * 100) / 100;
		currentPrice = Mathf.Round(currentPrice * 100) / 100;
		minPriceThreshold = Mathf.Round(minPriceThreshold * 100) / 100;
		currentPriceThreshold = Mathf.Round(currentPriceThreshold * 100) / 100;

		if (currentPrice < minPrice) {
			currentPrice = minPrice;
		}
	}



}