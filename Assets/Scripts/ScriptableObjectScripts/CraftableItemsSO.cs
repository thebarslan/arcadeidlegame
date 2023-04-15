using DefaultNamespace;
using UnityEngine;

[CreateAssetMenu(menuName = "Item", fileName = "Item")]
public class CraftableItemsSO : ScriptableObject
{
    public enum ItemType { Sword, Shield, Spear, Bow };
    public ItemType itemType;
    [Space(5)]
    public string itemName;
    [Space(5)]
    public Sprite itemSprite;
    [Space(5)] 
    public Recipe recipe;
    
}
