using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyingSystem : MonoBehaviour
{
    public GameObject productPrefab;
    public float productPrice;
    public float givenMoney;
    public float progressCount;
    
    public Image progressBar;
    public TMP_Text productPriceText; 
    void Start()
    {
        productPriceText.text = productPrice.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateProduct(int playerMoney)
    {
         givenMoney += playerMoney;

         if (givenMoney >= productPrice)
         {
             progressBar.fillAmount = 1;
             var newProduct = Instantiate(productPrefab,transform.position, Quaternion.identity);
             newProduct.name = productPrefab.tag;
             this.gameObject.SetActive(false);
         }
         else
         {
             progressBar.fillAmount = givenMoney / productPrice;
         }
    }
}
