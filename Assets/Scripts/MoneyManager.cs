using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;
    
    public TMP_Text moneyCountText;
    public int moneyCount;

    private void Awake()
    {
        Instance = this;
    }
    
    public void EarnMoney(int value)
    {
        moneyCount += value;
        moneyCountText.text = moneyCount.ToString();
    }
    
    public void SpendMoney(int value)
    {
        moneyCount -= value;
        moneyCountText.text = moneyCount.ToString();
    }
}
