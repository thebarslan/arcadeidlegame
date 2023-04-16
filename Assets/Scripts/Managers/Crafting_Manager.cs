using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;

namespace Managers
{
    public class Crafting_Manager : MonoBehaviour
    {
        [Header("UI")] 
        [SerializeField] private GameObject craftingPanel;
        [SerializeField] private GameObject[] craftingItems = new GameObject[4];
        [SerializeField] private GameObject[] craftingItemResources = new GameObject[4];
        
        
        public void OpenCraftfingPanel()
        {
            if(FindObjectOfType<CraftingTable>().GetComponent<CraftingTable>().isCrafting == true) return;
            craftingPanel.transform.DOScale(Vector3.one, .2f).SetEase(Ease.OutElastic);
        }
        public void CloseCraftingPanel()
        {
            craftingPanel.transform.DOScale(Vector3.zero, .2f).SetEase(Ease.OutElastic);
        }

        private void SetCraftingItems()
        {
            foreach (GameObject _craftingItems in craftingItems)
            {
                _craftingItems.GetComponent<Image>().sprite = _craftingItems.GetComponent<Item>().itemSprite;
            }
        }
        private void SetCraftingResources()
        {
            for (int i = 0; i < craftingItems.Length; i++)
            {
                craftingItemResources[i].transform.GetChild(0).transform.GetChild(1).GetComponent<TextMeshProUGUI>()
                    .text = craftingItems[i].GetComponent<Item>().itemRecipe.wood.ToString();
                craftingItemResources[i].transform.GetChild(1).transform.GetChild(1).GetComponent<TextMeshProUGUI>()
                    .text = craftingItems[i].GetComponent<Item>().itemRecipe.stone.ToString();
            }
        }
        private void SetCraftingItemButtons()
        {
            foreach (GameObject _craftingItems in craftingItems)
            {
                _craftingItems.transform.parent.GetComponent<Button>().onClick.AddListener( () => FindObjectOfType<CraftingTable>().StartCraft(_craftingItems.GetComponent<Item>()));
            }
        }
        private void Start()
        {
            SetCraftingItems();
            SetCraftingResources();
            SetCraftingItemButtons();
        }

       
    }
}
