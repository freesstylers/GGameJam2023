using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField]
    Transform container;
    [SerializeField]
    Transform itemTemplate;
    [SerializeField]
    float ItemHeight;
    // Start is called before the first frame update
    void Start()
    {
        CreateItemButton(Item.ItemType.ExtraSword, Item.getItemCost(Item.ItemType.ExtraSword), 0);   
        CreateItemButton(Item.ItemType.UpdgradeSword, Item.getItemCost(Item.ItemType.UpdgradeSword), 1);   
        CreateItemButton(Item.ItemType.ExtraHealth, Item.getItemCost(Item.ItemType.ExtraHealth), 2);   
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateItemButton(Item.ItemType type, int itemCost, int positionIndex)
    {
        Transform additionalItem = Instantiate(itemTemplate, container);

        RectTransform additionalItemRT = additionalItem.GetComponent<RectTransform>();

        additionalItemRT.anchoredPosition = new Vector2(0, -ItemHeight * positionIndex);

        //Set template parameters
    }

    public void BuyItem(Item.ItemType type)
    {
        //Check money

        //if more than item.cost
        //{
        //add item to player
        //remove from shop
        //}
        //else
        //{

        //}
    }

    public class Item
    {
        public enum ItemType
        {
            None,
            ExtraSword,
            UpdgradeSword,
            ExtraHealth
            //...
        }

        public static int getItemCost(ItemType itemType)
        {
            int ret = 0;

            switch (itemType)
            {
                case ItemType.None:
                    ret = 0;
                    break;
                case ItemType.ExtraSword:
                    ret = 100;
                    break;
                case ItemType.UpdgradeSword:
                    ret = 200;
                    break;
                case ItemType.ExtraHealth:
                    ret = 150;
                    break;
            }

            return ret;
        }
    }
}
