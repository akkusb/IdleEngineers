using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CompanyController : MonoBehaviour {

    private double money
    {
        get
        {
            return company.Money;
        }
        set
        {
            company.Money = value;
        }
    }

    double moneyPerSecond = 0;

    private Action<double> MoneyDidUpdate;
    private Action<double> MoneyPerSecondDidUpdate;

    public GameObject EngineerPrefab;
    public Text MoneyText;


    public static CompanyController Instance { get; protected set; }

    public CompanyModel company;
    Dictionary<EngineerModel, GameObject> engineerGameObjectMap;

	// Use this for initialization
	void Start () {
        Instance = this;
        engineerGameObjectMap = new Dictionary<EngineerModel, GameObject>();
        //company = new CompanyModel();
        LoadButtonOnClick();
        
        CreateEngineerGameObjects();

        InvokeRepeating("UpdateMoney", 0f, 1f);
    }
	
	// Update is called once per frame
	void Update () {
        UpdateMoneyText();
	}

    void CreateEngineerGameObjects()
    {
        foreach (EngineerModel engineer in company.engineers)
        {
            GameObject engineerGameObject = Instantiate(EngineerPrefab, Vector3.zero, Quaternion.identity);
            Button button = engineerGameObject.GetComponent<Button>();
            
            button.onClick.AddListener(delegate {
                UpgradeEngineerButtonOnClick(engineer);
            });
            engineerGameObject.name = engineer.Name;
            engineerGameObject.transform.SetParent(this.transform, true);

            engineerGameObjectMap.Add(engineer, engineerGameObject);
            UpdateEngineerUI(engineer);
        }
    }

    public void UpgradeEngineerButtonOnClick(EngineerModel engineer)
    {
        if (money >= engineer.CurrentPrice)
        {
            money -= engineer.CurrentPrice;
            engineer.UpgradeEngineer();

            UpdateEngineerUI(engineer);
        }
        else
        {
            Debug.LogError("Not enough money to upgrade the engineer!");
        }
        
    }

    void UpdateMoney()
    {
        if (moneyPerSecond > 0)
        {
            money += moneyPerSecond;
            if (MoneyDidUpdate != null)
            {
                MoneyDidUpdate(money);
            }
        }
        else
        {
            UpdateMoneyPerSecond();
        }
        
    }

    void UpdateMoneyPerSecond()
    {
        double updatedMoneyPerSecond = 0;
        foreach (EngineerModel engineer in company.engineers)
        {
            updatedMoneyPerSecond += engineer.CurrentEarning;
        }

        moneyPerSecond = updatedMoneyPerSecond;
        if (MoneyPerSecondDidUpdate != null)
        {
            MoneyPerSecondDidUpdate(moneyPerSecond);
        }
    }

    void EngineerDidUpdate()
    {
        // Refresh money per second
        UpdateMoneyPerSecond();
    }

    void UpdateEngineerUI(EngineerModel engineer)
    {
        GameObject engineerGameObject = engineerGameObjectMap[engineer];
        Text nameText = engineerGameObject.GetComponentInChildren<Text>();
        nameText.text = engineer.Name + "  Price: " + engineer.CurrentPrice + " Earning: " + engineer.CurrentEarning;
    }

    void UpdateMoneyText()
    {
        MoneyText.text = money.ToString();
    }

    public void RegisterMoneyDidUpdateAction(Action<double> action)
    {
        MoneyDidUpdate += action;
    }

    public void UnregisterMoneyDidUpdateAction(Action<double> action)
    {
        MoneyDidUpdate -= action;
    }

    public void RegisterMoneyPerSecondDidUpdateAction(Action<double> action)
    {
        MoneyPerSecondDidUpdate += action;
    }

    public void UnregisterMoneyPerSecondDidUpdateAction(Action<double> action)
    {
        MoneyPerSecondDidUpdate -= action;
    }

    public void SaveButtonOnClick()
    {
        company.SaveToFile();
    }

    public void LoadButtonOnClick()
    {
        company = CompanyModel.LoadFromFile();
        company.RegisterEngineerDidUpdateAction(EngineerDidUpdate);
        UpdateMoneyPerSecond();
    }

}
