using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class Inventory_Manager : MonoBehaviour
    {
        [SerializeField] private int inventorySize;
        public List<Item> items = new List<Item>(7);
        public List<Sprite> itemSprites = new List<Sprite>(7); 
        [SerializeField] private GameObject itemPanel;

        private void Start()
        {
            RefreshInventory();
        }

        private bool CheckInventory(Item _item)
        {
            Debug.Log("Girdi");
            foreach (var t in items)
            {
                if (t.itemName == _item.itemName)
                {
                    return true;
                }
            }

            return false;
        }


        public void AddInventory(Item item)
        {
            if (CheckInventory(item))
            {
                item.itemCount++;
                RefreshInventory();
                Debug.Log("a");
            }
            else
            {
                Debug.Log("b");
                items.Add(item);
                item.itemCount++;
                RefreshInventory();
            }
        
        
        }
    
        public void RemoveInventory(Item item)
        {
            if (CheckInventory(item))
            {
                item.itemCount--;
                if (item.itemCount == 0)
                {
                    items.Remove(item);
                }
                RefreshInventory();
            }
            else
            {
                if (item.itemCount == 0)
                {
                    return;
                }
                items.Remove(item);
                item.itemCount--;
                RefreshInventory();
            }
        }
    
        private void RefreshInventory()
        {
            itemSprites.Clear();
            foreach (Item item in items)
            {
                if (item != null)
                {
                    itemSprites.Add(item.itemSprite);
                }
            }
            for (int i = 0; i < inventorySize; i++)
            {
                if (itemSprites.Count > i)
                {
                    itemPanel.transform.GetChild(i).transform.GetChild(0).transform.GetComponent<Image>().sprite = itemSprites[i];
                    itemPanel.transform.GetChild(i).transform.GetChild(0).transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                    itemPanel.transform.GetChild(i).transform.GetChild(0).transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = items[i].itemCount.ToString();
                }
                else
                {
                    itemPanel.transform.GetChild(i).transform.GetChild(0).transform.GetComponent<Image>().sprite = null;
                    itemPanel.transform.GetChild(i).transform.GetChild(0).transform.localScale = Vector3.zero;
                    itemPanel.transform.GetChild(i).transform.GetChild(0).transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = "";
                }
            }
        }
    }
}
