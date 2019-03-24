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

    private CompanyModel company;
    Dictionary<int, GameObject> engineerGameObjectMap;

    private void Awake()
    {
        Instance = this;
        engineerGameObjectMap = new Dictionary<int, GameObject>();
        CreateCompany();
        CreateEngineerGameObjects();
    }

    //// Use this for initialization
    //void Start () {
    //    Instance = this;
    //    engineerGameObjectMap = new Dictionary<EngineerModel, GameObject>();
    //    //company = new CompanyModel();
    //    //LoadButtonOnClick();
        
    //    CreateEngineerGameObjects();
    //}
	
	// Update is called once per frame
	void Update () {
        UpdateMoney();
        UpdateMoneyText();
	}

    void CreateEngineerGameObjects()
    {
        foreach (EngineerModel engineer in company.engineers)
        {
            GameObject engineerGameObject = Instantiate(EngineerPrefab, Vector3.zero, Quaternion.identity);
            Button button = engineerGameObject.GetComponent<Button>();
            int engineerId = engineer.Id;
            button.onClick.AddListener(delegate {
                UpgradeEngineerButtonOnClick(GetEngineerModelById(engineerId));
            });
            engineerGameObject.name = engineer.Name;
            engineerGameObject.transform.SetParent(this.transform, true);

            engineerGameObjectMap.Add(engineer.Id, engineerGameObject);
            UpdateEngineerUI(engineer);
        }
    }

    void RefreshEngineerGameObjectMap()
    {
        foreach (EngineerModel engineer in company.engineers)
        {
            UpdateEngineerUI(engineer);
            //if (engineerGameObjectMap.ContainsKey(engineer.Id))
            //{

            //}
            //GameObject engineerGameObject = engineerGameObjectMap[engineer.Id];

            //engineerGameObjectMap.Add(engineer.Id, engineerGameObject);
            //UpdateEngineerUI(engineer);
        }
    }

    EngineerModel GetEngineerModelById(int Id)
    {
        return company.engineers.Find(engineer => engineer.Id == Id);
    }

    GameObject GetEngineerGameObjectByEngineer(EngineerModel engineer)
    {
        int Id = engineer.Id;
        if(engineerGameObjectMap.ContainsKey(Id))
        {
            return engineerGameObjectMap[Id];
        }
        else
        {
            return null;
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
            money += Time.deltaTime * moneyPerSecond;
            //money += moneyPerSecond;
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
        GameObject engineerGameObject = engineerGameObjectMap[engineer.Id];
        Text nameText = engineerGameObject.GetComponentInChildren<Text>();
        nameText.text = engineer.Name + "  Price: " + engineer.CurrentPrice + " Earning: " + engineer.CurrentEarning;
    }

    void UpdateMoneyText()
    {
        string moneyText = Math.Floor(money).ToString();
        
        MoneyText.text = moneyText;
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
        RefreshEngineerGameObjectMap();
        company.RegisterEngineerDidUpdateAction(EngineerDidUpdate);
        UpdateMoneyPerSecond();
    }

    public void CreateCompany()
    {
        //Test

        company = new CompanyModel();
        company.RegisterEngineerDidUpdateAction(EngineerDidUpdate);
        UpdateMoneyPerSecond();

        //Prod

        //LoadButtonOnClick();
    }

}
