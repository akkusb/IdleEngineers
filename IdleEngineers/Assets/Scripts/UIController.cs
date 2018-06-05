using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public Text MoneyText;

    // Use this for initialization
    void Start () {
        Invoke("RegisterMoneyDidUpdateAction", 1);
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void UpdateMoneyText(double money)
    {
        MoneyText.text = money.ToString();
    }

    void RegisterMoneyDidUpdateAction()
    {
        CompanyController.Instance.RegisterMoneyDidUpdateAction(UpdateMoneyText);
    }
}
