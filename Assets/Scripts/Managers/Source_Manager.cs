using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Source_Manager : MonoBehaviour
{
    public int moneyAmount, woodAmount, stoneAmount;
    [SerializeField] private TextMeshProUGUI moneyText, woodText, stoneText;
    private void Start()
    {
        if (!PlayerPrefs.HasKey("Money"))
        {
            PlayerPrefs.SetInt("Money", 0);
            PlayerPrefs.SetInt("Wood", 0);
            PlayerPrefs.SetInt("Stone", 0);
        }
        moneyAmount = PlayerPrefs.GetInt("Money");
        woodAmount = PlayerPrefs.GetInt("Wood");
        stoneAmount = PlayerPrefs.GetInt("Stone");
        UpdateUI();
    }
    public void UpdateMoney(int money)
    {
        moneyAmount -= money;
        PlayerPrefs.SetInt("Money", moneyAmount);
        UpdateUI();
    }
    public void UpdateWood(int wood)
    {
        woodAmount += wood;
        PlayerPrefs.SetInt("Wood", woodAmount);
        UpdateUI();
    }

    public void UpdateStone(int stone)
    {
        stoneAmount += stone;
        PlayerPrefs.SetInt("Stone", stoneAmount);
        UpdateUI();
    }
    private void UpdateUI()
    {
        moneyText.text = "$" + moneyAmount.ToString();
        woodText.text = woodAmount.ToString();
        stoneText.text = stoneAmount.ToString();
    }
}
