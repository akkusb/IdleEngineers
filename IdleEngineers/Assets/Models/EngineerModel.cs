using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EngineerModel {

    Action<double> CurrentEarningDidUpdate;

    public string Name { get; set; }
    public double CurrentPrice { get; set; }
    public double CurrentEarning { get; set; }
    public int Level { get; set; }
    public int OrderNumber { get; set; }

    public void RegisterCurrentEarningDidUpdateAction(Action<double> action)
    {
        CurrentEarningDidUpdate += action;
    }

    public void UnregisterCurrentEarningDidUpdateAction(Action<double> action)
    {
        CurrentEarningDidUpdate -= action;
    }
}
