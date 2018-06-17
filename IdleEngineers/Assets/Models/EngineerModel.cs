using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EngineerModel {

    Action<double> CurrentEarningDidUpdate;

    public string Name;
    public double CurrentPrice;

    public int Level;
    public int OrderNumber;

    [SerializeField]
    double currentEarning = 1;

    public double CurrentEarning
    {
        get
        {
            return currentEarning;
        }
        set
        {
            if(value > 0)
            {
                if (value > currentEarning)
                {
                    currentEarning = value;
                    if (CurrentEarningDidUpdate != null)
                    {
                        CurrentEarningDidUpdate(currentEarning);
                    }
                }
                else if (value < currentEarning)
                {
                    Debug.LogError("Engineer Current Earning cannot decrease");
                }
            }
            else
            {
                Debug.LogError("Engineer Current Earning must be more than 0");
            }
        }
    }

    public void RegisterCurrentEarningDidUpdateAction(Action<double> action)
    {
        CurrentEarningDidUpdate += action;
    }

    public void UnregisterCurrentEarningDidUpdateAction(Action<double> action)
    {
        CurrentEarningDidUpdate -= action;
    }

    public void UpgradeEngineer()
    {
        Level++;
        CurrentPrice = Math.Pow(4, Level);
        CurrentEarning *= 2;
    }
}
