using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CompanyController : MonoBehaviour {

    double money = 0;
    double moneyPerSecond = 0;

    public Action<double> MoneyDidUpdate;
    public Action<double> MoneyPerSecondDidUpdate;

    public GameObject EngineerPrefab;

    public static CompanyController Instance { get; protected set; }

    public CompanyModel company;
    Dictionary<EngineerModel, GameObject> engineerGameObjectMap;

	// Use this for initialization
	void Start () {
        Instance = this;
        engineerGameObjectMap = new Dictionary<EngineerModel, GameObject>();
        company = new CompanyModel();
        CreateEngineerGameObjects();

        InvokeRepeating("UpdateMoney", 0f, 1f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void CreateEngineerGameObjects()
    {
        foreach (EngineerModel engineer in company.engineers)
        {
            GameObject engineerGameObject = Instantiate(EngineerPrefab, Vector3.zero, Quaternion.identity);

            engineerGameObject.name = engineer.Name;
            engineerGameObject.transform.SetParent(this.transform, true);

            engineerGameObjectMap.Add(engineer, engineerGameObject);
        }
    }

    void UpdateMoney()
    {
        money += moneyPerSecond;
        if (MoneyDidUpdate != null)
        {
            MoneyDidUpdate(money);
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
}
