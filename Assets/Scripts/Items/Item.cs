using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private CraftableItemsSO ItemSO;

    public string itemName;
    public Sprite itemSprite;
    public string itemType;
    public Recipe itemRecipe;
    public int itemCount;

    private void Start()
    {
        itemName = ItemSO.itemName;
        itemSprite = ItemSO.itemSprite;
        itemType = ItemSO.itemType.ToString();
        itemRecipe = ItemSO.recipe;
    }
}
