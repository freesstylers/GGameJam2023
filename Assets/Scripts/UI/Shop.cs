using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField]
    Transform container;
    [SerializeField]
    Transform itemTemplate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public class Item
    {
        public enum ItemType
        {
            None,
            ExtraSword
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
            }

            return ret;
        }
    }
}
