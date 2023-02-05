using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopEvent : BaseEvent
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

        base.changeText("test");

        MusicManager.instance.PlayShopMusic();
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


        if (GameManager.instance.hairsCollected > Item.getItemCost(type))
        {
            GameManager.instance.hairsCollected -= Item.getItemCost(type);

            //Modify player stats
        }
        else
        {
            //Sound
            //Modify color
        }
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
