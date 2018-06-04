using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanyModel{

    public List<EngineerModel> engineers;

    public CompanyModel()
    {
        bool isLoaded = this.LoadFromFile();
        if (isLoaded == false)
        {
            this.CreateEngineers();
        }
        
    }

    public void SaveToFile()
    {

    }

    public bool LoadFromFile()
    {
        return false;
    }

    public void CreateEngineers()
    {
        this.engineers = new List<EngineerModel>();

        EngineerModel engineer = new EngineerModel();
        engineer.Name = "Developer";
        engineer.CurrentPrice = 1;
        engineer.CurrentEarning = 1;
        engineer.Level = 1;
        engineer.OrderNumber = 0;

        this.engineers.Add(engineer);
    }

}
