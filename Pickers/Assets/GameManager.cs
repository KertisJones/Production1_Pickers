using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	//This script will be persitent through changing scenes 
	private static bool created = false;

    //private GameObject restartButton;
	public GameObject dealMasterPrefab;
	public GameObject dealMaster;
    public GameObject currentItem;

	public int playerMoney = 1000;
    public int playerItemValue = 0;

    public bool triggerNegotiation = false;

	void Awake()
	{
		if (!created)
		{
			DontDestroyOnLoad(this.gameObject);
			created = true;
			//Debug.Log("Awake: " + this.gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		//dealMaster = Instantiate (dealMasterPrefab);
	}
	
	// Update is called once per frame
	void Update () {

        if (triggerNegotiation == true)
        {
            triggerNegotiation = false;
            restartNegotiation();
        }

		//if (Input.GetButtonDown ("Cancel"))
		//	restartNegotiation ();

        //if (restartButton == null)
        //{
        //    restartButton = GameObject.FindGameObjectWithTag("RESTARTButton");
        //}
	}

	public void restartNegotiation(){
        endNegotiation();
        dealMaster = Instantiate (dealMasterPrefab);
	}

    public void endNegotiation()
    {
        if (dealMaster != null)
            Destroy(dealMaster);
    }
}
