using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace items
{
    public enum ItemType { Heal, MaxPlus, Boulon}
    [CreateAssetMenu(fileName = "newItem", menuName = "Julia/Item", order = 0)]
    public class ItemScriptableObject : ScriptableObject
    {
        public Sprite itemSprite;
        public string itemName, itemDescription;
        public bool isFromShop;
        public Collider itemCollider;
        public int itemPrice;
        public ItemType itemType;
        public float strength;

    }
}
