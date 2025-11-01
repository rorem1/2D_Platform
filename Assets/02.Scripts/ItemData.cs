using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item",menuName = "ScriptableObject/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType { Range , Heal}

    [Header("Setting")]
     public ItemType itemType;
     public int itemId;
     public string itemName;
     public string itemDesc;
     public Sprite itemIcon;

    [Header("Level Setting")]
     public float baseDamage;
     public int baseCount;
     public float[] damages;
     public int[] counts;

    [Header("Weapon")]
     public GameObject projectile;
    
}
