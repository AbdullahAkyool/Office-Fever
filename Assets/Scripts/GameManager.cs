using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float stackLimit;
    public float stackSpeed;
    
    public float deskLimit;
    public float workerSpeed;

    public TMP_Text stackLimitUpgradeText;
    public TMP_Text stackSpeedUpgradeText;
    public TMP_Text deskLimitUpgradeText;
    public TMP_Text workerSpeedUpgradeText;

    public int stackLimitUpgradePrice;
    public int stackSpeedUpgradePrice;
    public int deskLimitUpgradePrice;
    public int workerSpeedUpgradePrice;
    
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateUI()
    {
        stackLimitUpgradeText.text = stackLimitUpgradePrice.ToString();
        stackSpeedUpgradeText.text = stackSpeedUpgradePrice.ToString();
        deskLimitUpgradeText.text = deskLimitUpgradePrice.ToString();
        workerSpeedUpgradeText.text = workerSpeedUpgradePrice.ToString();
    }

    public void UpgradeStackLimit()
    {
        if(MoneyManager.Instance.moneyCount < stackLimitUpgradePrice) return;
        
        stackLimit += 5;
        MoneyManager.Instance.SpendMoney(stackLimitUpgradePrice);
    }
    
    public void UpgradeStackSpeed()
    {
        if(MoneyManager.Instance.moneyCount < stackSpeedUpgradePrice) return;
        if (stackSpeed <= 0)
        {
            stackSpeed = .02f;
        }
        else
        {
            stackSpeed -= .02f;
        }
        MoneyManager.Instance.SpendMoney(stackSpeedUpgradePrice);

    }
    
    public void UpgradeDeskLimit()
    {
        if(MoneyManager.Instance.moneyCount < deskLimitUpgradePrice) return;
        deskLimit += 5;
        MoneyManager.Instance.SpendMoney(deskLimitUpgradePrice);
    }
    
    public void UpgradeWorkerSpeed()
    {
        if(MoneyManager.Instance.moneyCount < workerSpeedUpgradePrice) return;
        if (workerSpeed <=0)
        {
            workerSpeed = .09f;
        }
        else
        {
            workerSpeed -= .02f;
        }
        MoneyManager.Instance.SpendMoney(workerSpeedUpgradePrice);

    }
}
