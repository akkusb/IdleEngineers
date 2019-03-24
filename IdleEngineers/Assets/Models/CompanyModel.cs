using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public class CompanyModel{

    [SerializeField]
    double money = 0;

    public List<EngineerModel> engineers;
    private Action EngineerDidUpdate;


    private static string fileName = "Company.json";

    public CompanyModel()
    {
        //bool isLoaded = this.LoadFromFile();
        //if (isLoaded == false)
        //{
        //    this.CreateEngineers();
        //}
        this.CreateEngineers();
    }

    public double Money
    {
        get
        {
            return money;
        }
        set
        {
            money = value;
            //if (value > 0)
            //{
            //    if (value > currentEarning)
            //    {
            //        currentEarning = value;
            //        if (CurrentEarningDidUpdate != null)
            //        {
            //            CurrentEarningDidUpdate(currentEarning);
            //        }
            //    }
            //    else if (value < currentEarning)
            //    {
            //        Debug.LogError("Engineer Current Earning cannot decrease");
            //    }
            //}
            //else
            //{
            //    Debug.LogError("Engineer Current Earning must be more than 0");
            //}
        }
    }

    public void SaveToFile()
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        bool isFileExists = File.Exists(filePath);

        //BinaryFormatter binaryFormatter = new BinaryFormatter();
        //FileStream saveFile;
        //if (isFileExists)
        //{
        //    saveFile = File.Open(filePath, FileMode.Open);
        //}
        //else
        //{
        //    saveFile = File.Create(filePath);

        //}

        //binaryFormatter.Serialize(saveFile, engineers);
        //saveFile.Close();


        string dataAsJson = JsonUtility.ToJson(this, true);
        File.WriteAllText(filePath, dataAsJson);
    }

    public static CompanyModel LoadFromFile()
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        bool isFileExists = File.Exists(filePath);

        if (isFileExists)
        {
            //BinaryFormatter binaryFormatter = new BinaryFormatter();
            //FileStream loadFile = File.Open(Application.persistentDataPath + fileName, FileMode.Open);
            //engineers = (List<EngineerModel>)binaryFormatter.Deserialize(loadFile);
            //loadFile.Close();

            string dataAsJson = File.ReadAllText(filePath);
            return JsonUtility.FromJson<CompanyModel>(dataAsJson);


        }

        return new CompanyModel();
    }

    public void CreateEngineers()
    {
        this.engineers = new List<EngineerModel>();

        EngineerModel engineer = new EngineerModel();
        engineer.Id = 1;
        engineer.Name = "Developer";
        engineer.CurrentPrice = 1;
        engineer.CurrentEarning = 1;
        engineer.Level = 1;
        engineer.OrderNumber = 0;
        engineer.RegisterCurrentEarningDidUpdateAction(EngineerEarningDidUpdate);

        this.engineers.Add(engineer);

        EngineerModel engineer2 = new EngineerModel();
        engineer2.Id = 2;
        engineer2.Name = "Developer2";
        engineer2.CurrentPrice = 2;
        engineer2.CurrentEarning = 2;
        engineer2.Level = 1;
        engineer2.OrderNumber = 1;
        engineer2.RegisterCurrentEarningDidUpdateAction(EngineerEarningDidUpdate);

        this.engineers.Add(engineer2);
    }

    void EngineerEarningDidUpdate(double earning)
    {
        EngineerDidUpdate();
    }

    public void RegisterEngineerDidUpdateAction(Action action)
    {
        EngineerDidUpdate += action;
    }

    public void UnregisterEngineerDidUpdateAction(Action action)
    {
        EngineerDidUpdate -= action;
    }

}
