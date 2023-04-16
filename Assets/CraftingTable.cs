using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class CraftingTable : MonoBehaviour
{
    [SerializeField] private GameObject craftingCanvas;
    [SerializeField] private Image craftingCountdownImage;
    [SerializeField] private Image craftingItemImage;
    [SerializeField] private TextMeshProUGUI craftingCountdownText;
    [SerializeField] private int countdown;
    [SerializeField] private int totalCountdown;
    public bool isCrafting;
    private int minute;
    private int second;
    public Item item;
    private void Start()
    {
        craftingCanvas.SetActive(false);
    }

    public void StartCraft(Item _item)
    {
        Debug.Log("Craft");
        item = _item;
        craftingItemImage.sprite = item.itemSprite;
        craftingCanvas.SetActive(true);
        FindObjectOfType<Crafting_Manager>().CloseCraftingPanel();
        
        StartCoroutine(CraftingCountdown());
        isCrafting = true;
    }
    private void EndCraft()
    {
        FindObjectOfType<Inventory_Manager>().AddInventory(item);
        craftingCanvas.SetActive(false);
        isCrafting = false;
        Debug.Log("Çalış");
    }
    private IEnumerator CraftingCountdown()
    {
        totalCountdown = item.itemCraftingTime;
        countdown = totalCountdown;
        ChangeUI();
        while (countdown > 0)
        {
            yield return new WaitForSeconds(1f);
            countdown--;
            ChangeUI();
        }
        EndCraft();
    }
    private void ChangeUI()
    {
        CountdownToTimer();
        if (minute == 0)
        {
            craftingCountdownText.text = second.ToString();
        }
        else if(second < 10)
        {
            craftingCountdownText.text = minute.ToString() + ":" + "0"+second.ToString();
        }
        else
        {
            craftingCountdownText.text = minute.ToString() + ":" + second.ToString();
        }
        craftingCountdownImage.fillAmount = (float)countdown / (float)totalCountdown;
    }
    private void CountdownToTimer()
    {
        if (countdown >= 60)
        {
            minute = (countdown / 60);
            second = countdown % 60;
        }
        else
        {
            minute = 0;
            second = countdown % 60;
        }
        
    }
}
