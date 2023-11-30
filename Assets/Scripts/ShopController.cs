using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    [Header("Text UI")]
    public Text AmountCoinsText;
    public Text AmountBloodText;
    public Text AmountMagText;
    public Text PriceBloodText;
    public Text PriceMagText;
    public GameObject MessageBuyAlert;

    [Header("Item UI")]
    public GameObject bloodItem;
    public GameObject magItem;

    [Header("Amount")]
    public int currentCoins = 2;
    private int amountBlood;
    private int amountMag;

    [Header("Price")]
    public int priceBlood = 2;
    public float healBlood = 20f;
    public int priceMag = 3;

    public static ShopController occurrence;

    private void Awake()
    {
        occurrence = this;
    }

    private void Start()
    {
        amountBlood = 0;
        amountMag = 0;

        PriceBloodText.text = priceBlood + " coins";
        PriceMagText.text = priceMag + " coins";
        AmountCoinsText.text = currentCoins + "";
        AmountBloodText.text = "Amount: " + amountBlood;
        AmountMagText.text = "Amount: " + amountMag;

        bloodItem.SetActive(false);
        magItem.SetActive(false);
    }

    private void Update()
    {
        updateAmountText();
    }

    public void collectCoin()
    {
        currentCoins++;
    }

    public void buyBlood()
    {
        if(currentCoins < priceBlood)
        {
            displayBuyAlert();
            return;
        }
        
        amountBlood++;
        currentCoins = currentCoins - priceBlood;
    }

    public void buyMag()
    {
        if (currentCoins < priceMag)
        {
            displayBuyAlert();
            return;
        }

        amountMag++;
        currentCoins = currentCoins - priceMag;
    }

    public void useBlood()
    {
        if(amountBlood <= 0)
        {
            return;
        }

        amountBlood--;
        PlayerScript.occurrence.playerHealing(healBlood);
    }
    public void useMag()
    {
        if (amountMag <= 0)
        {
            return;
        }

        amountMag--;
        Rifle.occurrence.addMag();
    }

    private void displayBuyAlert()
    {
        MessageBuyAlert.SetActive(true);
        StartCoroutine(hiddenBuyAlert());
    }

    IEnumerator hiddenBuyAlert()
    {
        yield return new WaitForSeconds(1);
        MessageBuyAlert.SetActive(false);
    }

    private void updateAmountText()
    {
        if (amountBlood <= 0)
        {
            bloodItem.SetActive(false);
        }
        else
        {
            bloodItem.SetActive(true);
        }

        if (amountMag <= 0)
        {
            magItem.SetActive(false);
        }
        else
        {
            magItem.SetActive(true);
        }

        AmountCoinsText.text = currentCoins + "";
        AmountBloodText.text = "Amount: " + amountBlood;
        AmountMagText.text = "Amount: " + amountMag;
    }
}
